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
            statusCode = httpError.status;
            if (httpError.error != null && httpError.error.errors != null) {
                // If a request could reach the API end-point.
                // An error emitted by the server must be in this format "httpError.error.errors"
                httpError.error.errors.forEach(i => {
                    if (msg != '') msg += ' | ';
                    if (i.code != null && i.code != '') msg += '[' + i.code + ']';
                    if (i.description != null && i.description != '') msg += i.description;
                });
            }
            else {
                msg = 'The request might not reach the API end-point! ';
                if (statusCode == 401) // Unauthorized
                {
                    msg += 'Unauthorized! ';
                    this.router.navigateByUrl('/user/login');
                }
                else if (statusCode == 400) // Bad Request
                {
                    msg += 'Bad Request! ';
                    this.router.navigateByUrl('');
                }
                else if (statusCode == 404) // Page not found
                {
                    msg += 'Page Not Found! ';
                    this.router.navigateByUrl('');
                }
                else if (statusCode == 500) // Internal Error
                {
                    msg += 'Internal Error! ';
                    this.router.navigateByUrl('');
                }

                msg += httpError.message;
            }
        }

        console.error('AppErrorInterceptor =>', 'Http response code:', statusCode, 'Message:', msg);
        return throwError(msg);
    }
}