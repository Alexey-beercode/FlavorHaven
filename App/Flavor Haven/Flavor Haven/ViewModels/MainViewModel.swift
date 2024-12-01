import Foundation
import Combine

enum SortOrder {
    case ascending
    case descending
}

class MainViewModel: ObservableObject {
    @Published var userName: String = "Войти"
    @Published var searchText: String = "" {
        didSet {
            searchDishes()
        }
    }
    @Published var selectedCategory: String = "Все"
    @Published var categories: [Category] = []
    @Published var dishes: [Dish] = []
    @Published var filteredDishes: [Dish] = []
    @Published var sortOrder: SortOrder = .ascending

    init() {
        loadCategories() {
            self.loadDishes()
        }
        
        UserSession.shared.$userName
            .map { $0 ?? "Войти" }
            .assign(to: &$userName)
    }
    
    func loadCategories(completion: @escaping () -> Void = {}) {
        CategoryService.shared.fetchCategories { result in
            DispatchQueue.main.async {
                switch result {
                case .success(let fetchedCategories):
                    self.categories = [Category(id: "all", name: "Все")] + fetchedCategories
                    if let firstCategory = self.categories.first {
                        self.selectedCategory = firstCategory.id
                    }
                    completion()
                case .failure(let error):
                    print("Ошибка загрузки категорий: \(error.localizedDescription)")
                    completion()
                }
            }
        }
    }


    func filterDishes() {
        filteredDishes = dishes.filter {
            (selectedCategory == "all" || $0.category!.id == selectedCategory) &&
            (searchText.isEmpty || $0.name.lowercased().contains(searchText.lowercased()))
        }
    }
    
    func loadDishes() {
        DishService.shared.fetchDishes(
            categoryId: selectedCategory == "all" ? nil : selectedCategory,
            searchName: searchText,
            sorting: "None",
            pageNumber: 1,
            pageSize: 10
        ) { result in
            DispatchQueue.main.async {
                switch result {
                case .success(let fetchedDishes):
                    self.dishes = fetchedDishes
                    self.filterDishes()
                case .failure(let error):
                    print("Ошибка загрузки блюд: \(error.localizedDescription)")
                }
            }
        }
    }
    
    func searchDishes() {
        loadDishes()
    }



    func selectCategory(_ categoryId: String) {
        selectedCategory = categoryId
        loadDishes()
    }

    func openCart() {
        CartView()
    }
    
    private func filterAndSortDishes() {

    }
    
    func sortDishes(by order: SortOrder) {
        self.sortOrder = order
        filteredDishes.sort {
            if order == .ascending {
                return $0.price < $1.price
            } else {
                return $0.price > $1.price
            }
        }
    }
}
