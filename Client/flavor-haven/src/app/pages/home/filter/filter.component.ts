import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { DishCategoryService } from '../../../services/dish-category.service';
import { DishCategoryDTO } from '../../../models/dtos/dish-category/dish-category.dto';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common'; // Импортируем CommonModule

@Component({
  selector: 'app-filter',
  templateUrl: './filter.component.html',
  styleUrls: ['./filter.component.css'],
  standalone: true,
  imports: [FormsModule, CommonModule], // Добавляем CommonModule
})
export class FilterComponent implements OnInit {
  categories: DishCategoryDTO[] = [];
  selectedCategoryId: string | undefined;

  @Output() categorySelected = new EventEmitter<string | undefined>();

  constructor(private categoryService: DishCategoryService) {}

  ngOnInit(): void {
    this.loadCategories();
  }

  loadCategories(): void {
    this.categoryService.getAllCategories().subscribe({
      next: (categories) => {
        this.categories = categories;
        console.log('Категории:', this.categories); // Проверяем, что категории приходят
      },
      error: (err) => console.error(err),
    });
  }

  onCategoryChange(): void {
    console.log('Выбранная категория:', this.selectedCategoryId);
    this.categorySelected.emit(this.selectedCategoryId);
  }
}
