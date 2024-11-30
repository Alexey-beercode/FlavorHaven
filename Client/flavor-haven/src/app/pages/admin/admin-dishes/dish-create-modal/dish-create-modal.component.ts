import { Component, EventEmitter, Input, Output } from '@angular/core';
import { DishService } from '../../../../services/dish.service';
import { DishRequestDTO } from '../../../../models/dtos/dish/dish-request.dto';
import { DishCategoryDTO } from '../../../../models/dtos/dish-category/dish-category.dto';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-dish-create-modal',
  templateUrl: './dish-create-modal.component.html',
  styleUrls: ['./dish-create-modal.component.css'],
  standalone: true,
  imports: [CommonModule, FormsModule],
})
export class DishCreateModalComponent {
  @Input() categories: DishCategoryDTO[] = [];
  @Output() close = new EventEmitter<void>();

  dish: DishRequestDTO = {
    name: '',
    description: '',
    price: 0,
    imageUrl: '',
    categoryId: '',
  };

  constructor(private dishService: DishService) {}

  createDish(): void {
    this.dishService.createDish(this.dish).subscribe({
      next: () => this.close.emit(),
      error: (err) => console.error('Ошибка при создании блюда:', err),
    });
  }
}
