import { Component, Input, OnInit } from '@angular/core';
import { ReviewService } from '../../../../services/review.service';
import { ReviewDTO } from '../../../../models/dtos/review/review.dto';
import { CreateReviewRequestDTO } from '../../../../models/dtos/review/create-review-request.dto';
import { CommonModule } from '@angular/common';
import { LoadingSpinnerComponent } from '../../../../components/loading-spinner/loading-spinner.component';
import { ErrorMessageComponent } from '../../../../components/error-message/error-message.component';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-reviews-tab',
  templateUrl: './reviews-tab.component.html',
  styleUrls: ['./reviews-tab.component.css'],
  standalone: true,
  imports: [CommonModule, LoadingSpinnerComponent, ErrorMessageComponent, FormsModule],
})
export class ReviewsTabComponent implements OnInit {
  @Input() userId: string | null = ''; // ID пользователя
  @Input() orderId: string | null = ''; // ID заказа для отзыва

  review: string = ''; // Текст отзыва
  reviews: ReviewDTO[] = []; // Список отзывов
  isLoading: boolean = false; // Состояние загрузки
  error: string | null = null; // Сообщение об ошибке

  constructor(private reviewService: ReviewService) {}

  ngOnInit(): void {
    // Если передан orderId, то возможно это запрос для создания нового отзыва
    if (this.orderId) {
      this.loadExistingReview(); // Загрузка существующего отзыва по заказу (если он есть)
    }
  }

  // Загрузка текущего отзыва по заказу (если он существует)
  loadExistingReview(): void {
    if (!this.orderId) {
      return;
    }

    this.isLoading = true;
    this.reviewService.getReviewByOrderId(this.orderId).subscribe({
      next: (review) => {
        if (review) {
          this.review = review.note; // Загружаем существующий отзыв
        }
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Ошибка загрузки отзыва:', err);
        this.isLoading = false;
        this.error = 'Не удалось загрузить отзыв.';
      },
    });
  }

  // Отправка нового отзыва
  submitReview(): void {
    if (!this.orderId || !this.review) {
      this.error = 'Отзыв не может быть пустым';
      return;
    }

    this.isLoading = true;
    const reviewRequest: CreateReviewRequestDTO = {
      orderId: this.orderId,
      note: this.review,
    };

    this.reviewService.createReview(reviewRequest).subscribe({
      next: () => {
        this.isLoading = false;
        this.review = ''; // Очистить поле отзыва после отправки
        // Можем добавить логику для успешной отправки, например, возврат на предыдущую вкладку
      },
      error: (err) => {
        console.error('Ошибка отправки отзыва:', err);
        this.isLoading = false;
        this.error = 'Не удалось отправить отзыв.';
      },
    });
  }
}
