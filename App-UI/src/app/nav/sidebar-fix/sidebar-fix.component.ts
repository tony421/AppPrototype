import { Component, OnInit } from '@angular/core';

declare var $: any;

@Component({
    selector: 'app-sidebar-fix',
    templateUrl: './sidebar-fix.component.html',
    styleUrls: ['./sidebar-fix.component.scss']
})
export class SidebarFixComponent implements OnInit {

    constructor() { }

    ngOnInit() {
        $(document).ready(function () {
            $("#sidebar-fix").mCustomScrollbar({
                theme: "minimal"
            });

            // $('#sidebar-fix #dismiss, .overlay').on('click', function () {
            //     // hide sidebar
            //     $('#sidebar-fix').removeClass('active');
            //     // hide sidebar
            //     $('#app-content').removeClass('active');
            // });

            $('#sidebar-toggle').on('click', function () {
                // Open the sidebar
                $('#sidebar-fix').toggleClass('active');
                // Reduce content width
                $('#app-content').toggleClass('active');

                $('#titlebar').toggleClass('active');
            });
        });
    }
}
