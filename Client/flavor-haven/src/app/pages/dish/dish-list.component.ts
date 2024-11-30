import { Component, OnInit, Input, OnChanges, SimpleChanges } from '@angular/core';
import { DishService } from '../../services/dish.service';
import { TokenService } from '../../services/token.service';
import { DishDTO } from '../../models/dtos/dish/dish.dto';
import { GetDishesRequestDTO } from '../../models/dtos/dish/get-dishes-request.dto';
import { PaginatedResult } from '../../models/dtos/common/paginated-result.dto';
import { SortingParameters } from '../../models/dtos/dish/sorting-parameters.dto';
import { CurrencyPipe, CommonModule } from '@angular/common';
import { AddToCartComponent } from './add-to-cart/add-to-cart.component';
import { DishDetailsComponent } from './dish-details/dish-details.component'; // Импортируем компонент модалки

@Component({
  selector: 'app-dish-list',
  templateUrl: './dish-list.component.html',
  styleUrls: ['./dish-list.component.css'],
  standalone: true,
  imports: [CurrencyPipe, AddToCartComponent, DishDetailsComponent, CommonModule],
})
export class DishListComponent implements OnInit, OnChanges {
  @Input() categoryId!: string | undefined;
  @Input() searchName!: string | undefined;
  @Input() sorting!: SortingParameters;

  userId: string = ''; // Используем строку по умолчанию вместо null
  dishes: DishDTO[] = [];
  currentPage = 1;
  totalPageCount = 0;

  selectedDish: DishDTO | null = null; // Для хранения выбранного блюда

  constructor(
    private dishService: DishService,
    private tokenService: TokenService
  ) {}

  ngOnInit(): void {
    this.loadUserId(); // Загружаем userId при инициализации
    this.loadDishes();
  }

  ngOnChanges(changes: SimpleChanges): void {
    if ('categoryId' in changes || 'searchName' in changes || 'sorting' in changes) {
      this.currentPage = 1; // Сбросить на первую страницу при изменении фильтров
      this.loadDishes();
    }
  }

  // Получаем userId из TokenService
  private loadUserId(): void {
    const userId = this.tokenService.getUserIdFromToken();
    this.userId = userId || ''; // Если userId == null, заменяем на пустую строку
    if (!userId) {
      console.warn('Пользователь не авторизован: userId не найден.');
    }
  }

  loadDishes(): void {
    const request: GetDishesRequestDTO = {
      searchName: this.searchName,
      sorting: this.sorting,
      pageNumber: this.currentPage,
      pageSize: 10,
    };

    if (this.categoryId) {
      request.categoryId = this.categoryId;
    }

    this.dishService.getDishes(request).subscribe({
      next: (result: PaginatedResult<DishDTO>) => {
        this.dishes = result.collection;
        this.currentPage = result.currentPage;
        this.totalPageCount = result.totalPageCount;
      },
      error: (err) => {
        console.error(err);
      },
    });
  }

  nextPage(): void {
    if (this.currentPage < this.totalPageCount) {
      this.currentPage++;
      this.loadDishes();
    }
  }

  prevPage(): void {
    if (this.currentPage > 1) {
      this.currentPage--;
      this.loadDishes();
    }
  }

  // Открытие модалки
  openDishDetails(dish: DishDTO): void {
    console.log('Dish selected:', dish);
    this.selectedDish = dish;
  }

  // Закрытие модалки
  closeDishDetails(): void {
    this.selectedDish = null;
  }
}
