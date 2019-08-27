import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Router, NavigationEnd, ActivatedRoute } from '@angular/router';
import { filter, map, distinctUntilChanged } from 'rxjs/operators';
import { Breadcrumb } from '../../interfaces/breadcrumb';
import { CastingService } from 'src/app/services/casting.service';

@Component({
    selector: 'app-topbar',
    templateUrl: './topbar.component.html',
    styleUrls: ['./topbar.component.scss'],
    encapsulation: ViewEncapsulation.None
})
export class TopbarComponent implements OnInit {
    constructor(private toast: ToastrService
        , private router: Router
        , private activatedRoute: ActivatedRoute
        , private castingService: CastingService) { }

    // Returning an overable object which can be used within *ngFor with Async pipe
    breadcrumbs$ = this.router.events.pipe(
        filter(event => event instanceof NavigationEnd),
        distinctUntilChanged(),
        map(event => this.buildBreadcrumbs(this.activatedRoute.root))
    );

    ngOnInit() {

    }

    buildBreadcrumbs(route: ActivatedRoute, url: string = '', breadcrumbs: Array<Breadcrumb> = []): Array<Breadcrumb> {
        // If no routeConfig is avalailable we are on the root path
        const path = route.routeConfig ? route.routeConfig.path : '';
        const routeData = route.routeConfig ? this.castingService.GetRouteData(route.routeConfig) : null;
        
        const label = routeData ? routeData.breadcrumb : 'Home';        

        // Rebuild the next url from the previous one
        const nextUrl = `${url}${path}/`;
        const breadcrumb = {
            label: label,
            url: nextUrl,
        };

        const newBreadcrumbs = [...breadcrumbs, breadcrumb];
        if (route.firstChild) {
            // If we are not on our current path yet,
            // there will be more children to look after, to build our breadcumb
            return this.buildBreadcrumbs(route.firstChild, nextUrl, newBreadcrumbs);
        }

        return newBreadcrumbs;
    }
}
