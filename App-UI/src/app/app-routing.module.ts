import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { UserComponent } from './user/user.component';
import { RegisterComponent } from './user/register/register.component';
import { LoginComponent } from './user/login/login.component';
import { ProfileComponent } from './user/profile/profile.component';
import { AuthGuard } from './auth/auth.guard';


const routes: Routes = [
    { path: '', data: { breadcrumb: "Home" }, redirectTo: 'user/register', pathMatch: 'full' },
    {
        path: 'user', data: { breadcrumb: "User" }, component: UserComponent, children: [
            { path: 'register', data: { breadcrumb: "Register" }, component: RegisterComponent },
            { path: 'login', data: { breadcrumb: "Login" }, component: LoginComponent },
            { path: 'profile', data: { breadcrumb: "Profile", permittedRoles: ["Any"] }, component: ProfileComponent, canActivate: [AuthGuard] },
        ]
    }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
