import {inject, Injectable} from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';
import { LoginRequestDTO } from '../models/dtos/auth/login-request.dto';
import { RegisterRequestDTO } from '../models/dtos/auth/register-request.dto';
import { TokensDTO } from '../models/dtos/auth/tokens.dto';
import { TokenService } from './token.service';
import {Router} from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private baseUrl = environment.baseApiUrl;
  private authUrls = environment.apiUrls.auth;

  constructor(private http: HttpClient, private tokenService: TokenService) {}

  login(request: LoginRequestDTO): Observable<TokensDTO> {
    return new Observable((subscriber) => {
      this.http.post<TokensDTO>(`${this.baseUrl}${this.authUrls.login}`, request).subscribe({
        next: (tokens) => {
          this.tokenService.saveTokens(tokens.accessToken, tokens.refreshToken);
          subscriber.next(tokens);
          subscriber.complete();
        },
        error: (err) => subscriber.error(),
      });
    });
  }

  register(request: RegisterRequestDTO): Observable<TokensDTO> {
    return new Observable((subscriber) => {
      this.http.post<TokensDTO>(`${this.baseUrl}${this.authUrls.register}`, request).subscribe({
        next: (tokens) => {
          this.tokenService.saveTokens(tokens.accessToken, tokens.refreshToken);
          subscriber.next(tokens);
          subscriber.complete();
        },
        error: (err) => subscriber.error(),
      });
    });
  }

  refreshToken(): Observable<TokensDTO> {
    const refreshToken = this.tokenService.getRefreshToken();
    if (!refreshToken) throw new Error('No refresh token available');

    return new Observable((subscriber) => {
      this.http.post<TokensDTO>(`${this.baseUrl}${this.authUrls.refreshToken}/${refreshToken}`, {}).subscribe({
        next: (tokens) => {
          this.tokenService.saveTokens(tokens.accessToken, tokens.refreshToken);
          subscriber.next(tokens);
          subscriber.complete();
        },
        error: (err) => subscriber.error(),
      });
    });
  }

  revoke(userId: string | null): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}${this.authUrls.revoke}/${userId}`, {});
  }

  logout(): void {
    this.tokenService.clearTokens();
    // Перенаправление на страницу логина
    inject(Router).navigate(['/login']);
  }

}
