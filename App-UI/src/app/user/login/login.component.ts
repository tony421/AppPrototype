import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/services/user.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { BaseComponent } from 'src/app/components/base/base.component';


@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss']
})
export class LoginComponent extends BaseComponent implements OnInit {

    constructor(private service: UserService) { super(); }

    ngOnInit() {

    }

    onSubmit() {
        this.service.login().subscribe(
            (res: any) => {
                if (res.succeeded) {
                    localStorage.setItem("token", res.data);
                    this.router.navigateByUrl("/user/profile");
                    //this.toast.info(res.token);
                }
                else {
                    res.messages.forEach(i => {
                        let msg;
                        if (i.code)
                            msg = '[' + i.code + '] ' + i.description;
                        else
                            msg = i.description;
                        this.toast.warning(msg);
                    });
                }
            }
            , err => {
                this.toast.warning(err);
                // err.error.errors.forEach(i => {
                //     this.toast.error(i.code + ': ' + i.description);
                // });
            }
        );
    }
}
