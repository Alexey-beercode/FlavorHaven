import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';
import { UserDTO } from '../models/dtos/user/user.dto';
import { UpdateUserBalanceRequestDTO } from '../models/dtos/user/update-user-balance-request.dto';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private baseUrl = environment.baseApiUrl;
  private userUrls = environment.apiUrls.user;

  constructor(private http: HttpClient) {}

  // Получение всех пользователей
  getAllUsers(): Observable<UserDTO[]> {
    return this.http.get<UserDTO[]>(`${this.baseUrl}${this.userUrls.getAll}`);
  }

  // Получение пользователя по ID
  getUserById(id: string | null): Observable<UserDTO> {
    return this.http.get<UserDTO>(`${this.baseUrl}${this.userUrls.getById}/${id}`);
  }

  // Обновление баланса пользователя
  updateUserBalance(id: string, request: UpdateUserBalanceRequestDTO): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}${this.userUrls.updateBalance}/${id}`, request);
  }

  // Удаление пользователя
  deleteUser(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}${this.userUrls.delete}/${id}`);
  }
}
