import { Component, OnInit } from '@angular/core';
import { CartService } from '../../services/cart.service';
import { CartDTO } from '../../models/dtos/cart/cart.dto';
import { CartItemComponent } from './cart-item/cart-item.component';
import { CartSummaryComponent } from './cart-summary/cart-summary.component';
import { CommonModule } from '@angular/common';
import { TokenService } from '../../services/token.service';
import {CreateOrderModalComponent} from './create-order-modal/create-order-modal.component';
import {BackButtonComponent} from '../../components/back-button/back-button.component';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css'],
  standalone: true,
  imports: [CartItemComponent, CartSummaryComponent, CreateOrderModalComponent, CommonModule, BackButtonComponent],
})
export class CartComponent implements OnInit {
  cartItems: CartDTO[] = [];
  userId: string = '';
  isLoading: boolean = false;
  isOrderModalVisible: boolean = false; // Управление отображением модального окна

  constructor(private cartService: CartService, private tokenService: TokenService) {}

  ngOnInit(): void {
    this.loadUserId();
    this.loadCart();
  }

  private loadUserId(): void {
    const userId = this.tokenService.getUserIdFromToken();
    this.userId = userId || '';
    if (!userId) {
      console.warn('Пользователь не авторизован: userId не найден.');
    }
  }

  // Загрузка корзины
  loadCart(): void {
    this.isLoading = true;
    this.cartService.getCartByUserId(this.userId).subscribe({
      next: (items) => {
        this.cartItems = items;
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Ошибка загрузки корзины:', err);
        this.isLoading = false;
      },
    });
  }

  // Очистка корзины
  clearCart(): void {
    this.cartService.clearCart(this.userId).subscribe({
      next: () => {
        this.cartItems = [];
      },
      error: (err) => console.error('Ошибка очистки корзины:', err),
    });
  }

  // Подсчет общей суммы
  calculateTotal(): number {
    return this.cartItems.reduce((total, item) => total + item.dish.price * item.count, 0);
  }

  // Открытие модального окна
  openOrderModal(): void {
    this.isOrderModalVisible = true;
  }

  // Обработка успешного создания заказа
  handleOrderCreated(): void {
    console.log('Заказ успешно создан!');
    this.clearCart(); // Очистить корзину
    this.isOrderModalVisible = false; // Закрыть модалку
  }
  trackByDishId(index: number, item: CartDTO): string {
    return item.dish.id; // Уникальный идентификатор блюда
  }

}
