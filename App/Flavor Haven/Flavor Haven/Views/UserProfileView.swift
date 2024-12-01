import SwiftUI

struct UserProfileView: View {
    @ObservedObject var viewModel = UserProfileModel()
    @Environment(\.dismiss) var dismiss
    @State private var isOrdersPresented = false

    var body: some View {
        VStack(spacing: 20) {
            Text("Профиль")
                .font(.largeTitle)
                .bold()
                .padding(.top)

            VStack(spacing: 15) {
                HStack {
                    Text("Имя:")
                        .foregroundColor(.gray)
                    Spacer()
                    Text(viewModel.userName)
                        .bold()
                }
                .padding()
                .background(Color.gray.opacity(0.1))
                .cornerRadius(10)

                HStack {
                    Text("Баланс:")
                        .foregroundColor(.gray)
                    Spacer()
                    Text("\(viewModel.balance) BYN")
                        .bold()
                }
                .padding()
                .background(Color.gray.opacity(0.1))
                .cornerRadius(10)

                Button(action: {}) {
                    Text("Пополнить баланс")
                        .frame(maxWidth: .infinity)
                        .padding()
                        .background(Color.orange)
                        .foregroundColor(.white)
                        .cornerRadius(10)
                }
            }
            .padding(.horizontal)

            VStack(spacing: 15) {
                HStack {
                    Text("Заказы:")
                        .foregroundColor(.gray)
                    Spacer()
                    Text("\(viewModel.ordersCount)")
                        .bold()
                        .foregroundColor(.blue)
                }
                .padding()
                .background(Color.gray.opacity(0.1))
                .cornerRadius(10)

                Button(action: {
                    isOrdersPresented = true
                }) {
                    Text("Посмотреть заказы")
                        .frame(maxWidth: .infinity)
                        .padding()
                        .background(Color.blue)
                        .foregroundColor(.white)
                        .cornerRadius(10)
                }
                .fullScreenCover(isPresented: $isOrdersPresented) {
                    OrdersView()
                }
            }
            .padding(.horizontal)

            Spacer()

            Button(action: {
                viewModel.logout()
                dismiss()
            }) {
                Text("Выйти")
                    .frame(maxWidth: .infinity)
                    .padding()
                    .background(Color.red)
                    .foregroundColor(.white)
                    .cornerRadius(10)
            }
            .padding(.horizontal)
        }
        .padding()
        .background(Color(UIColor.systemGroupedBackground).edgesIgnoringSafeArea(.all))
        .navigationTitle("Профиль")
        .navigationBarTitleDisplayMode(.inline)
    }
}
