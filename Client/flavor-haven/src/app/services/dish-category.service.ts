import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';
import { DishCategoryDTO } from '../models/dtos/dish-category/dish-category.dto';
import { DishCategoryRequestDTO } from '../models/dtos/dish-category/dish-category-request.dto';

@Injectable({
  providedIn: 'root',
})
export class DishCategoryService {
  private baseUrl = environment.baseApiUrl;
  private dishCategoryUrls = environment.apiUrls.dishCategory;

  constructor(private http: HttpClient) {}

  // Получить все категории
  getAllCategories(): Observable<DishCategoryDTO[]> {
    return this.http.get<DishCategoryDTO[]>(`${this.baseUrl}${this.dishCategoryUrls.getAll}`);
  }

  // Получить категорию по ID
  getCategoryById(id: string): Observable<DishCategoryDTO> {
    return this.http.get<DishCategoryDTO>(`${this.baseUrl}${this.dishCategoryUrls.getById}/${id}`);
  }

  // Создать категорию
  createCategory(request: DishCategoryRequestDTO): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}${this.dishCategoryUrls.create}`, request);
  }

  // Удалить категорию
  deleteCategory(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}${this.dishCategoryUrls.delete}/${id}`);
  }

  // Обновить категорию
  updateCategory(id: string, request: DishCategoryRequestDTO): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}${this.dishCategoryUrls.update}/${id}`, request);
  }
}
