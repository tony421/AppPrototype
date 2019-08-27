import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, CanActivate, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { SharedLocalStorageService } from '../services/shared-local-storage.service';
import { CastingService } from '../services/casting.service';

@Injectable({
    providedIn: 'root'
})
export class AuthGuard implements CanActivate {
    constructor(private router: Router
        , private localStorageService: SharedLocalStorageService
        , private castingService: CastingService) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
        // Checking permission here if token is not null
        if (this.localStorageService.GetToken() != null)
        {
            // Parsing "route.data" to "RouteData" interface
            const routeData = this.castingService.GetRouteData(route.routeConfig);
            const pagePermittedRoles = routeData.permittedRoles;
            console.log('pagePermittedRoles:', pagePermittedRoles);
            
            // Parsing the decoded json to "TokenPayload" interface
            const payloadData = this.localStorageService.GetTokenPayload();
            //let payload = JSON.parse(window.atob(localStorage.getItem('token')));
            const userId = payloadData.userIdClaim
            const userRole = payloadData.roleClaim;
            console.log('payload:', payloadData);
            console.log('userId:', userId);
            console.log('userRole:', userRole);
            return true;
        }
        else {
            this.router.navigateByUrl("/user/login");
            return false;
        }
    }
}
