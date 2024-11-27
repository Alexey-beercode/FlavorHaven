import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';
import { CreateReviewRequestDTO } from '../models/dtos/review/create-review-request.dto';
import { ReviewDTO } from '../models/dtos/review/review.dto';

@Injectable({
  providedIn: 'root',
})
export class ReviewService {
  private baseUrl = environment.baseApiUrl;
  private reviewUrls = environment.apiUrls.review;

  constructor(private http: HttpClient) {}

  createReview(request: CreateReviewRequestDTO): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}${this.reviewUrls.create}`, request);
  }

  deleteReview(reviewId: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}${this.reviewUrls.delete}/${reviewId}`);
  }

  getReviewById(reviewId: string): Observable<ReviewDTO> {
    return this.http.get<ReviewDTO>(`${this.baseUrl}${this.reviewUrls.getById}/${reviewId}`);
  }

  getReviewByOrderId(orderId: string): Observable<ReviewDTO> {
    return this.http.get<ReviewDTO>(`${this.baseUrl}${this.reviewUrls.getByOrder}/${orderId}`);
  }

  getReviewsByUserId(userId: string): Observable<ReviewDTO[]> {
    return this.http.get<ReviewDTO[]>(`${this.baseUrl}${this.reviewUrls.getByUser}/${userId}`);
  }
}
