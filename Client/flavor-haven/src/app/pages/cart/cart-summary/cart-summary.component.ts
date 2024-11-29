import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-cart-summary',
  templateUrl: './cart-summary.component.html',
  styleUrls: ['./cart-summary.component.css'],
  standalone: true,
  imports: [CommonModule],
})
export class CartSummaryComponent {
  @Input() total!: number; // Общая стоимость
  @Input() cartEmpty!: boolean; // Пустая ли корзина
  @Output() clearCart = new EventEmitter<void>(); // Событие очистки корзины
  @Output() checkout = new EventEmitter<void>(); // Событие оформления заказа
}
