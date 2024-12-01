import Foundation

struct CartItem: Identifiable, Codable {
    let id: String
    let count: Int
    let dish: Dish
}
