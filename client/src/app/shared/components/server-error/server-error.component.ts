import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MatCard } from '@angular/material/card';
import { Router } from '@angular/router';

@Component({
    selector: 'app-server-error',
    standalone: true,
    imports: [
        MatCard
    ],
    templateUrl: './server-error.component.html',
    styleUrl: './server-error.component.scss'
})
export class ServerErrorComponent {
    error?: any;

    constructor(private router: Router) { 
        // ATENTION: to get the navigation error from the router interceptor, it's only possible through the contructor. It doesn't work using ngOnInit.
        const navigation = this.router.getCurrentNavigation();
        this.error = navigation?.extras.state?.['error'];
    }

}
