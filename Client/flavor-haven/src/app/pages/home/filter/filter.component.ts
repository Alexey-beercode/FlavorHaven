import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { DishCategoryService } from '../../../services/dish-category.service';
import { DishCategoryDTO } from '../../../models/dtos/dish-category/dish-category.dto';
import {FormsModule} from '@angular/forms';

@Component({
  selector: 'app-filter',
  templateUrl: './filter.component.html',
  styleUrls: ['./filter.component.css'],
  standalone: true,
  imports: [
    FormsModule
  ]
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
      next: (categories) => (this.categories = categories),
      error: (err) => console.error('Ошибка загрузки категорий', err),
    });
  }

  onCategoryChange(): void {
    this.categorySelected.emit(this.selectedCategoryId);
  }
}
