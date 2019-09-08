import { Component, ChangeDetectorRef } from '@angular/core';
import { LoadingService } from './services/loading.service';
import { Subject } from 'rxjs';
import { Router, NavigationStart, NavigationCancel, NavigationError, NavigationEnd, ChildActivationStart, ChildActivationEnd, ActivationStart, ActivationEnd } from '@angular/router';
import { delay } from 'rxjs/operators';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss']
})
export class AppComponent {
    title = 'App Components';
    isLoading$ = this.loadingService.isLoading;

    constructor(private route: Router, private loadingService: LoadingService, private cdRef: ChangeDetectorRef) {
        this.route.events.subscribe(event => {
            switch (true) {
                case event instanceof NavigationStart:
                    //console.info("NavigationStart");
                    this.loadingService.show();
                    break;
                case event instanceof NavigationCancel:
                case event instanceof NavigationError:
                case event instanceof NavigationEnd:
                    //console.info("NavigationEnd");
                    setTimeout(e => this.loadingService.hide(), 350);
                    break;
                default:
                    break;
            }
        });
    }

    ngAfterViewChecked() {
        // Invoke this mothod to sovling the error "ExpressionChangedAfterItHasBeenCheckedError: Expression has changed after it was checked"
        // Because the error occurs when showing ngx-loading
        this.cdRef.detectChanges();
    }
}
