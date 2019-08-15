import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';

import { Router } from '@angular/router';

@Injectable({
    providedIn: 'root'
})
export class UserService {
    readonly BASE_URL = 'https://localhost:44379/api/';

    constructor(private fb: FormBuilder, private http: HttpClient, private toastrService: ToastrService, private router: Router) {

    }

    formModel = this.fb.group({
        Email: ['test1@mail.com', [Validators.required, Validators.email]],
        Passwords: this.fb.group({
            Password: ['111', [Validators.required, Validators.minLength(3)]],
            ConfirmPassword: ['111', [Validators.required]]
        }, { validator: this.comparePasswords })
    });

    loginModel = this.fb.group({
        Email: ['test1@mail.com', [Validators.required, Validators.email]],
        Password: ['111', [Validators.required]]
    });

    comparePasswords(fb: FormGroup) {
        let password = fb.get('Password');
        let cPassword = fb.get('ConfirmPassword');
        // passwordMismatch
        if (cPassword.errors == null || 'passwordMismatch' in cPassword.errors) {
            if (password.value != cPassword.value)
                cPassword.setErrors({ passwordMismatch: true });
            else
                cPassword.setErrors(null);
        }
    }

    register() {
        this.toastrService.info('Registering!');
        var body = {
            Email: this.formModel.value.Email,
            Password: this.formModel.value.Passwords.Password
        };

        return this.http.post(this.BASE_URL + 'account/register', body);
    }

    login() {
        var body = {
            Email: this.loginModel.value.Email,
            Password: this.loginModel.value.Password
        };

        return this.http.post(this.BASE_URL + 'account/login', body);
    }

    getProfile() {
        return this.http.get(this.BASE_URL + 'account/getProfile');
    }
}
