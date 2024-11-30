import { Component, Output, EventEmitter } from '@angular/core';
import { DishCategoryService } from '../../../../services/dish-category.service';
import { DishCategoryRequestDTO } from '../../../../models/dtos/dish-category/dish-category-request.dto';
import { DishCategoryDTO } from '../../../../models/dtos/dish-category/dish-category.dto';
import {FormsModule} from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-dish-category-add-modal',
  templateUrl: './dish-category-add-modal.component.html',
  styleUrls: ['./dish-category-add-modal.component.css'],
  standalone: true,
  imports: [
    FormsModule,CommonModule
  ]
})
export class DishCategoryAddModalComponent {
  name: string = '';
  isLoading: boolean = false;
  error: string | null = null;

  @Output() categoryAdded = new EventEmitter<DishCategoryDTO>();
  @Output() close = new EventEmitter<void>();

  constructor(private categoryService: DishCategoryService) {}

  saveCategory(): void {
    if (!this.name.trim()) {
      this.error = 'Название категории не может быть пустым.';
      return;
    }

    const request: DishCategoryRequestDTO = { name: this.name.trim() };
    this.isLoading = true;

    this.categoryService.createCategory(request).subscribe({
      next: () => {
        this.isLoading = false;
        const newCategory: DishCategoryDTO = { id: crypto.randomUUID(), name: this.name.trim() }; // Создаем новый объект для локального отображения
        this.categoryAdded.emit(newCategory);
      },
      error: (err) => {
        console.error('Ошибка создания категории:', err);
        this.error = 'Не удалось создать категорию.';
        this.isLoading = false;
      },
    });
  }

  closeModal(): void {
    this.close.emit();
  }
}
