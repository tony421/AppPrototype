import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/services/user.service';
import { ToastrService } from 'ngx-toastr';

@Component({
    selector: 'app-profile',
    templateUrl: './profile.component.html',
    styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {

    constructor(private userService: UserService, private toast: ToastrService) { }

    vm = {
        userId: "",
        email: ""
    }

    ngOnInit() {
        this.userService.getProfile().subscribe(
            (res: any) => {
                console.info(res);
                this.vm.userId = res.data.id;
                this.vm.email = res.data.email;
            }
            , err => { // using a function in "ComponentBase" implemention would be the best practice to handle error message
                if (err != '')
                    this.toast.warning(err);
            }
        );
    }
}
