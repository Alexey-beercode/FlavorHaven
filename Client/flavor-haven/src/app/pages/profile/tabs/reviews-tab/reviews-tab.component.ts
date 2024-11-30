import { Component, Input, OnInit } from '@angular/core';
import { ReviewService } from '../../../../services/review.service';
import { ReviewDTO } from '../../../../models/dtos/review/review.dto';
import { OrderService } from '../../../../services/order.service';
import { OrderDTO } from '../../../../models/dtos/order/order.dto';
import { CommonModule } from '@angular/common';
import { LoadingSpinnerComponent } from '../../../../components/loading-spinner/loading-spinner.component';
import { ErrorMessageComponent } from '../../../../components/error-message/error-message.component';

@Component({
  selector: 'app-reviews-tab',
  templateUrl: './reviews-tab.component.html',
  styleUrls: ['./reviews-tab.component.css'],
  standalone: true,
  imports: [CommonModule, LoadingSpinnerComponent, ErrorMessageComponent],
})
export class ReviewsTabComponent implements OnInit {
  @Input() userId: string | null = ''; // ID пользователя

  reviews: ReviewDTO[] = []; // Список отзывов
  orders: OrderDTO[] = []; // Список заказов пользователя
  isLoading: boolean = false; // Состояние загрузки
  error: string | null = null; // Сообщение об ошибке

  constructor(
    private reviewService: ReviewService,
    private orderService: OrderService
  ) {}

  ngOnInit(): void {
    if (this.userId) {
      this.loadUserReviews(); // Загрузка отзывов пользователя
      this.loadUserOrders();  // Загрузка заказов пользователя
    }
  }

  // Загрузка отзывов для текущего пользователя
  loadUserReviews(): void {
    if (!this.userId) {
      return;
    }

    this.isLoading = true;
    this.reviewService.getReviewsByUserId(this.userId).subscribe({
      next: (reviews) => {
        this.reviews = reviews; // Сохраняем только отзывы этого пользователя
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Ошибка загрузки отзывов:', err);
        this.isLoading = false;
        this.error = 'Не удалось загрузить отзывы.';
      },
    });
  }

  // Загрузка заказов для текущего пользователя
  loadUserOrders(): void {
    if (!this.userId) {
      return;
    }

    this.isLoading = true;
    this.orderService.getOrdersByUserId(this.userId).subscribe({
      next: (orders) => {
        this.orders = orders; // Сохраняем заказы пользователя
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Ошибка загрузки заказов:', err);
        this.isLoading = false;
        this.error = 'Не удалось загрузить заказы.';
      },
    });
  }
  // Функция для получения номера заказа по orderId
  getOrderNumberById(orderId: string): string | undefined {
    const order = this.orders.find((order) => order.id === orderId);
    return order ? order.orderNumber : 'Не найден';
  }

// Функция для получения заказа по orderId (если нужно выводить больше информации)
  getOrderById(orderId: string): OrderDTO | undefined {
    return this.orders.find((order) => order.id === orderId);
  }

}
