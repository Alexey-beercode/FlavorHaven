import Foundation

class UserProfileModel: ObservableObject {
    @Published var userName: String = "NULL"
    @Published var balance: String = "0"
    @Published var ordersCount: Int = 0
    @Published var orders: [OrderSummary] = []
    @Published var errorMessage: String?
    
    init() {
        UserSession.shared.$userName
            .map { $0 ?? "NULL" }
            .assign(to: &$userName)
        getUserInformation()
        getOrdersCount()
    }
    
    func getUserInformation() {
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
    
    func logout() {
        UserSession.shared.userId = nil
        UserSession.shared.userName = nil
        UserSession.shared.roles = []
        TokenManager.shared.deleteToken(key: "accessToken")
        TokenManager.shared.deleteToken(key: "refreshToken")
    }
    
    func getOrdersCount() {
        OrderService.shared.getOrders(for: UserSession.shared.userId!) { [weak self] result in
            DispatchQueue.main.async {
                switch result {
                case .success(let fetchedOrders):
                    let orderCount = fetchedOrders.count
                    print("Количество заказов: \(orderCount)")
                    self?.orders = fetchedOrders
                    self?.ordersCount = orderCount
                case .failure(let error):
                    print("Ошибка при получении заказов: \(error.localizedDescription)")
                    self?.errorMessage = error.localizedDescription
                    self?.ordersCount = 0
                }
            }
        }
    }
}
