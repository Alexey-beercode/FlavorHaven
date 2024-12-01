import Foundation

class ReviewViewModel: ObservableObject {
    @Published var reviewText: String = ""
    @Published var isSubmitting: Bool = false
    @Published var errorMessage: String?

    func submitReview(orderId: String, completion: @escaping (Bool) -> Void) {
        guard !reviewText.isEmpty else {
            errorMessage = "Отзыв не может быть пустым"
            return
        }

        isSubmitting = true
        errorMessage = nil

        ReviewService.shared.createReview(orderId: orderId, note: reviewText) { result in
            DispatchQueue.main.async {
                self.isSubmitting = false
                switch result {
                case .success:
                    completion(true)
                case .failure(let error):
                    self.errorMessage = "Ошибка: \(error.localizedDescription)"
                    completion(false)
                }
            }
        }
    }
}
