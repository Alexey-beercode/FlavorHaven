import SwiftUI

struct OrderDetailView: View {
    @StateObject private var viewModel = OrderDetailViewModel()
    let orderId: String
    @Environment(\.dismiss) var dismiss
    @State private var isReviewPresented = false

    var body: some View {
        VStack {
            if viewModel.isLoading {
                ProgressView("Загрузка информации о заказе...")
                    .padding()
            } else if let errorMessage = viewModel.errorMessage {
                Text("Ошибка: \(errorMessage)")
                    .foregroundColor(.red)
                    .padding()
            } else if let order = viewModel.order {
                ScrollView {
                    VStack(alignment: .leading, spacing: 10) {
                        Text("Заказ № \(order.orderNumber)")
                            .font(.title2)
                            .bold()

                        Text("Дата: \(order.createdTime)")
                        Text("Сумма: \(order.amount, specifier: "%.2f") BYN")
                        Text("Адрес: \(order.address)")
                        Text("Примечание: \(order.note)")
                        Text("Оплачен: \(order.isPayed ? "Да" : "Нет")")
                        if let statusName = order.status?.name {
                            Text("Статус: \(statusName)")
                        } else {
                            Text("Статус: Нет статуса")
                        }
                    }
                    .padding()
                }
            } else {
                Text("Нет данных для отображения")
            }

            Spacer()

            VStack(spacing: 10) {
                if let reviewText = viewModel.reviewText {
                    Text("Ваш отзыв:")
                        .font(.headline)
                    Text(reviewText)
                        .font(.body)
                        .foregroundColor(.gray)
                        .padding()
                        .frame(maxWidth: .infinity)
                        .background(Color.blue.opacity(0.1))
                        .cornerRadius(10)
                } else {
                    Button(action: {
                        isReviewPresented = true
                    }) {
                        Text("Оставить отзыв")
                            .frame(maxWidth: .infinity)
                            .padding()
                            .background(Color.orange)
                            .foregroundColor(.white)
                            .cornerRadius(10)
                            .padding(.horizontal)
                    }
                    .sheet(isPresented: $isReviewPresented) {
                        ReviewView(orderId: orderId)
                    }
                }

                Button(action: {
                    dismiss()
                }) {
                    Text("Окей")
                        .frame(maxWidth: .infinity)
                        .padding()
                        .background(Color.blue)
                        .foregroundColor(.white)
                        .cornerRadius(10)
                        .padding(.horizontal)
                }
                .onChange(of: isReviewPresented) { newValue in
                    if !newValue {
                        viewModel.fetchOrderDetail(orderId: orderId)
                        viewModel.fetchReview(orderId: orderId)
                    }
                }
            }
            .padding(.bottom)
        }
        .onAppear {
            viewModel.fetchOrderDetail(orderId: orderId)
            viewModel.fetchReview(orderId: orderId)
        }
        .navigationTitle("Информация о заказе")
        .navigationBarTitleDisplayMode(.inline)
    }
}
