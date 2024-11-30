import { Component, Input, Output, EventEmitter } from '@angular/core';
import { DishDTO } from '../../../models/dtos/dish/dish.dto';
import { CommonModule } from '@angular/common';
import {AddToCartComponent} from '../add-to-cart/add-to-cart.component'; // Предполагаем, что есть модель блюда

@Component({
  selector: 'app-dish-details',
  templateUrl: './dish-details.component.html',
  styleUrls: ['./dish-details.component.css'],
  standalone: true,
  imports: [
    AddToCartComponent, CommonModule
  ]
})
export class DishDetailsComponent {
  @Input() dish!: DishDTO; // Данные блюда
  @Input() isVisible: boolean = false; // Управление видимостью модалки
  @Input() userId!: string; // ID пользователя
  @Output() closeModal = new EventEmitter<void>(); // Эмиттер для закрытия модалки

  // Закрытие модалки
  close(): void {
    this.closeModal.emit();
  }
}
