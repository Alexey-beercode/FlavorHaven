import Foundation

class UserSession: ObservableObject {
    static let shared = UserSession()
    private init() {}

    @Published var userId: String?
    @Published var userName: String?
    @Published var roles: [String] = []
}
