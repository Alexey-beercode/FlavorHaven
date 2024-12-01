import Foundation

struct OrderRequest: Codable {
    let address: String
    let note: String
}

struct OrderResponse: Codable {
    let id: String
    let createdTime: String
}

struct OrderSummary: Decodable {
    let id: String
    let createdTime: String
    let statusName: String
    
    enum CodingKeys: String, CodingKey {
        case id
        case createdTime
        case status
    }
    
    enum StatusKeys: String, CodingKey {
        case name
    }
    
    init(from decoder: Decoder) throws {
        let container = try decoder.container(keyedBy: CodingKeys.self)
        id = try container.decode(String.self, forKey: .id)
        createdTime = try container.decode(String.self, forKey: .createdTime)
        
        let statusContainer = try container.nestedContainer(keyedBy: StatusKeys.self, forKey: .status)
        statusName = try statusContainer.decode(String.self, forKey: .name)
    }
    
    init(id: String, createdTime: String, statusName: String) {
        self.id = id
        self.createdTime = createdTime
        self.statusName = statusName
    }
}

struct OrderDetail: Codable {
    let id: String
    let orderNumber: String
    let createdTime: String
    let amount: Double
    let note: String
    let address: String
    let isPayed: Bool
    let userId: String
    let status: Status?
    let orderItems: [OrderItem]?

    struct Status: Codable {
        let id: String
        let name: String
    }

    struct OrderItem: Codable {
        let id: String
        let count: Int
        let dish: Dish

        struct Dish: Codable {
            let id: String
            let name: String
            let description: String
            let price: Double
            let imageUrl: String
            let category: Category

            struct Category: Codable {
                let id: String
                let name: String
            }
        }
    }
}


class OrderService {
    static let shared = OrderService()
    private init() {}

    func createOrder(for userId: String, with request: OrderRequest, completion: @escaping (Result<Void, Error>) -> Void) {
        guard let token = TokenManager.shared.loadToken(key: "accessToken") else {
            print("No access token found")
            completion(.failure(NSError(domain: "No access token", code: 401)))
            return
        }

        guard let url = URL(string: "http://localhost:8081/api/order/create-by-user-id/\(userId)") else {
            print("Invalid URL")
            completion(.failure(NSError(domain: "Invalid URL", code: 400)))
            return
        }

        var httpRequest = URLRequest(url: url)
        httpRequest.httpMethod = "POST"
        httpRequest.setValue("Bearer \(token)", forHTTPHeaderField: "Authorization")
        httpRequest.setValue("application/json", forHTTPHeaderField: "Content-Type")

        do {
            httpRequest.httpBody = try JSONEncoder().encode(request)
        } catch {
            print("Failed to encode request body: \(error.localizedDescription)")
            completion(.failure(error))
            return
        }

        URLSession.shared.dataTask(with: httpRequest) { data, response, error in
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
                let errorMessage = "Server returned status code \(httpResponse.statusCode)"
                print(errorMessage)
                completion(.failure(NSError(domain: errorMessage, code: httpResponse.statusCode)))
            }
        }.resume()
    }
    
    func getLastOrder(for userId: String, completion: @escaping (Result<String, Error>) -> Void) {
        guard let token = TokenManager.shared.loadToken(key: "accessToken") else {
            print("No access token found")
            completion(.failure(NSError(domain: "No access token", code: 401)))
            return
        }

        guard let url = URL(string: "http://localhost:8081/api/order/get-by-user/\(userId)") else {
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
                print("Server returned status code \(httpResponse.statusCode)")
                completion(.failure(NSError(domain: "Server error", code: httpResponse.statusCode)))
                return
            }

            do {
                let orders = try JSONDecoder().decode([OrderResponse].self, from: data)
                guard let lastOrder = orders.sorted(by: { $0.createdTime > $1.createdTime }).first else {
                    print("No orders found")
                    completion(.failure(NSError(domain: "No orders found", code: 404)))
                    return
                }
                completion(.success(lastOrder.id))
            } catch {
                print("Failed to decode response: \(error.localizedDescription)")
                completion(.failure(error))
            }
        }.resume()
    }
    
    func getOrders(for userId: String, completion: @escaping (Result<[OrderSummary], Error>) -> Void) {
        guard let token = TokenManager.shared.loadToken(key: "accessToken") else {
            print("No access token found")
            completion(.failure(NSError(domain: "No access token", code: 401)))
            return
        }

        guard let url = URL(string: "http://localhost:8081/api/order/get-by-user/\(userId)") else {
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
                print("Server returned status code \(httpResponse.statusCode)")
                completion(.failure(NSError(domain: "Server error", code: httpResponse.statusCode)))
                return
            }

            do {
                let orders = try JSONDecoder().decode([OrderSummary].self, from: data)
                completion(.success(orders))
            } catch {
                print("Failed to decode response: \(error.localizedDescription)")
                completion(.failure(error))
            }
        }.resume()
    }
    
    func getOrderById(orderId: String, completion: @escaping (Result<OrderDetail, Error>) -> Void) {
        guard let token = TokenManager.shared.loadToken(key: "accessToken") else {
            print("No access token found")
            completion(.failure(NSError(domain: "No access token", code: 401)))
            return
        }

        guard let url = URL(string: "http://localhost:8081/api/order/get-by-id/\(orderId)") else {
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
                print("Server returned status code \(httpResponse.statusCode)")
                completion(.failure(NSError(domain: "Server error", code: httpResponse.statusCode)))
                return
            }

            do {
                let orderDetail = try JSONDecoder().decode(OrderDetail.self, from: data)
                completion(.success(orderDetail))
            } catch {
                print("Failed to decode response: \(error.localizedDescription)")
                completion(.failure(error))
            }
        }.resume()
    }
}
