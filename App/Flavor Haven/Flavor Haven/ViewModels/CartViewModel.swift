import Foundation

class CartViewModel: ObservableObject {
    @Published var cartItems: [CartItem] = []
    @Published var isLoading = false
    @Published var errorMessage: String?

    func totalPrice() -> Double {
        cartItems.reduce(0) { $0 + ($1.dish.price * Double($1.count)) }
    }

    func increaseItemCount(for cartItem: CartItem) {
        if let index = cartItems.firstIndex(where: { $0.id == cartItem.id }) {
            let updatedItem = CartItem(
                id: cartItems[index].id,
                count: cartItems[index].count + 1,
                dish: cartItems[index].dish
            )
            cartItems[index] = updatedItem
            
            addCartItem(updatedItem, 1)
        }
    }

    func decreaseItemCount(for cartItem: CartItem) {
        if let index = cartItems.firstIndex(where: { $0.id == cartItem.id }) {
            if cartItems[index].count > 1 {
                let updatedItem = CartItem(
                    id: cartItems[index].id,
                    count: cartItems[index].count - 1,
                    dish: cartItems[index].dish
                )
                cartItems[index] = updatedItem
                
                deleteCartItem(updatedItem, 1)
            }
        }
    }

    func removeItem(_ cartItem: CartItem) {
        if let index = cartItems.firstIndex(where: { $0.id == cartItem.id }) {
            cartItems.remove(at: index)
            deleteCartItem(cartItem, cartItem.count)
        }
    }
    
    private func addCartItem(_ cartItem: CartItem, _ count: Int) {
        CartService.shared.addCartItem(userId: UserSession.shared.userId!, dishId: cartItem.dish.id, count: count) { result in
            switch result {
            case .success:
                print("Item successfully added to the cart")
            case .failure(let error):
                print("Failed to add item: \(error.localizedDescription)")
            }
        }

    }

    private func deleteCartItem(_ cartItem: CartItem, _ count: Int) {
        CartService.shared.deleteCartItem(userId: UserSession.shared.userId!, dishId: cartItem.dish.id, count: count) { result in
            switch result {
            case .success:
                print("Item successfully deleted")
            case .failure(let error):
                print("Failed to delete item: \(error.localizedDescription)")
            }
        }
    }

    func placeOrder() {
        print("Заказ оформлен на сумму: \(totalPrice()) ₽")
    }
}
