import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';
import { DishDTO } from '../models/dtos/dish/dish.dto';
import { GetDishesRequestDTO } from '../models/dtos/dish/get-dishes-request.dto';
import { PaginatedResult } from '../models/dtos/common/paginated-result.dto';
import { DishRequestDTO } from '../models/dtos/dish/dish-request.dto';

@Injectable({
  providedIn: 'root',
})
export class DishService {
  private baseUrl = environment.baseApiUrl;
  private dishUrls = environment.apiUrls.dish;

  constructor(private http: HttpClient) {}

  // Получить блюдо по ID
  getDishById(id: string): Observable<DishDTO> {
    return this.http.get<DishDTO>(`${this.baseUrl}${this.dishUrls.getById}/${id}`);
  }

  // Получить список блюд по параметрам
  getDishes(request: GetDishesRequestDTO): Observable<PaginatedResult<DishDTO>> {
    return this.http.post<PaginatedResult<DishDTO>>(
      `${this.baseUrl}${this.dishUrls.getByParameters}`,
      request
    );
  }

  // Создать блюдо
  createDish(request: DishRequestDTO): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}${this.dishUrls.create}`, request);
  }

  // Удалить блюдо
  deleteDish(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}${this.dishUrls.delete}/${id}`);
  }

  // Обновить блюдо
  updateDish(id: string, request: DishRequestDTO): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}${this.dishUrls.update}/${id}`, request);
  }
}
