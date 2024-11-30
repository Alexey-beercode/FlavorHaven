import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { TokenService } from './token.service';
import { AuthService } from './auth.service';
import { Router } from '@angular/router';
import { catchError, switchMap, throwError } from 'rxjs';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const authService = inject(AuthService);
  const tokenService = inject(TokenService);
  const router = inject(Router);

  const token = tokenService.getAccessToken();

  const authReq = req.clone({
    withCredentials: true,
    headers: req.headers.set('Authorization', token ? `Bearer ${token}` : ''),
  });

  return next(authReq).pipe(
    catchError((error) => {
      if (error.status === 401 && !req.url.includes('/auth/refresh')) {
        // Если 401 и не запрос на refresh, пробуем обновить токен
        return authService.refreshToken().pipe(
          switchMap((tokens) => {
            tokenService.saveTokens(tokens.accessToken, tokens.refreshToken);
            const retryReq = authReq.clone({
              headers: authReq.headers.set('Authorization', `Bearer ${tokens.accessToken}`),
            });
            return next(retryReq);
          }),
          catchError((refreshError) => {
            // Если рефреш не удался, очищаем токены и перенаправляем на логин
            console.error('Ошибка обновления токена:', refreshError);
            tokenService.clearTokens();
            router.navigate(['/login']);
            return throwError(() => new Error('Не удалось обновить токен'));
          })
        );
      }
      return throwError(() => error);
    })
  );
};
