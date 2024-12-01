import SwiftUI

struct DishDetailView: View {
    @ObservedObject var viewModel: DishDetailViewModel
    @Environment(\.dismiss) var dismiss

    var body: some View {
        ScrollView {
            ZStack {
                VStack(spacing: 20) {
                    AsyncImage(url: URL(string: viewModel.dish.imageUrl)) { image in
                        image
                            .resizable()
                            .scaledToFill()
                    } placeholder: {
                        Rectangle()
                            .fill(Color.gray.opacity(0.2))
                            .overlay(ProgressView())
                    }
                    .frame(height: 300)
                    .clipped()
                    .cornerRadius(15)
                    .shadow(radius: 5)
                    
                    VStack(alignment: .leading, spacing: 10) {
                        Text(viewModel.dish.name)
                            .font(.largeTitle)
                            .bold()
                            .foregroundColor(.orange)
                        
                        Text(viewModel.dish.description)
                            .font(.body)
                            .foregroundColor(.gray)
                            .lineLimit(nil)
                    }
                    .padding(.horizontal)
                    
                    HStack {
                        Stepper("Количество: \(viewModel.quantity)", value: $viewModel.quantity, in: 1...10)
                            .font(.headline)
                            .padding()
                            .background(Color.orange.opacity(0.2))
                            .cornerRadius(8)
                    }
                    .padding(.horizontal)
                    
                    HStack {
                        Text("\(viewModel.totalPrice, specifier: "%.2f") BYN")
                            .font(.title)
                            .bold()
                            .foregroundColor(.orange)
                        
                        Spacer()
                        
                        Button(action: {
                            viewModel.addToCart()
                        }) {
                            Text("Добавить в корзину")
                                .frame(maxWidth: .infinity)
                                .padding()
                                .background(Color.orange)
                                .foregroundColor(.white)
                                .font(.headline)
                                .cornerRadius(12)
                        }
                        .frame(width: 160)
                    }
                    .padding(.horizontal)
                    
                    Spacer(minLength: 20)
                }
                .padding(.vertical)
                
                if viewModel.showNotification {
                    VStack {
                        Text("Вы должны войти, чтобы добавлять заказы в корзину")
                            .font(.body)
                            .foregroundColor(.white)
                            .padding()
                            .background(Color.red)
                            .cornerRadius(10)
                            .shadow(radius: 10)
                    }
                    .frame(maxWidth: .infinity)
                    .padding()
                    .transition(.move(edge: .top).combined(with: .opacity))
                    .zIndex(1)
                }
            }
        }
        .background(Color(UIColor.systemGroupedBackground).edgesIgnoringSafeArea(.all))
        .navigationTitle("Детали блюда")
        .navigationBarTitleDisplayMode(.inline)
        .tint(.orange)
        .onReceive(NotificationCenter.default.publisher(for: .didDismissDishDetail)) { _ in
            dismiss()
        }
        .animation(.easeInOut, value: viewModel.showNotification)
    }
}
