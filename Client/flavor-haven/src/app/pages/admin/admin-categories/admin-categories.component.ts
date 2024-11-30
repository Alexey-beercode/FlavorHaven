import { Component, OnInit } from '@angular/core';
import { DishCategoryService } from '../../../services/dish-category.service';
import { DishCategoryDTO } from '../../../models/dtos/dish-category/dish-category.dto';
import {DishCategoryAddModalComponent} from './dish-category-add-modal/dish-category-add-modal.component';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-admin-categories',
  templateUrl: './admin-categories.component.html',
  styleUrls: ['./admin-categories.component.css'],
  standalone: true,
  imports: [
    DishCategoryAddModalComponent,CommonModule
  ]
})
export class AdminCategoriesComponent implements OnInit {
  categories: DishCategoryDTO[] = [];
  isLoading: boolean = false;
  error: string | null = null;
  isAddModalVisible: boolean = false;

  constructor(private categoryService: DishCategoryService) {}

  ngOnInit(): void {
    this.loadCategories();
  }

  // Загрузка всех категорий
  loadCategories(): void {
    this.isLoading = true;
    this.categoryService.getAllCategories().subscribe({
      next: (data) => {
        this.categories = data;
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Ошибка при загрузке категорий:', err);
        this.error = 'Не удалось загрузить категории.';
        this.isLoading = false;
      },
    });
  }

  // Удаление категории
  deleteCategory(id: string): void {
      this.categoryService.deleteCategory(id).subscribe({
        next: () => {
          this.categories = this.categories.filter((cat) => cat.id !== id);
        },
        error: (err) => {
          console.error('Ошибка удаления категории:', err);
          this.error = 'Не удалось удалить категорию.';
        },
      });
  }

  // Открытие модалки добавления
  openAddModal(): void {
    this.isAddModalVisible = true;
  }

  // Закрытие модалки добавления
  closeAddModal(): void {
    this.isAddModalVisible = false;
  }

  // Обработка добавления новой категории
  handleCategoryAdded(newCategory: DishCategoryDTO): void {
    this.categories.push(newCategory);
    this.closeAddModal();
  }
}
