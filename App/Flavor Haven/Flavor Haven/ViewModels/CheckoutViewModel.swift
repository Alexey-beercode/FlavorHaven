import Combine
import Foundation

class CheckoutViewModel: ObservableObject {
    @Published var order: Order
    @Published var isOrderPlaced: Bool = false
    @Published var balance: String = "0"
    
    init(order: Order) {
        self.order = order
        getBalance()
    }
    
    func submitOrder() {
        let orderRequest = OrderRequest(address: order.address, note: order.comment)

        OrderService.shared.createOrder(for: UserSession.shared.userId!, with: orderRequest) { result in
            switch result {
            case .success:
                self.isOrderPlaced = true
                self.payOrder()
            case .failure(let error):
                print("Failed to create order: \(error.localizedDescription)")
            }
        }
    }
    
    func getBalance() {
        UserService.shared.fetchUser(by: UserSession.shared.userId!) { [weak self] result in
            switch result {
            case .success(let user):
                print("User fetched successfully: \(user)")
                DispatchQueue.main.async {
                    self?.balance = String(user.balance)
                }
            case .failure(let error):
                print("Failed to fetch user: \(error.localizedDescription)")
            }
        }
    }
    
    func payOrder() {
        OrderService.shared.getLastOrder(for: UserSession.shared.userId!) { result in
            switch result {
            case .success(let orderId):
                let paymentRequest = PaymentRequest(orderId: orderId)
                
                PaymentService.shared.createPayment(for: paymentRequest) { result in
                    switch result {
                    case .success:
                        print("Payment created successfully!")
                    case .failure(let error):
                        print("Failed to create payment: \(error.localizedDescription)")
                    }
                }
                print("Last order ID: \(orderId)")
            case .failure(let error):
                print("Failed to fetch last order: \(error.localizedDescription)")
            }
        }
    }
    
    func cancelOrder() {
        print("Заказ отменён")
    }
}
