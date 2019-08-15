import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, throwError } from 'rxjs';
import { tap, catchError } from 'rxjs/operators';

@Injectable()
export class AppErrorInterceptor implements HttpInterceptor {
    constructor(private router: Router) {

    }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(req.clone()).pipe(
            catchError(this.handleError)
        );
    }

    handleError = (httpError: HttpErrorResponse) => {
        let msg: string = '';
        let statusCode: number = 0;

        if (httpError.error instanceof ErrorEvent) {
            // A client-side or network error occurred. Handle it accordingly.
            msg = httpError.error.message;
        } else {
            if (httpError.error != null && httpError.error.errors != null) {
                httpError.error.errors.forEach(i => {
                    msg += i.code + ':' + i.description + ' | ';
                });
            }
            else {
                statusCode = httpError.status;
                if (statusCode == 401) // Unauthorized
                {
                    msg = 'Unauthorized!';
                    this.router.navigateByUrl('/user/login');
                }
                else if (statusCode == 400) // Bad Request
                {
                    msg = httpError.message;
                    this.router.navigateByUrl('');
                }
                else if (statusCode == 404) // Page not found
                {
                    msg = httpError.message;
                    this.router.navigateByUrl('');
                }
                else if (statusCode == 500) // Internal Error
                {
                    msg = httpError.message;
                    this.router.navigateByUrl('');
                }
                else {
                    msg = httpError.message;
                }
            }
        }

        console.error('AppErrorInterceptor =>', 'Http response code:', statusCode, 'Message:', msg);
        return throwError(msg);
    }
}