import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { AuthService } from './auth.service';
import { TokenService } from './token.service';
import { Router } from '@angular/router';
import { catchError, switchMap } from 'rxjs/operators';
import { throwError, of } from 'rxjs';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const authService = inject(AuthService);
  const tokenService = inject(TokenService);
  const router = inject(Router);

  const token = tokenService.getAccessToken();

  const authReq = req.clone({
    withCredentials: true,
    headers: req.headers.set(
      'Authorization',
      token ? `Bearer ${token}` : ''
    ),
  });

  return next(authReq).pipe(
    catchError((error) => {
      if (error.status === 401) {
        // Если 401, пробуем рефреш токена
        return authService.refreshToken().pipe(
          switchMap((tokens) => {
            tokenService.saveTokens(tokens.accessToken, tokens.refreshToken);
            const retryReq = authReq.clone({
              headers: authReq.headers.set(
                'Authorization',
                `Bearer ${tokens.accessToken}`
              ),
            });
            return next(retryReq);
          }),
          catchError((refreshError) => {
            // Если рефреш не удался, переходим на логин
            tokenService.clearTokens();
            router.navigate(['/login']);
            return throwError(() => refreshError);
          })
        );
      }
      return throwError(() => error);
    })
  );
};
