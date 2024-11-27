import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';
import { CreatePaymentRequestDTO } from '../models/dtos/payment/create-payment-request.dto';
import { PaymentDTO } from '../models/dtos/payment/payment.dto';

@Injectable({
  providedIn: 'root',
})
export class PaymentService {
  private baseUrl = environment.baseApiUrl;
  private paymentUrls = environment.apiUrls.payment;

  constructor(private http: HttpClient) {}

  createPayment(request: CreatePaymentRequestDTO): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}${this.paymentUrls.create}`, request);
  }

  getAllPayments(): Observable<PaymentDTO[]> {
    return this.http.get<PaymentDTO[]>(`${this.baseUrl}${this.paymentUrls.getAll}`);
  }

  getPaymentById(paymentId: string): Observable<PaymentDTO> {
    return this.http.get<PaymentDTO>(`${this.baseUrl}${this.paymentUrls.getById}/${paymentId}`);
  }

  getPaymentByOrderId(orderId: string): Observable<PaymentDTO> {
    return this.http.get<PaymentDTO>(`${this.baseUrl}${this.paymentUrls.getByOrder}/${orderId}`);
  }

  getPaymentsByUserId(userId: string): Observable<PaymentDTO[]> {
    return this.http.get<PaymentDTO[]>(`${this.baseUrl}${this.paymentUrls.getByUser}/${userId}`);
  }
}
