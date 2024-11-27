import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';
import { DishCategoryDTO } from '../models/dtos/dish-category/dish-category.dto';

@Injectable({
  providedIn: 'root',
})
export class DishCategoryService {
  private baseUrl = environment.baseApiUrl;
  private categoryUrls = environment.apiUrls.dishCategory;

  constructor(private http: HttpClient) {}

  getAllCategories(): Observable<DishCategoryDTO[]> {
    return this.http.get<DishCategoryDTO[]>(`${this.baseUrl}${this.categoryUrls.getAll}`);
  }

  getCategoryById(id: string): Observable<DishCategoryDTO> {
    return this.http.get<DishCategoryDTO>(`${this.baseUrl}${this.categoryUrls.getById}/${id}`);
  }
}
