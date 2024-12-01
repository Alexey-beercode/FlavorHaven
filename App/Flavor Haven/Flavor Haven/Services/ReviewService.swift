import Foundation

struct ReviewRequest: Codable {
    let orderId: String
    let note: String
}

struct ReviewResponse: Codable {
    let note: String
}

class ReviewService {
    static let shared = ReviewService()
    private init() {}

    func createReview(orderId: String, note: String, completion: @escaping (Result<Void, Error>) -> Void) {
        guard let token = TokenManager.shared.loadToken(key: "accessToken") else {
            completion(.failure(NSError(domain: "No access token", code: 401)))
            return
        }

        guard let url = URL(string: "http://localhost:8081/api/review/create") else {
            completion(.failure(NSError(domain: "Invalid URL", code: 400)))
            return
        }

        var request = URLRequest(url: url)
        request.httpMethod = "POST"
        request.setValue("Bearer \(token)", forHTTPHeaderField: "Authorization")
        request.setValue("application/json", forHTTPHeaderField: "Content-Type")

        let reviewData = ReviewRequest(orderId: orderId, note: note)

        do {
            request.httpBody = try JSONEncoder().encode(reviewData)
        } catch {
            completion(.failure(error))
            return
        }

        URLSession.shared.dataTask(with: request) { data, response, error in
            if let error = error {
                print(error)
                completion(.failure(error))
                return
            }

            guard let httpResponse = response as? HTTPURLResponse, httpResponse.statusCode == 200 else {
                completion(.failure(NSError(domain: "Server error", code: (response as? HTTPURLResponse)?.statusCode ?? 500)))
                return
            }

            completion(.success(()))
        }.resume()
    }
    
    func getReviewByOrderId(orderId: String, completion: @escaping (Result<String, Error>) -> Void) {
        guard let token = TokenManager.shared.loadToken(key: "accessToken") else {
            completion(.failure(NSError(domain: "No access token", code: 401)))
            return
        }

        guard let url = URL(string: "http://localhost:8081/api/review/get-by-order/\(orderId)") else {
            completion(.failure(NSError(domain: "Invalid URL", code: 400)))
            return
        }

        var request = URLRequest(url: url)
        request.httpMethod = "GET"
        request.setValue("Bearer \(token)", forHTTPHeaderField: "Authorization")
        request.setValue("application/json", forHTTPHeaderField: "Content-Type")

        URLSession.shared.dataTask(with: request) { data, response, error in
            if let error = error {
                print(error)
                completion(.failure(error))
                return
            }

            guard let data = data else {
                completion(.failure(NSError(domain: "No data received", code: 500)))
                return
            }

            do {
                let reviewResponse = try JSONDecoder().decode(ReviewResponse.self, from: data)
                completion(.success(reviewResponse.note))
            } catch {
                print("Failed to decode response: \(error)")
                completion(.failure(error))
            }
        }.resume()
    }
}
