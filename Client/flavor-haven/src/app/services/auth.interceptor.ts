import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { TokenService } from './token.service';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const tokenService = inject(TokenService);
  const token = tokenService.getAccessToken();

  const authReq = req.clone({
    withCredentials: true, // Если требуется
    headers: req.headers
      .set('Accept', 'application/json')
      .set(
        'Authorization',
        token ? `Bearer ${token}` : ''
      ),
  });

  console.log('Request:', authReq);

  return next(authReq);
};
