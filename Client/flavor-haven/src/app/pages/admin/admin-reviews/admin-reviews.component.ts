import { Component, OnInit } from '@angular/core';
import { OrderService } from '../../../services/order.service';
import { ReviewService } from '../../../services/review.service';
import { OrderDTO } from '../../../models/dtos/order/order.dto';
import { ReviewDTO } from '../../../models/dtos/review/review.dto';
import { ErrorMessageComponent } from '../../../components/error-message/error-message.component';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-admin-reviews',
  templateUrl: './admin-reviews.component.html',
  styleUrls: ['./admin-reviews.component.css'],
  imports: [ErrorMessageComponent,CommonModule],
  standalone: true
})
export class AdminReviewsComponent implements OnInit {
  orders: OrderDTO[] = [];
  reviews: { orderId: string; review: ReviewDTO | null }[] = [];
  isLoading: boolean = false;
  error: string ='';

  constructor(
    private orderService: OrderService,
    private reviewService: ReviewService
  ) {}

  ngOnInit(): void {
    this.loadOrdersWithReviews();
  }

  loadOrdersWithReviews(): void {
    this.isLoading = true;
    this.error = '';

    this.orderService.getAll().subscribe({
      next: (orders) => {
        this.orders = orders;
        this.loadReviewsForOrders(orders);
      },
      error: (err) => {
        console.error('Ошибка при загрузке заказов:', err);
        this.error = 'Не удалось загрузить заказы.';
        this.isLoading = false;
      },
    });
  }

  loadReviewsForOrders(orders: OrderDTO[]): void {
    this.reviews = [];

    orders.forEach((order) => {
      this.reviewService.getReviewByOrderId(order.id).subscribe({
        next: (review) => {
          this.reviews.push({ orderId: order.id, review });
        },
        error: () => {
          this.reviews.push({ orderId: order.id, review: null });
        },
      });
    });

    this.isLoading = false;
  }

  // Функция для получения отзыва по orderId
  getReviewForOrder(orderId: string): ReviewDTO | null {
    const review = this.reviews.find((r) => r.orderId === orderId);
    return review ? review.review : null;
  }

  deleteReview(reviewId: string): void {
    this.reviewService.deleteReview(reviewId).subscribe({
      next: () => {
        this.loadOrdersWithReviews();
      },
      error: (err) => {
        console.error('Ошибка при удалении отзыва:', err);
        this.error = 'Не удалось удалить отзыв.';
      },
    });
  }
}
