import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';
import { OrderStatusDTO } from '../models/dtos/order/order-status.dto';

@Injectable({
  providedIn: 'root',
})
export class OrderStatusService {
  private baseUrl = environment.baseApiUrl;
  private orderStatusUrls = environment.apiUrls.orderStatus;

  constructor(private http: HttpClient) {}

  getAllOrderStatuses(): Observable<OrderStatusDTO[]> {
    return this.http.get<OrderStatusDTO[]>(`${this.baseUrl}${this.orderStatusUrls.getAll}`);
  }

  getOrderStatusById(id: string): Observable<OrderStatusDTO> {
    return this.http.get<OrderStatusDTO>(`${this.baseUrl}${this.orderStatusUrls.getById}/${id}`);
  }
}
