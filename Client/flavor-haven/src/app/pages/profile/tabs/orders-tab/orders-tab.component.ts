import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { OrderService } from '../../../../services/order.service';
import { UserService } from '../../../../services/user.service';
import { OrderDTO } from '../../../../models/dtos/order/order.dto';
import { UserDTO } from '../../../../models/dtos/user/user.dto';
import { CommonModule } from '@angular/common';
import { LoadingSpinnerComponent } from '../../../../components/loading-spinner/loading-spinner.component';
import { ErrorMessageComponent } from '../../../../components/error-message/error-message.component';
import { PaymentService } from '../../../../services/payment.service';
import { ReviewService } from '../../../../services/review.service';
import { CreateReviewRequestDTO } from '../../../../models/dtos/review/create-review-request.dto';
import {FormsModule} from '@angular/forms';

@Component({
  selector: 'app-order-tab',
  templateUrl: './orders-tab.component.html',
  styleUrls: ['./orders-tab.component.css'],
  standalone: true,
  imports: [CommonModule, LoadingSpinnerComponent, ErrorMessageComponent, FormsModule],
})
export class OrderTabComponent implements OnInit {
  @Input() userId!: string | null;
  @Output() orderSelected = new EventEmitter<string>();

  orders: OrderDTO[] = [];
  user: UserDTO | null = null;
  isLoading: boolean = false;
  error: string | null = null;
  isReviewModalOpen: boolean = false; // Статус модального окна
  reviewNote: string = ''; // Текст отзыва
  selectedOrderId: string | null = null; // Выбранный заказ для отзыва

  constructor(
    private orderService: OrderService,
    private userService: UserService,
    private paymentService: PaymentService,
    private reviewService: ReviewService
  ) {}

  ngOnInit(): void {
    this.loadUserOrders();
    this.loadUserInfo();
  }

  // Загрузка заказов пользователя
  loadUserOrders(): void {
    if (!this.userId) {
      this.error = 'ID пользователя отсутствует.';
      return;
    }

    this.isLoading = true;
    this.orderService.getOrdersByUserId(this.userId).subscribe({
      next: (orders) => {
        this.orders = orders;
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Ошибка загрузки заказов:', err);
        this.error = 'Не удалось загрузить заказы.';
        this.isLoading = false;
      },
    });
  }

  // Загрузка информации о пользователе
  loadUserInfo(): void {
    this.userService.getUserById(this.userId).subscribe({
      next: (user) => {
        this.user = user;
      },
      error: (err) => {
        console.error('Ошибка загрузки данных пользователя:', err);
      },
    });
  }

  // Оплата заказа
  payOrder(order: OrderDTO): void {
    if (!this.user || this.user.balance < order.amount) {
      this.error = 'Недостаточно средств для оплаты заказа.';
      return;
    }

    this.isLoading = true;
    this.paymentService.createPayment({ orderId: order.id }).subscribe({
      next: () => {
        this.loadUserOrders(); // Обновить список заказов
        this.loadUserInfo(); // Обновить баланс пользователя
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Ошибка оплаты заказа:', err);
        this.isLoading = false;
      },
    });
  }

  // Открытие модального окна для отзыва
  openReviewModal(orderId: string): void {
    this.selectedOrderId = orderId;
    this.isReviewModalOpen = true;
  }

  // Закрытие модального окна
  closeReviewModal(): void {
    this.isReviewModalOpen = false;
    this.reviewNote = '';
    this.selectedOrderId = null;
  }

  // Отправка отзыва
  submitReview(): void {
    if (!this.reviewNote.trim()) {
      this.error = 'Отзыв не может быть пустым.';
      return;
    }

    if (this.selectedOrderId) {
      const reviewRequest: CreateReviewRequestDTO = {
        orderId: this.selectedOrderId,
        note: this.reviewNote,
      };

      this.isLoading = true;
      this.reviewService.createReview(reviewRequest).subscribe({
        next: () => {
          this.isLoading = false;
          this.closeReviewModal();
          this.loadUserOrders(); // Обновить заказы
        },
        error: (err) => {
          console.error('Ошибка отправки отзыва:', err);
          this.isLoading = false;

          // Проверка на ошибку 409 (конфликт)
          if (err.status === 409) {
            this.error = 'Отзыв уже был оставлен для этого заказа.';
          } else {
            this.error = 'Не удалось отправить отзыв.';
          }
        },
      });
    }
  }

}
