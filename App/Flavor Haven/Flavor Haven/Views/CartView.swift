import SwiftUI

struct CartView: View {
    @StateObject private var viewModel = CartViewModel()
    @State private var showConfirmation = false

    var body: some View {
        VStack {
            if viewModel.isLoading {
                ProgressView("Загрузка...")
                    .progressViewStyle(CircularProgressViewStyle())
                    .padding()
            } else if let errorMessage = viewModel.errorMessage {
                Text("Ваша корзина пуста")
                    .font(.title)
                    .foregroundColor(.gray)
                    .padding()
            } else if viewModel.cartItems.isEmpty {
                Text("Ваша корзина пуста")
                    .font(.title)
                    .foregroundColor(.gray)
                    .padding()
            } else {
                List {
                    ForEach(viewModel.cartItems) { cartItem in
                        HStack(alignment: .top) {
                            AsyncImage(url: URL(string: cartItem.dish.imageUrl)) { image in
                                image
                                    .resizable()
                                    .scaledToFit()
                                    .frame(width: 60, height: 60)
                                    .cornerRadius(8)
                                    .shadow(radius: 2)
                            } placeholder: {
                                ProgressView()
                            }
                            
                            VStack(alignment: .leading, spacing: 4) {
                                Text(cartItem.dish.name)
                                    .font(.headline)

                                Text(cartItem.dish.description)
                                    .font(.subheadline)
                                    .foregroundColor(.gray)
                                    .lineLimit(3) // Ограничение описания в 3 строки
                                    .truncationMode(.tail) // Добавляет "..." в конце, если текст не помещается
                            }
                            
                            Spacer()
                            
                            VStack(alignment: .center, spacing: 6) {
                                Spacer()
                                
                                Text("\(cartItem.dish.price * Double(cartItem.count), specifier: "%.2f") ₽")
                                    .font(.subheadline)
                                    .foregroundColor(.orange)

                                HStack(spacing: 8) {
                                    Button(action: {
                                        viewModel.decreaseItemCount(for: cartItem)
                                    }) {
                                        Image(systemName: "minus")
                                            .resizable()
                                            .scaledToFit()
                                            .frame(width: 8, height: 8)
                                            .padding(10)
                                            .background(Color.orange.opacity(0.8))
                                            .foregroundColor(.white)
                                            .clipShape(Circle())
                                    }
                                    .buttonStyle(BorderlessButtonStyle())
                                    
                                    Text("\(cartItem.count)")
                                        .font(.subheadline)

                                    Button(action: {
                                        viewModel.increaseItemCount(for: cartItem)
                                    }) {
                                        Image(systemName: "plus")
                                            .resizable()
                                            .scaledToFit()
                                            .frame(width: 8, height: 8)
                                            .padding(10)
                                            .background(Color.orange.opacity(0.8))
                                            .foregroundColor(.white)
                                            .clipShape(Circle())
                                    }
                                    .buttonStyle(BorderlessButtonStyle())
                                }
                                
                                Spacer()
                            }
                            
                            VStack {
                                Spacer()
                                
                                Button(action: {
                                    viewModel.removeItem(cartItem)
                                }) {
                                    Image(systemName: "trash")
                                        .foregroundColor(.red)
                                }
                                .buttonStyle(BorderlessButtonStyle())
                                
                                Spacer()
                            }
                        }
                        .padding(.vertical, 8)
                    }
                }
                
                HStack {
                    Text("Итого:")
                        .font(.headline)
                    Spacer()
                    Text("\(viewModel.totalPrice(), specifier: "%.2f") ₽")
                        .font(.headline)
                        .foregroundColor(.orange)
                }
                .padding(.horizontal)
                
                Button(action: {
                    viewModel.placeOrder()
                    showConfirmation = true
                }) {
                    Text("Оформить заказ")
                        .frame(maxWidth: .infinity)
                        .padding()
                        .background(Color.orange)
                        .foregroundColor(.white)
                        .cornerRadius(10)
                }
                .padding(.horizontal)
                .padding(.top)
            }
        }
        .onAppear {
            guard let userId = UserSession.shared.userId else {
                return
            }
            CartService.shared.fetchCart(userId: userId) { result in
                DispatchQueue.main.async {
                    switch result {
                    case .success(let cartItems):
                        viewModel.cartItems = cartItems
                    case .failure(let error):
                        viewModel.errorMessage = error.localizedDescription
                    }
                }
            }
        }
        .fullScreenCover(isPresented: $showConfirmation) {
            let order = Order(amount: viewModel.totalPrice(), address: "", comment: "")
            let viewOrderModel = CheckoutViewModel(order: order)
            CheckoutView(viewModel: viewOrderModel)
        }
    }
}
