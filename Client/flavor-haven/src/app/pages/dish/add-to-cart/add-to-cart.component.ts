import { Component, Input } from '@angular/core';
import { CartService } from '../../../services/cart.service';
import { CartItemRequestDTO } from '../../../models/dtos/cart/cart-item-request.dto';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms'; // FormsModule добавлен для привязки

@Component({
  selector: 'app-add-to-cart',
  templateUrl: './add-to-cart.component.html',
  styleUrls: ['./add-to-cart.component.css'],
  standalone: true,
  imports: [CommonModule, FormsModule],
})
export class AddToCartComponent {
  @Input() dishId!: string; // ID блюда
  @Input() userId!: string; // ID пользователя

  quantity: number = 1; // Количество по умолчанию
  isLoading: boolean = false; // Состояние загрузки
  isAdded: boolean = false; // Показывает, что блюдо добавлено в корзину

  constructor(private cartService: CartService) {}

  addToCart(): void {
    if (!this.userId || !this.dishId) {
      console.error();
      return;
    }

    this.isLoading = true;

    const item: CartItemRequestDTO = {
      dishId: this.dishId,
      count: this.quantity, // Используем выбранное количество
    };

    this.cartService.addToCart(this.userId, item).subscribe({
      next: () => {
        this.isLoading = false;
        this.isAdded = true;
        setTimeout(() => (this.isAdded = false), 2000);
      },
      error: (err) => {
        this.isLoading = false;
        console.error(err);
      },
    });
  }

  // Увеличить количество
  increaseQuantity(): void {
    this.quantity++;
  }

  // Уменьшить количество
  decreaseQuantity(): void {
    if (this.quantity > 1) {
      this.quantity--;
    }
  }
}
