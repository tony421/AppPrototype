import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/services/user.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';


@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

    constructor(private service: UserService, private toast: ToastrService, private router: Router) { }

    ngOnInit() {

    }

    onSubmit() {
        this.service.login().subscribe(
            (res: any) => {
                localStorage.setItem("token", res.data);
                this.router.navigateByUrl("/user/profile");
                //this.toast.info(res.token);
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
