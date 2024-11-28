import { Component, Input } from '@angular/core';
import { CartService } from '../../../services/cart.service';
import { CartItemRequestDTO } from '../../../models/dtos/cart/cart-item-request.dto';

@Component({
  selector: 'app-add-to-cart',
  templateUrl: './add-to-cart.component.html',
  styleUrls: ['./add-to-cart.component.css'],
  standalone: true,
})
export class AddToCartComponent {
  @Input() dishId!: string; // ID блюда
  @Input() userId!: string; // ID пользователя

  isLoading = false; // Состояние загрузки
  isAdded = false; // Показывает, что блюдо добавлено в корзину

  constructor(private cartService: CartService) {}

  addToCart(): void {
    if (!this.userId || !this.dishId) {
      console.error('Не указан userId или dishId для добавления в корзину.');
      return;
    }

    this.isLoading = true;

    const item: CartItemRequestDTO = {
      dishId: this.dishId,
      count: 1, // Количество по умолчанию
    };

    this.cartService.addToCart(this.userId, item).subscribe({
      next: () => {
        this.isLoading = false;
        this.isAdded = true; // Показать статус успешного добавления
        setTimeout(() => (this.isAdded = false), 2000); // Скрыть статус через 2 секунды
      },
      error: (err) => {
        this.isLoading = false;
        console.error('Ошибка при добавлении в корзину:', err);
      },
    });
  }
}
