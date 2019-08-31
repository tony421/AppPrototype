import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { delay } from 'rxjs/operators';

@Injectable({
    providedIn: 'root'
})
export class LoadingService {
    isLoading = new Subject<Boolean>();

    show() {
        console.info('Page is loading');
        this.isLoading.next(true);
    }
    hide() {
        console.info('Page is hiding');
        this.isLoading.next(false);
    }
}
