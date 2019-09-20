import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { Router, ActivatedRoute, NavigationEnd } from '@angular/router';
import { filter, map, distinctUntilChanged } from 'rxjs/operators';
import { RouteData } from 'src/app/interfaces/route-data';
import { CastingService } from 'src/app/services/casting.service';

@Component({
    selector: 'app-titlebar',
    templateUrl: './titlebar.component.html',
    styleUrls: ['./titlebar.component.scss']
})
export class TitlebarComponent {
    constructor(private router: Router, private actiavedRoute: ActivatedRoute, private castingService: CastingService) { }

    currentRouteName$ = this.router.events.pipe(
        filter(event => event instanceof NavigationEnd),
        distinctUntilChanged(),
        map(event => this.getCurrentRouteName(this.actiavedRoute.root))
    );

    getCurrentRouteName(route: ActivatedRoute): string {
        const routeData = route.routeConfig ? this.castingService.GetRouteData(route.routeConfig) : null;
        
        const routeName = routeData ? routeData.breadcrumb : 'Home';

        if (route.firstChild) {
            // If we are not on our current path yet, keep looking into "firstChild"
            return this.getCurrentRouteName(route.firstChild);
        }

        return routeName;
    }
}
