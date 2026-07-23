import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { catchError, throwError } from 'rxjs';

export const errorHandlerInterceptor: HttpInterceptorFn = (req, next) => {
  const router = inject(Router);
  return next(req).pipe(
    catchError((err) => {
      if (err.status === 401) {
        console.error('Unauthorized request - redirecting to home');
        router.navigate(['/']);
      } else if (err.status === 500) {
        alert('Server Error occurred. Please try again later.');
      }
      return throwError(() => err);
    })
  );
};