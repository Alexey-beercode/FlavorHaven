import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';
import { RoleDTO } from '../models/dtos/role/role.dto';

@Injectable({
  providedIn: 'root',
})
export class RoleService {
  private baseUrl = environment.baseApiUrl;
  private roleUrls = environment.apiUrls.role;

  constructor(private http: HttpClient) {}

  getAllRoles(): Observable<RoleDTO[]> {
    return this.http.get<RoleDTO[]>(`${this.baseUrl}${this.roleUrls.getAll}`);
  }

  getRoleById(id: string): Observable<RoleDTO> {
    return this.http.get<RoleDTO>(`${this.baseUrl}${this.roleUrls.getById}/${id}`);
  }

  getRoleByName(name: string): Observable<RoleDTO> {
    return this.http.get<RoleDTO>(`${this.baseUrl}${this.roleUrls.getByName}/${name}`);
  }

  getRolesByUserId(userId: string): Observable<RoleDTO[]> {
    return this.http.get<RoleDTO[]>(`${this.baseUrl}${this.roleUrls.getByUser}/${userId}`);
  }
}
