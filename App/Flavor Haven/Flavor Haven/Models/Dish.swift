import Foundation

struct Dish: Identifiable, Codable {
    let id: String
    let name: String
    let description: String
    let price: Double
    let imageUrl: String
    let category: Category?
}
