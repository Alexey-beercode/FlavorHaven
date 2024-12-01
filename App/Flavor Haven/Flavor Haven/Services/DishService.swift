import Foundation

struct DishRequest: Codable {
    let categoryId: String?
    let searchName: String
    let sorting: String
    let pageNumber: Int
    let pageSize: Int
}

struct DishResponse: Codable {
    let collection: [Dish]
    let currentPage: Int
    let pageSize: Int
    let totalPageCount: Int
    let totalItemCount: Int
}

struct ValidationError: Codable {
    let title: String
    let status: Int
    let errors: [String: [String]]

    var description: String {
        errors.map { "\($0.key): \($0.value.joined(separator: ", "))" }.joined(separator: "\n")
    }
    
    func classify() -> RegisterErrorType {
        if errors.keys.contains("Password") {
            return .invalidPassword
        }
        if errors.keys.contains("Email") {
            return .invalidEmail
        }

        return .unknown
    }

}

class DishService {
    static let shared = DishService()
    private init() {}

    func fetchDishes(
        categoryId: String?,
        searchName: String = "",
        sorting: String = "None",
        pageNumber: Int = 1,
        pageSize: Int = 10,
        completion: @escaping (Result<[Dish], Error>) -> Void
    ) {
        guard let url = URL(string: "http://localhost:8081/api/dish/get-by-parameters") else {
            print("Invalid URL")
            completion(.failure(NSError(domain: "Invalid URL", code: 400)))
            return
        }

        var request = URLRequest(url: url)
        request.httpMethod = "POST"
        request.setValue("application/json", forHTTPHeaderField: "Content-Type")

        let requestBody = DishRequest(
            categoryId: categoryId?.isEmpty == true ? nil : categoryId,
            searchName: searchName,
            sorting: sorting,
            pageNumber: max(pageNumber, 1),
            pageSize: max(pageSize, 1)
        )

        do {
            request.httpBody = try JSONEncoder().encode(requestBody)
        } catch {
            print("Failed to encode request: \(error.localizedDescription)")
            completion(.failure(error))
            return
        }

        let task = URLSession.shared.dataTask(with: request) { data, response, error in
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
                let dishResponse = try JSONDecoder().decode(DishResponse.self, from: data)
                completion(.success(dishResponse.collection))
            } catch {
                print("Failed to decode response: \(error.localizedDescription)")
                completion(.failure(error))
            }
        }
        task.resume()
    }
}
