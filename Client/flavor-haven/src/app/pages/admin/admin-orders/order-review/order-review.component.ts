import { Component, Input, OnInit } from '@angular/core';
import { ReviewService } from '../../../../services/review.service';
import { ReviewDTO } from '../../../../models/dtos/review/review.dto';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-order-review',
  templateUrl: './order-review.component.html',
  styleUrls: ['./order-review.component.css'],
  standalone: true,
  imports: [CommonModule],
})
export class OrderReviewComponent implements OnInit {
  @Input() orderId!: string;
  review: ReviewDTO | null = null;

  constructor(private reviewService: ReviewService) {}

  ngOnInit(): void {
    this.loadReview();
  }

  loadReview(): void {
    this.reviewService.getReviewByOrderId(this.orderId).subscribe({
      next: (review) => {
        this.review = review;
      },
      error: (err) => {
        console.error('Ошибка при загрузке отзыва:', err);
      },
    });
  }
}
