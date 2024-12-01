import SwiftUI

struct DishCellView: View {
    let dish: Dish

    var body: some View {
        VStack(spacing: 8) {
            AsyncImage(url: URL(string: dish.imageUrl)) { image in
                image
                    .resizable()
                    .scaledToFill()
            } placeholder: {
                ProgressView()
            }
            .frame(width: 100, height: 100)
            .clipShape(RoundedRectangle(cornerRadius: 10))
            .shadow(radius: 3)

            Text(dish.name)
                .font(.headline)
                .lineLimit(1)
                .foregroundColor(.orange)

            Text("\(dish.price, specifier: "%.2f") BYN")
                .font(.subheadline)
                .foregroundColor(.gray)
        }
        .padding()
        .background(Color.white)
    }
}
