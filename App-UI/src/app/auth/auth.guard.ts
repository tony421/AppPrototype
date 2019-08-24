import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, CanActivate, Router } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class AuthGuard implements CanActivate {
    constructor(private router: Router) {
    }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
        console.log('CanActivate', localStorage.getItem('token'));
        if (localStorage.getItem('token') != null)
        {
            return true;
        }
        else {
            this.router.navigateByUrl("/user/login");
            return false;
        }
    }
}
