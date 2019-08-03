import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { HttpClient } from '@angular/common/http';

@Injectable({
    providedIn: 'root'
})
export class UserService {
    readonly BASE_URL = 'https://localhost:44379/api/';

    constructor(private fb: FormBuilder, private http: HttpClient) {

    }

    formModel = this.fb.group({
        Email: ['', [Validators.required, Validators.email]],
        Passwords: this.fb.group({
            Password: ['', [Validators.required, Validators.minLength(3)]],
            ConfirmPassword: ['', [Validators.required]]
        }, { validator: this.comparePasswords })
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
        var body = {
            Email: this.formModel.value.Email,
            Password: this.formModel.value.Passwords.Password
        };

        return this.http.post(this.BASE_URL + 'account/register', body);
    }
}
