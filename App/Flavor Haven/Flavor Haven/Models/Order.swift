import Foundation

struct Order: Identifiable {
    let id = UUID()
    var amount: Double
    var address: String
    var comment: String
}
