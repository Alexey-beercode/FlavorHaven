import Foundation
import SwiftUI
import Combine

class OrdersViewModel: ObservableObject {
    @Published var orders: [OrderSummary] = []
    @Published var errorMessage: String?
    @Published var isLoading: Bool = false

    func fetchOrders() {
        isLoading = true
        OrderService.shared.getOrders(for: UserSession.shared.userId!) { [weak self] result in
            DispatchQueue.main.async {
                self?.isLoading = false
                switch result {
                case .success(let fetchedOrders):
                    self?.orders = fetchedOrders
                case .failure(let error):
                    self?.errorMessage = error.localizedDescription
                }
            }
        }
    }
}
