import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';
import { DishDTO } from '../models/dtos/dish/dish.dto';
import { GetDishesRequestDTO } from '../models/dtos/dish/get-dishes-request.dto';
import { PaginatedResult } from '../models/dtos/common/paginated-result.dto';

@Injectable({
  providedIn: 'root',
})
export class DishService {
  private baseUrl = environment.baseApiUrl;
  private dishUrls = environment.apiUrls.dish;

  constructor(private http: HttpClient) {}

  getDishById(id: string): Observable<DishDTO> {
    return this.http.get<DishDTO>(`${this.baseUrl}${this.dishUrls.getById}/${id}`);
  }

  getDishes(request: GetDishesRequestDTO): Observable<PaginatedResult<DishDTO>> {
    return this.http.post<PaginatedResult<DishDTO>>(
      `${this.baseUrl}${this.dishUrls.getByParameters}`,
      request
    );
  }
}
