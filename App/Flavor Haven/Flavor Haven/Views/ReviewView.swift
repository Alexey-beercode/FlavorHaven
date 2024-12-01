import SwiftUI

struct ReviewView: View {
    let orderId: String
    @Environment(\.dismiss) var dismiss
    @StateObject var viewModel = ReviewViewModel()

    var body: some View {
        VStack {
            Text("Оставить отзыв для заказа")
                .font(.title2)
                .bold()
                .padding()

            TextField("Введите ваш отзыв...", text: $viewModel.reviewText)
                .textFieldStyle(RoundedBorderTextFieldStyle())
                .padding()

            Button(action: {
                viewModel.submitReview(orderId: orderId) { success in
                    if success {
                        dismiss()
                    } else {
                        dismiss()
                    }
                }
            }) {
                Text("Отправить отзыв")
                    .frame(maxWidth: .infinity)
                    .padding()
                    .background(viewModel.reviewText.isEmpty ? Color.gray : Color.orange)
                    .foregroundColor(.white)
                    .cornerRadius(10)
                    .padding(.horizontal)
            }
            .disabled(viewModel.reviewText.isEmpty)
        }
        .padding()
    }
}
