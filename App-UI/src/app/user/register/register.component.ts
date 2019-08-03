import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/services/user.service';
import { ToastrService } from 'ngx-toastr';

@Component({
    selector: 'app-register',
    templateUrl: './register.component.html',
    styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

    constructor(private service: UserService, private toast: ToastrService) { }

    ngOnInit() {
    }

    onSubmit() {
        this.service.register().subscribe(
            (res: any) => {
                if (res.succeeded) {
                    this.toast.success(this.service.formModel.value.Email + ' is created.');
                    this.service.formModel.reset();
                }
                else {
                    res.errors.forEach(i => {
                        this.toast.error(i.code + ': ' + i.description);
                    });
                }
            },
            err => {
                console.log('Opps! error...');
                err.error.errors.forEach(i => {
                    this.toast.error(i.code + ': ' + i.description);
                });
            }
        );
    }
}
