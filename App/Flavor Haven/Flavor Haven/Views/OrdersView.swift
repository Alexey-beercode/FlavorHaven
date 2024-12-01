import SwiftUI

struct OrderID: Identifiable {
    let id: String

    init(_ id: String) {
        self.id = id
    }
}

struct OrdersView: View {
    @StateObject var viewModel = OrdersViewModel()
    @State private var selectedOrderId: OrderID?
    @Environment(\.dismiss) var dismiss

    var body: some View {
        NavigationView {
            VStack {
                if viewModel.isLoading {
                    ProgressView("Загрузка заказов...")
                        .padding()
                } else if let errorMessage = viewModel.errorMessage {
                    Text("Ошибка: \(errorMessage)")
                        .foregroundColor(.red)
                        .padding()
                } else {
                    List(viewModel.orders, id: \.id) { order in
                        VStack(alignment: .leading) {
                            Text("Дата: \(order.createdTime)")
                                .font(.headline)
                            Text("Статус: \(order.statusName)")
                                .foregroundColor(order.statusName == "Processing" ? .orange : .green)
                        }
                        .padding(.vertical, 5)
                        .onTapGesture {
                            selectedOrderId = OrderID(order.id)
                        }
                    }
                    .listStyle(PlainListStyle())
                }
            }
            .navigationTitle("Ваши заказы")
            .toolbar {
                ToolbarItem(placement: .navigationBarLeading) {
                    Button(action: {
                        dismiss() // Закрытие текущего экрана
                    }) {
                        HStack {
                            Image(systemName: "chevron.backward")
                            Text("Назад")
                        }
                    }
                }
            }
            .onAppear {
                viewModel.fetchOrders()
            }
            .fullScreenCover(item: $selectedOrderId) { orderId in
                OrderDetailView(orderId: orderId.id)
            }
        }
    }
}
