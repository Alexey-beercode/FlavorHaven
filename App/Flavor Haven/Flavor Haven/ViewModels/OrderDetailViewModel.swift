import SwiftUI

class OrderDetailViewModel: ObservableObject {
    @Published var order: OrderDetail?
    @Published var isLoading = false
    @Published var errorMessage: String?
    @Published var reviewText: String?

    func fetchOrderDetail(orderId: String) {
        isLoading = true
        errorMessage = nil

        OrderService.shared.getOrderById(orderId: orderId) { [weak self] result in
            DispatchQueue.main.async {
                self?.isLoading = false
                switch result {
                case .success(let order):
                    self?.order = order
                case .failure(let error):
                    self?.errorMessage = error.localizedDescription
                }
            }
        }
    }

    func fetchReview(orderId: String) {
        ReviewService.shared.getReviewByOrderId(orderId: orderId) { [weak self] result in
            DispatchQueue.main.async {
                switch result {
                case .success(let note):
                    self?.reviewText = note
                case .failure(let error):
                    print("Ошибка загрузки отзыва: \(error.localizedDescription)")
                }
            }
        }
    }
}
