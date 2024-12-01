import SwiftUI

class DishDetailViewModel: ObservableObject {
    @Published var quantity: Int = 1
    @Published var showNotification: Bool = false
    
    let dish: Dish
    private let cartService = CartService.shared
    @Environment(\.dismiss) var dismiss

    init(dish: Dish) {
        self.dish = dish
    }
    
    var totalPrice: Double {
        Double(quantity) * dish.price
    }
    
    func addToCart() {
        if UserSession.shared.userId != nil {
            cartService.addCartItem(userId: UserSession.shared.userId!, dishId: dish.id, count: quantity) { result in
                DispatchQueue.main.async {
                    switch result {
                    case .success:
                        self.dismissScreen()
                    case .failure(let error):
                        print("Failed to add item: \(error.localizedDescription)")
                    }
                }
            }
        } else {
            showNotification = true
            DispatchQueue.main.asyncAfter(deadline: .now() + 3) {
                self.showNotification = false
            }
        }
    }
    
    private func dismissScreen() {
        DispatchQueue.main.async {
            NotificationCenter.default.post(name: .didDismissDishDetail, object: nil)
        }
    }
}

extension Notification.Name {
    static let didDismissDishDetail = Notification.Name("didDismissDishDetail")
}
