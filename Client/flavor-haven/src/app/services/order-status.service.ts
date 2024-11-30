import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';
import { OrderStatusDTO } from '../models/dtos/order/order-status.dto';
import { OrderStatusRequestDTO } from '../models/dtos/order/order-status-request.dto';

@Injectable({
  providedIn: 'root',
})
export class OrderStatusService {
  private baseUrl = environment.baseApiUrl;
  private orderStatusUrls = environment.apiUrls.orderStatus;

  constructor(private http: HttpClient) {}

  // Получить все статусы заказов
  getAllOrderStatuses(): Observable<OrderStatusDTO[]> {
    return this.http.get<OrderStatusDTO[]>(`${this.baseUrl}${this.orderStatusUrls.getAll}`);
  }

  // Получить статус заказа по ID
  getOrderStatusById(id: string): Observable<OrderStatusDTO> {
    return this.http.get<OrderStatusDTO>(`${this.baseUrl}${this.orderStatusUrls.getById}/${id}`);
  }

  // Создать новый статус заказа
  createOrderStatus(request: OrderStatusRequestDTO): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}${this.orderStatusUrls.create}`, request);
  }

  // Удалить статус заказа
  deleteOrderStatus(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}${this.orderStatusUrls.delete}/${id}`);
  }

  // Обновить статус заказа
  updateOrderStatus(id: string, request: OrderStatusRequestDTO): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}${this.orderStatusUrls.update}/${id}`, request);
  }
}
