import { Component, ChangeDetectorRef } from '@angular/core';
import { LoadingService } from './services/loading.service';
import { Subject } from 'rxjs';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss']
})
export class AppComponent {
    title = 'App Components';
    isLoading$ = this.loadingService.isLoading;

    constructor(private loadingService: LoadingService, private cdRef: ChangeDetectorRef) {
    }

    ngAfterViewChecked() {
        // Invoke this mothod to sovling the error "ExpressionChangedAfterItHasBeenCheckedError: Expression has changed after it was checked"
        // Because the error occurs when showing ngx-loading
        this.cdRef.detectChanges();
    }
}
