import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { TokenService } from '../services/token.service';
import { AuthService } from '../services/auth.service';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  private isRefreshing = false; // Флаг для предотвращения дублирования запросов

  constructor(
    private tokenService: TokenService,
    private authService: AuthService,
    private router: Router
  ) {}

  canActivate(): boolean {
    const accessToken = this.tokenService.getAccessToken();

    if (accessToken) {
      return true;
    }

    const refreshToken = this.tokenService.getRefreshToken();

    if (refreshToken && !this.isRefreshing) {
      this.isRefreshing = true;
      this.authService.refreshToken().subscribe({
        next: (tokens) => {
          this.tokenService.saveTokens(tokens.accessToken, tokens.refreshToken);
          this.isRefreshing = false;
          return true;
        },
        error: () => {
          this.isRefreshing = false;
          this.router.navigate(['/login']);
          return false;
        },
      });
    } else {
      this.router.navigate(['/login']);
      return false;
    }
    return false; // Возвращаем false, если не удалось проверить токен
  }
}
