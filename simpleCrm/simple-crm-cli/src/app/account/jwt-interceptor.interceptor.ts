import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { AccountService } from './account.service';
import { Router } from '@angular/router';

@Injectable()
export class JwtInterceptorInterceptor implements HttpInterceptor {

  constructor(
    private accountService: AccountService,
    private router: Router ) {}

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const jwtToken = this.getJwtToken();
    if (jwtToken &&
      this.accountService.user.value.name != 'Anonymous') {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${jwtToken}`
        }
//        headers: request.headers.set('Authorization', 'Bearer ${jwtToken}')
      })
    }

    return next.handle(request).pipe(
      tap(
        (event: HttpEvent<any>) => {},
        (err: any) => {
          if (err instanceof HttpErrorResponse ) {
            if (err.status === 401) {
              this.accountService.logout();
              this.router.navigate(['login']);
            }
          }
        }
      )
    );
  }
  getJwtToken() {
    return this.accountService.user.value.jwtToken;
  }}

