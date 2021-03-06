import { Component, OnInit } from '@angular/core';

declare var $: any;

@Component({
    selector: 'app-sidebar',
    templateUrl: './sidebar.component.html',
    styleUrls: ['./sidebar.component.scss']
})
export class SidebarComponent implements OnInit {

    constructor() { }

    ngOnInit() {
        $(document).ready(function () {
            $("#sidebar-overlay").mCustomScrollbar({
                theme: "minimal"
            });

            $('#sidebar-overlay #dismiss, .overlay').on('click', function () {
                // hide sidebar
                $('#sidebar-overlay').removeClass('active');
                // hide overlay
                $('.overlay').removeClass('active');
            });

            $('#sidebarCollapse').on('click', function () {
                // open navbar
                $('#sidebar-overlay').addClass('active');
                // fade in the overlay
                $('.overlay').addClass('active');
                // close dropdowns
                $('.collapse.in').toggleClass('in');
                // and also adjust aria-expanded attributes we use for the open/closed arrows in our CSS
                $('a[aria-expanded=true]').attr('aria-expanded', 'false');
            });
        });
    }
}
