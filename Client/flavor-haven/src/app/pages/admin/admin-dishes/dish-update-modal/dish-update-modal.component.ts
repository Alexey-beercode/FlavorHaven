import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { DishDTO } from '../../../../models/dtos/dish/dish.dto';
import { DishRequestDTO } from '../../../../models/dtos/dish/dish-request.dto';
import { DishService } from '../../../../services/dish.service';
import { DishCategoryDTO } from '../../../../models/dtos/dish-category/dish-category.dto';
import { CommonModule } from '@angular/common';
import {FormsModule} from '@angular/forms';

@Component({
  selector: 'app-dish-update-modal',
  templateUrl: './dish-update-modal.component.html',
  styleUrls: ['./dish-update-modal.component.css'],
  standalone: true,
  imports: [CommonModule, FormsModule],
})
export class DishUpdateModalComponent implements OnInit {
  @Input() dish!: DishDTO;
  @Input() categories: DishCategoryDTO[] = [];
  @Output() close = new EventEmitter<void>();

  updatedDish: DishRequestDTO = {
    name: '',
    description: '',
    price: 0,
    imageUrl: '',
    categoryId: '',
  };

  constructor(private dishService: DishService) {}

  ngOnInit(): void {
    if (this.dish) {
      this.updatedDish = {
        name: this.dish.name,
        description: this.dish.description,
        price: this.dish.price,
        imageUrl: this.dish.imageUrl,
        categoryId: this.dish.category?.id || '',
      };
    }
  }

  updateDish(): void {
    this.dishService.updateDish(this.dish.id, this.updatedDish).subscribe({
      next: () => this.close.emit(),
      error: (err: any) => {
        console.error('Ошибка при обновлении блюда:', err);
      },
    });
  }
}
