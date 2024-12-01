import SwiftUI

struct MainView: View {
    @ObservedObject var viewModel = MainViewModel()
    @State private var isUserProfilePresented = false
    @State private var isCartPresented = false
    @State private var sortOrder: SortOrder = .ascending
    @State private var scrollCategoryOffset: String = ""

    var body: some View {
        NavigationView {
            VStack(spacing: 0) {
                HStack {
                    Button(action: {
                        isUserProfilePresented = true
                    }) {
                        HStack {
                            Text(viewModel.userName.count > 10 ? "\(viewModel.userName.prefix(10))..." : viewModel.userName)
                                .font(.headline)
                                .foregroundColor(.primary)
                        }
                    }
                    .sheet(isPresented: $isUserProfilePresented) {
                        if let userId = UserSession.shared.userId, !userId.isEmpty {
                            UserProfileView()
                        } else {
                            LoginView()
                        }
                    }

                    Spacer()

                    Button(action: {
                        isCartPresented = true
                    }) {
                        Image(systemName: "cart.fill")
                            .font(.title2)
                            .foregroundColor(.orange)
                    }
                    .sheet(isPresented: $isCartPresented) {
                        CartView()
                    }
                }
                .overlay(
                    Text("Flavor Haven")
                        .font(.system(size: 16))
                        .fontWeight(.bold)
                        .foregroundColor(.primary)
                        .frame(maxWidth: .infinity, alignment: .center)
                )
                .padding(.horizontal)
                .padding(.top, 0)
                .padding(.bottom, 4)

                HStack {
                    HStack {
                        Image(systemName: "magnifyingglass")
                            .foregroundColor(.gray)
                        TextField("Поиск блюд", text: $viewModel.searchText)
                            .textFieldStyle(PlainTextFieldStyle())
                    }
                    .padding()
                    .background(Color.gray.opacity(0.1))
                    .frame(height: 36)
                    .cornerRadius(12)

                    Button(action: {
                        sortOrder = (sortOrder == .ascending) ? .descending : .ascending
                        viewModel.sortDishes(by: sortOrder)
                    }) {
                        HStack(spacing: 4) {
                            Text("Цена")
                                .font(.subheadline)
                                .foregroundColor(.primary)
                            Image(systemName: sortOrder == .ascending ? "arrow.up" : "arrow.down")
                                .foregroundColor(.orange)
                        }
                        .padding(.horizontal, 10)
                        .padding(.vertical, 8)
                        .background(Color.gray.opacity(0.1))
                        .cornerRadius(12)
                    }
                }
                .padding(.horizontal)
                .padding(.bottom, 8)
                .padding(.top, 5)

                if viewModel.filteredDishes.isEmpty {
                    CategoryPicker
                    VStack {
                        Spacer()
                        Image(systemName: "fork.knife")
                            .font(.system(size: 60))
                            .foregroundColor(.gray)
                        Text("К сожалению, блюда не найдены :(")
                            .font(.headline)
                            .foregroundColor(.gray)
                            .padding()
                        Text("Попробуйте выбрать другую категорию.")
                            .font(.subheadline)
                            .foregroundColor(.gray)
                            .multilineTextAlignment(.center)
                            .padding(.horizontal)
                        Spacer()
                    }
                } else {
                    ScrollView {
                        CategoryPicker
                            .padding(.bottom, 10)

                        LazyVGrid(
                            columns: [
                                GridItem(.adaptive(minimum: 150), spacing: 20)
                            ],
                            spacing: 20
                        ) {
                            ForEach(viewModel.filteredDishes, id: \.id) { dish in
                                NavigationLink(destination: DishDetailView(viewModel: DishDetailViewModel(dish: dish))) {
                                    VStack(spacing: 0) {
                                        DishCellView(dish: dish)
                                            .frame(maxWidth: .infinity)
                                            .aspectRatio(1.2, contentMode: .fit)
                                            .background(Color.white)
                                            .cornerRadius(12)
                                            .shadow(color: .gray.opacity(0.3), radius: 6, x: 2, y: 2)
                                    }
                                    .padding(.vertical, 5)
                                }
                            }
                        }
                        .padding()
                    }
                }
            }
            .background(Color(UIColor.systemGroupedBackground).edgesIgnoringSafeArea(.all))
            .navigationBarHidden(true)
        }
    }
}

extension MainView {
    var CategoryPicker: some View {
        ScrollViewReader { proxy in
            ScrollView(.horizontal, showsIndicators: false) {
                HStack(spacing: 15) {
                    ForEach(viewModel.categories) { category in
                        Text(category.name)
                            .id(category.id)
                            .padding(.horizontal, 12)
                            .padding(.vertical, 8)
                            .background(
                                category.id == viewModel.selectedCategory
                                    ? Color.orange
                                    : Color.gray.opacity(0.2)
                            )
                            .foregroundColor(
                                category.id == viewModel.selectedCategory ? .white : .primary
                            )
                            .cornerRadius(12)
                            .shadow(color: .gray.opacity(0.3), radius: 4, x: 2, y: 2)
                            .onTapGesture {
                                viewModel.selectCategory(category.id)
                                scrollCategoryOffset = category.id
                                DispatchQueue.main.asyncAfter(deadline: .now() + 0.1) {
                                    if !viewModel.filteredDishes.isEmpty {
                                        viewModel.sortDishes(by: sortOrder)
                                    }
                                }
                            }
                    }
                }
                .padding(.horizontal)
            }
            .onAppear {
                proxy.scrollTo(scrollCategoryOffset, anchor: .center)
            }
        }
    }
}


#Preview {
    MainView()
}
