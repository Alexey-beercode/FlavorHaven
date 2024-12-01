import Foundation
import Combine

struct UserResponse: Codable {
    let id: String
    let userName: String
    let email: String
    let balance: Int
}

class UserService {
    static let shared = UserService()
    private init() {}

    func fetchUser(by id: String, completion: @escaping (Result<UserResponse, Error>) -> Void) {
        guard let token = TokenManager.shared.loadToken(key: "accessToken") else {
            print("No access token found")
            completion(.failure(NSError(domain: "No access token", code: 401)))
            return
        }

        guard let url = URL(string: "http://localhost:8081/api/user/get-by-id/\(id)") else {
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

            do {
                let userResponse = try JSONDecoder().decode(UserResponse.self, from: data)
                completion(.success(userResponse))
            } catch {
                print("Failed to decode response: \(error.localizedDescription)")
                completion(.failure(error))
            }
        }.resume()
    }
}
