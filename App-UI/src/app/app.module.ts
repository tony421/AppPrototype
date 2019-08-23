import { BrowserModule } from '@angular/platform-browser';
import { NgModule, ErrorHandler } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { UserComponent } from './user/user.component';
import { RegisterComponent } from './user/register/register.component';
import { UserService } from './services/user.service';
import { LoginComponent } from './user/login/login.component';
import { ProfileComponent } from './user/profile/profile.component';
import { AuthInterceptor } from './auth/auth.interceptor';
import { AppErrorInterceptor } from './app-error-intercecptor';
import { AppErrorHandler } from './app-error-handler';

import { FontAwesomeModule, FaIconLibrary } from '@fortawesome/angular-fontawesome';
import { faCannabis, faArrowLeft, faCoffee } from '@fortawesome/free-solid-svg-icons';
import { SidebarComponent } from './nav/sidebar/sidebar.component';
import { TopbarComponent } from './nav/topbar/topbar.component';
//import { far } from '@fortawesome/free-regular-svg-icons';
//import { fab } from '@fortawesome/free-brands-svg-icons';

@NgModule({
    declarations: [
        AppComponent,
        UserComponent,
        RegisterComponent,
        LoginComponent,
        ProfileComponent,
        SidebarComponent,
        TopbarComponent
    ],
    imports: [
        BrowserModule,
        HttpClientModule,
        AppRoutingModule,
        ReactiveFormsModule,
        BrowserAnimationsModule,
        FontAwesomeModule,
        ToastrModule.forRoot(),
    ],
    providers: [
        UserService,
        { provide: ErrorHandler, useClass: AppErrorHandler },
        { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
        { provide: HTTP_INTERCEPTORS, useClass: AppErrorInterceptor, multi: true },        
    ],
    bootstrap: [AppComponent]
})
export class AppModule {
    constructor(faLibrary: FaIconLibrary) {
        faLibrary.addIcons(faCannabis, faArrowLeft, faCoffee);
    }
}
