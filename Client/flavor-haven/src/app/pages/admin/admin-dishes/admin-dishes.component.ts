import { Component, OnInit } from '@angular/core';
import { DishService } from '../../../services/dish.service';
import { DishCategoryService } from '../../../services/dish-category.service';
import { DishDTO } from '../../../models/dtos/dish/dish.dto';
import { DishCategoryDTO } from '../../../models/dtos/dish-category/dish-category.dto';
import { PaginatedResult } from '../../../models/dtos/common/paginated-result.dto';
import { CommonModule } from '@angular/common';
import { ErrorMessageComponent } from '../../../components/error-message/error-message.component';
import { DishCreateModalComponent } from './dish-create-modal/dish-create-modal.component';
import { DishUpdateModalComponent } from './dish-update-modal/dish-update-modal.component';

@Component({
  selector: 'app-admin-dishes',
  templateUrl: './admin-dishes.component.html',
  styleUrls: ['./admin-dishes.component.css'],
  standalone: true,
  imports: [
    CommonModule,
    ErrorMessageComponent,
    DishCreateModalComponent,
    DishUpdateModalComponent,
  ],
})
export class AdminDishesComponent implements OnInit {
  dishes: DishDTO[] = [];
  categories: DishCategoryDTO[] = [];
  isLoading: boolean = false;
  error: string | null = null;

  isCreateModalVisible: boolean = false;
  selectedDishForUpdate: DishDTO | null = null;

  constructor(
    private dishService: DishService,
    private categoryService: DishCategoryService
  ) {}

  ngOnInit(): void {
    this.loadDishesAndCategories();
  }

  // Загрузка блюд и категорий
  loadDishesAndCategories(): void {
    this.isLoading = true;
    this.error = null;

    Promise.all([
      this.dishService.getDishes({}).toPromise() as Promise<PaginatedResult<DishDTO>>,
      this.categoryService.getAllCategories().toPromise(),
    ])
      .then(([dishResult, categories]) => {
        this.dishes = dishResult?.collection || [];
        this.categories = categories || [];
        this.isLoading = false;
      })
      .catch((err) => {
        console.error('Ошибка при загрузке данных:', err);
        this.error = 'Не удалось загрузить блюда и категории.';
        this.isLoading = false;
      });
  }

  // Удаление блюда
  deleteDish(dishId: string): void {
    this.dishService.deleteDish(dishId).subscribe({
      next: () => this.loadDishesAndCategories(),
      error: (err) => {
        console.error('Ошибка при удалении блюда:', err);
        this.error = 'Не удалось удалить блюдо.';
      },
    });
  }

  // Открытие модалки создания
  openCreateModal(): void {
    this.isCreateModalVisible = true;
  }

  // Закрытие модалки создания
  closeCreateModal(): void {
    this.isCreateModalVisible = false;
    this.loadDishesAndCategories();
  }

  // Открытие модалки обновления
  openUpdateModal(dish: DishDTO): void {
    this.selectedDishForUpdate = dish;
  }

  // Закрытие модалки обновления
  closeUpdateModal(): void {
    this.selectedDishForUpdate = null;
    this.loadDishesAndCategories();
  }
}
