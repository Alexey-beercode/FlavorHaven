import Foundation

struct PaymentRequest: Codable {
    let orderId: String
}

class PaymentService {
    static let shared = PaymentService()
    private init() {}

    func createPayment(for request: PaymentRequest, completion: @escaping (Result<Void, Error>) -> Void) {
        guard let token = TokenManager.shared.loadToken(key: "accessToken") else {
            print("No access token found")
            completion(.failure(NSError(domain: "No access token", code: 401)))
            return
        }

        guard let url = URL(string: "http://localhost:8081/api/payment/create") else {
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
}
