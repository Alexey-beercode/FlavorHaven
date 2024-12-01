import Foundation

struct ErrorResponse: Codable {
    let error: String
    let details: String
}


class CartService {
    static let shared = CartService()
    private init() {}

    func fetchCart(
        userId: String,
        completion: @escaping (Result<[CartItem], Error>) -> Void
    ) {
        guard let token = TokenManager.shared.loadToken(key: "accessToken") else {
            print("No access token found")
            completion(.failure(NSError(domain: "No access token", code: 401)))
            return
        }

        guard let url = URL(string: "http://localhost:8081/api/cart/get-by-user-id/\(userId)") else {
            print("Invalid URL")
            completion(.failure(NSError(domain: "Invalid URL", code: 400)))
            return
        }

        var request = URLRequest(url: url)
        request.httpMethod = "GET"
        request.setValue("Bearer \(token)", forHTTPHeaderField: "Authorization")
        request.setValue("*/*", forHTTPHeaderField: "accept")

        URLSession.shared.dataTask(with: request) { data, response, error in
            if let error = error {
                print("Network error: \(error.localizedDescription)")
                completion(.failure(error))
                return
            }

            guard let data = data else {
                print("No data received from server")
                completion(.failure(NSError(domain: "No data received", code: 500)))
                return
            }

            if let httpResponse = response as? HTTPURLResponse, httpResponse.statusCode != 200 {
                if let errorResponse = try? JSONDecoder().decode(ValidationError.self, from: data) {
                    print("Server error: \(errorResponse.description)")
                    completion(.failure(NSError(domain: errorResponse.description, code: httpResponse.statusCode)))
                } else {
                    print("Unknown server error")
                    completion(.failure(NSError(domain: "Unknown server error", code: httpResponse.statusCode)))
                }
                return
            }
            
            print(String(data: data, encoding: .utf8) ?? "Нет данных")

            do {
                let cartItems = try JSONDecoder().decode([CartItem].self, from: data)
                completion(.success(cartItems))
            } catch {
                print("Failed to decode response: \(error.localizedDescription)")
                completion(.failure(error))
            }
        }.resume()
    }
    
    func deleteCartItem(
            userId: String,
            dishId: String,
            count: Int,
            completion: @escaping (Result<Void, Error>) -> Void
        ) {
            guard let token = TokenManager.shared.loadToken(key: "accessToken") else {
                print("No access token found")
                completion(.failure(NSError(domain: "No access token", code: 401)))
                return
            }

            guard let url = URL(string: "http://localhost:8081/api/cart/remove-by-user-id/\(userId)") else {
                print("Invalid URL")
                completion(.failure(NSError(domain: "Invalid URL", code: 400)))
                return
            }

            var request = URLRequest(url: url)
            request.httpMethod = "DELETE"
            request.setValue("Bearer \(token)", forHTTPHeaderField: "Authorization")
            request.setValue("application/json", forHTTPHeaderField: "Content-Type")

            let body: [String: Any] = [
                "dishId": dishId,
                "count": count
            ]

            do {
                request.httpBody = try JSONSerialization.data(withJSONObject: body, options: [])
            } catch {
                print("Failed to encode request body: \(error.localizedDescription)")
                completion(.failure(error))
                return
            }

            URLSession.shared.dataTask(with: request) { data, response, error in
                if let error = error {
                    print("Network error: \(error.localizedDescription)")
                    completion(.failure(error))
                    return
                }

                guard let httpResponse = response as? HTTPURLResponse else {
                    print("No response from server")
                    completion(.failure(NSError(domain: "No response from server", code: 500)))
                    return
                }
                if httpResponse.statusCode == 200 {
                    completion(.success(()))
                } else {
                    var errorMessage = "Unknown error"
                    if let data = data,
                       let errorResponse = try? JSONDecoder().decode(ErrorResponse.self, from: data) {
                        errorMessage = "Error: \(errorResponse.error)\nDetails: \(errorResponse.details)"
                    } else {
                        errorMessage = "Server returned status code \(httpResponse.statusCode)"
                    }
                    print("Failed to add item: \(errorMessage)")
                    completion(.failure(NSError(domain: errorMessage, code: httpResponse.statusCode)))
                }
            }.resume()
        }
    
    func addCartItem(
        userId: String,
        dishId: String,
        count: Int,
        completion: @escaping (Result<Void, Error>) -> Void
    ) {
        guard let token = TokenManager.shared.loadToken(key: "accessToken") else {
            print("No access token found")
            completion(.failure(NSError(domain: "No access token", code: 401)))
            return
        }

        guard let url = URL(string: "http://localhost:8081/api/cart/add-by-user-id/\(userId)") else {
            print("Invalid URL")
            completion(.failure(NSError(domain: "Invalid URL", code: 400)))
            return
        }

        var request = URLRequest(url: url)
        request.httpMethod = "POST"
        request.setValue("Bearer \(token)", forHTTPHeaderField: "Authorization")
        request.setValue("application/json", forHTTPHeaderField: "Content-Type")

        let body: [String: Any] = [
            "dishId": dishId,
            "count": count
        ]

        do {
            request.httpBody = try JSONSerialization.data(withJSONObject: body, options: [])
        } catch {
            print("Failed to encode request body: \(error.localizedDescription)")
            completion(.failure(error))
            return
        }

        URLSession.shared.dataTask(with: request) { data, response, error in
            if let error = error {
                print("Network error: \(error.localizedDescription)")
                completion(.failure(error))
                return
            }

            guard let httpResponse = response as? HTTPURLResponse else {
                print("No response from server")
                completion(.failure(NSError(domain: "No response from server", code: 500)))
                return
            }

            if httpResponse.statusCode == 204 {
                completion(.success(()))
            } else {
                var errorMessage = "Server returned status code \(httpResponse.statusCode)"
                
                if let data = data,
                   let errorResponse = try? JSONDecoder().decode(ErrorResponse.self, from: data) {
                    errorMessage = "Error: \(errorResponse.error)\nDetails: \(errorResponse.details)"
                }
                
                if data == nil {
                    errorMessage = "Server returned status code \(httpResponse.statusCode), but no additional details were provided."
                }

                print("Failed to add item: \(errorMessage)")
                completion(.failure(NSError(domain: errorMessage, code: httpResponse.statusCode)))
            }
        }.resume()
    }
}
