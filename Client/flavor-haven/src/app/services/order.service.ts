import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';
import { CreateOrderRequestDTO } from '../models/dtos/order/create-order-request.dto';
import { OrderDTO } from '../models/dtos/order/order.dto';
import { UpdateOrderRequestDTO } from '../models/dtos/order/update-order-request.dto';

@Injectable({
  providedIn: 'root',
})
export class OrderService {
  private baseUrl = environment.baseApiUrl;
  private orderUrls = environment.apiUrls.order;

  constructor(private http: HttpClient) {}

  // Создание заказа
  createOrder(userId: string | null, request: CreateOrderRequestDTO): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}${this.orderUrls.createByUserId}/${userId}`, request);
  }

  // Получить заказ по ID
  getOrderById(orderId: string): Observable<OrderDTO> {
    return this.http.get<OrderDTO>(`${this.baseUrl}${this.orderUrls.getById}/${orderId}`);
  }

  // Получить заказы по статусу
  getOrdersByStatus(statusId: string): Observable<OrderDTO[]> {
    return this.http.get<OrderDTO[]>(`${this.baseUrl}${this.orderUrls.getByStatus}/${statusId}`);
  }

  // Получить заказы пользователя
  getOrdersByUserId(userId: string): Observable<OrderDTO[]> {
    return this.http.get<OrderDTO[]>(`${this.baseUrl}${this.orderUrls.getByUser}/${userId}`);
  }

  // Удалить заказ
  deleteOrder(orderId: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}${this.orderUrls.delete}/${orderId}`);
  }

  // Обновить статус заказа
  updateOrderStatus(orderId: string, request: UpdateOrderRequestDTO): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}${this.orderUrls.updateStatus}/${orderId}`, request);
  }
}
