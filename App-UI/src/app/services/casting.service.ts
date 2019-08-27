import { Injectable } from '@angular/core';
import { ActivatedRoute, ActivatedRouteSnapshot, Route } from '@angular/router';
import { RouteData } from '../interfaces/route-data';

@Injectable({
    providedIn: 'root'
})
export class CastingService {
    constructor() { }

    GetRouteData(route: Route) {
        return route.data as RouteData;
    }
}
