import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, throwError } from 'rxjs';
import { tap, catchError, finalize } from 'rxjs/operators';
import { LoadingService } from './services/loading.service';

@Injectable()
export class AppLoadingInterceptor implements HttpInterceptor {
    constructor(private loadingService: LoadingService) {
    }
    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        //console.info('AppLoadingInterceptor is interceping the request');
        this.loadingService.show();

        return next.handle(req).pipe(
            finalize(() => this.loadingService.hide())
        );
    }
}