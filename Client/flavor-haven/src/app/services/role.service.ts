import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';
import { RoleDTO } from '../models/dtos/role/role.dto';
import { RoleRequestDTO } from '../models/dtos/role/role-request.dto';
import { UserRoleRequestDTO } from '../models/dtos/role/user-role-request.dto';

@Injectable({
  providedIn: 'root',
})
export class RoleService {
  private baseUrl = environment.baseApiUrl;
  private roleUrls = environment.apiUrls.role;

  constructor(private http: HttpClient) {}

  // Получить все роли
  getAllRoles(): Observable<RoleDTO[]> {
    return this.http.get<RoleDTO[]>(`${this.baseUrl}${this.roleUrls.getAll}`);
  }

  // Получить роль по ID
  getRoleById(id: string): Observable<RoleDTO> {
    return this.http.get<RoleDTO>(`${this.baseUrl}${this.roleUrls.getById}/${id}`);
  }

  // Получить роль по имени
  getRoleByName(name: string): Observable<RoleDTO> {
    return this.http.get<RoleDTO>(`${this.baseUrl}${this.roleUrls.getByName}/${name}`);
  }

  // Получить роли пользователя
  getRolesByUserId(userId: string | undefined): Observable<RoleDTO[]> {
    return this.http.get<RoleDTO[]>(`${this.baseUrl}${this.roleUrls.getByUser}/${userId}`);
  }

  // Создать новую роль
  createRole(request: RoleRequestDTO): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}${this.roleUrls.create}`, request);
  }

  // Удалить роль
  deleteRole(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}${this.roleUrls.delete}/${id}`);
  }

  // Назначить роль пользователю
  setRoleToUser(request: UserRoleRequestDTO): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}${this.roleUrls.setToUser}`, request);
  }

  // Удалить роль у пользователя
  removeRoleFromUser(request: UserRoleRequestDTO): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}${this.roleUrls.removeFromUser}`, request);
  }

  // Обновить роль
  updateRole(id: string, request: RoleRequestDTO): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}${this.roleUrls.update}/${id}`, request);
  }
}
