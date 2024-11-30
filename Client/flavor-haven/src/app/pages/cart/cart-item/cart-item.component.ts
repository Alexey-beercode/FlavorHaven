import {
  Component,
  Input,
  Output,
  EventEmitter,
  ChangeDetectionStrategy,
} from '@angular/core';
import { CartService } from '../../../services/cart.service';
import { CartDTO } from '../../../models/dtos/cart/cart.dto';
import { CartItemRequestDTO } from '../../../models/dtos/cart/cart-item-request.dto';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-cart-item',
  templateUrl: './cart-item.component.html',
  styleUrls: ['./cart-item.component.css'],
  standalone: true,
  imports: [CommonModule],
  changeDetection: ChangeDetectionStrategy.OnPush, // Оптимизация
})
export class CartItemComponent {
  @Input() item!: CartDTO; // Элемент корзины
  @Input() userId!: string; // ID пользователя
  @Output() updated = new EventEmitter<void>(); // Эмиттер для обновления корзины

  isProcessing: boolean = false; // Локальная переменная для индикации загрузки

  constructor(private cartService: CartService) {}

  // Увеличение количества через addToCart
  increaseQuantity(): void {
    if (this.isProcessing) return; // Блокируем, если идет текущий запрос

    this.isProcessing = true;
    const cartItem: CartItemRequestDTO = {
      dishId: this.item.dish.id,
      count: 1, // Увеличиваем на 1
    };

    this.cartService.addToCart(this.userId, cartItem).subscribe({
      next: () => {
        this.item.count += 1; // Локально увеличиваем количество
        this.isProcessing = false;
        this.updated.emit(); // Эмит обновления корзины
      },
      error: (err) => {
        console.error('Ошибка увеличения количества:', err);
        this.isProcessing = false;
      },
    });
  }

  // Уменьшение количества через removeFromCart
  decreaseQuantity(): void {
    if (this.isProcessing || this.item.count <= 1) return; // Блокируем, если идет текущий запрос или количество = 1

    this.isProcessing = true;
    const cartItem: CartItemRequestDTO = {
      dishId: this.item.dish.id,
      count: 1, // Уменьшаем на 1
    };

    this.cartService.removeFromCart(this.userId, cartItem).subscribe({
      next: () => {
        this.item.count -= 1; // Локально уменьшаем количество
        this.isProcessing = false;
        this.updated.emit(); // Эмит обновления корзины
      },
      error: (err) => {
        console.error('Ошибка уменьшения количества:', err);
        this.isProcessing = false;
      },
    });
  }

  // Удаление элемента из корзины
  removeItem(): void {
    if (this.isProcessing) return; // Блокируем, если идет текущий запрос

    this.isProcessing = true;
    const cartItem: CartItemRequestDTO = {
      dishId: this.item.dish.id,
      count: this.item.count, // Удаляем все количество
    };

    this.cartService.removeFromCart(this.userId, cartItem).subscribe({
      next: () => {
        this.updated.emit(); // Уведомляем об изменении корзины
        this.isProcessing = false;
      },
      error: (err) => {
        console.error('Ошибка удаления товара:', err);
        this.isProcessing = false;
      },
    });
  }
}
