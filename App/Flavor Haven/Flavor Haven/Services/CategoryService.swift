import Foundation

class CategoryService {
    static let shared = CategoryService()
    private init() {}

    func fetchCategories(completion: @escaping (Result<[Category], Error>) -> Void) {
        guard let url = URL(string: "http://localhost:8081/api/dish-category/get-all") else {
            completion(.failure(NSError(domain: "Invalid URL", code: 400, userInfo: nil)))
            return
        }

        let task = URLSession.shared.dataTask(with: url) { data, response, error in
            if let error = error {
                completion(.failure(error))
                return
            }

            guard let data = data else {
                completion(.failure(NSError(domain: "No data received", code: 500, userInfo: nil)))
                return
            }

            do {
                let categories = try JSONDecoder().decode([Category].self, from: data)
                completion(.success(categories))
            } catch {
                completion(.failure(error))
            }
        }
        task.resume()
    }
}
