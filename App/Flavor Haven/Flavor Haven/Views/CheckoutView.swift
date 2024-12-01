import SwiftUI

struct CheckoutView: View {
    @StateObject var viewModel: CheckoutViewModel
    @Environment(\.dismiss) private var dismiss
    @State private var alertTitle = "Успешно!"
    @State private var alertMessage = "Ваш заказ оформлен."
    @State private var showAlert = false
    
    var body: some View {
        VStack(spacing: 20) {
            Text("С вашего баланса спишется \(viewModel.order.amount) BYN")
                .font(.headline)
                .padding()

            VStack(alignment: .leading, spacing: 10) {
                Text("Адрес доставки:")
                    .font(.subheadline)
                TextField("Введите адрес", text: $viewModel.order.address)
                    .textFieldStyle(RoundedBorderTextFieldStyle())
                    .padding(.horizontal)
            }

            VStack(alignment: .leading, spacing: 10) {
                Text("Комментарий:")
                    .font(.subheadline)
                TextField("Оставьте комментарий", text: $viewModel.order.comment)
                    .textFieldStyle(RoundedBorderTextFieldStyle())
                    .padding(.horizontal)
            }

            Spacer()

            HStack {
                Button(action: {
                    dismiss()
                }) {
                    Text("Назад")
                        .frame(maxWidth: .infinity)
                        .padding()
                        .background(Color.gray.opacity(0.2))
                        .cornerRadius(8)
                }
                .padding()

                Button(action: {
                    viewModel.getBalance()
                    
                    if let balance = Double(viewModel.balance) {
                        if viewModel.order.address.isEmpty {
                            alertTitle = "Внимание!"
                            alertMessage = "Пожалуйста, введите адрес."
                            showAlert = true
                        } else if balance < viewModel.order.amount {
                            alertTitle = "Внимание!"
                            alertMessage = "У вас недостаточно денег на балансе."
                            showAlert = true
                        } else {
                            viewModel.submitOrder()
                            alertTitle = "Успешно!"
                            alertMessage = "Ваш заказ оформлен."
                            showAlert = true
                            dismiss()
                        }
                    }
                }) {
                    Text("Оформить заказ")
                        .frame(maxWidth: .infinity)
                        .padding()
                        .background(Color.blue)
                        .foregroundColor(.white)
                        .cornerRadius(8)
                }
                .padding()
            }
        }
        .padding()
        .alert(isPresented: $showAlert) {
                Alert(
                    title: Text(alertTitle),
                    message: Text(alertMessage),
                    dismissButton: .default(Text("OK")) {
                        showAlert = false
                    }
                )
            }
    }
}

#Preview {
    let order = Order(amount: 100, address: "", comment: "")
    let viewModel = CheckoutViewModel(order: order)
    return CheckoutView(viewModel: viewModel)
}
