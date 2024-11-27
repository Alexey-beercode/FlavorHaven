import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';
import { CreateOrderRequestDTO } from '../models/dtos/order/create-order-request.dto';
import { OrderDTO } from '../models/dtos/order/order.dto';

@Injectable({
  providedIn: 'root',
})
export class OrderService {
  private baseUrl = environment.baseApiUrl;
  private orderUrls = environment.apiUrls.order;

  constructor(private http: HttpClient) {}

  createOrder(userId: string, request: CreateOrderRequestDTO): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}${this.orderUrls.createByUserId}/${userId}`, request);
  }

  getOrderById(orderId: string): Observable<OrderDTO> {
    return this.http.get<OrderDTO>(`${this.baseUrl}${this.orderUrls.getById}/${orderId}`);
  }

  getOrdersByStatus(statusId: string): Observable<OrderDTO[]> {
    return this.http.get<OrderDTO[]>(`${this.baseUrl}${this.orderUrls.getByStatus}/${statusId}`);
  }

  getOrdersByUserId(userId: string): Observable<OrderDTO[]> {
    return this.http.get<OrderDTO[]>(`${this.baseUrl}${this.orderUrls.getByUser}/${userId}`);
  }
}
