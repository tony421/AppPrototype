import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { CastingService } from 'src/app/services/casting.service';
import { AppInjector } from 'src/app/app-injector';

@Component({
    selector: 'app-base',
    templateUrl: './base.component.html',
    styleUrls: []
})
export class BaseComponent implements OnInit {
    protected router: Router;
    protected castingService: CastingService;
    protected toast: ToastrService;

    constructor() { 
        const injector = AppInjector.getInjector();    
        this.router = injector.get(Router);    
        this.castingService = injector.get(CastingService);
        this.toast = injector.get(ToastrService);
    }

    ngOnInit() {
    }
}
