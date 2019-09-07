import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { delay } from 'rxjs/operators';

@Injectable({
    providedIn: 'root'
})
export class LoadingService {
    isLoading = new Subject<Boolean>();
    loadingCounter = 0;

    show() {
        console.info('Page is loading');
        this.isLoading.next(true);
        this.loadingCounter++;
    }
    hide() {        
        this.loadingCounter--;
        if (this.loadingCounter <= 0) {
            console.info('Page is hiding');
            this.isLoading.next(false);
            this.loadingCounter = 0;
        }
    }
}
