import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButton } from '@angular/material/button';
import { MatCard } from '@angular/material/card';
import { MatFormField, MatLabel } from '@angular/material/form-field';
import { MatInput } from '@angular/material/input';
import { AccountService } from '../../../core/services/account.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
    selector: 'app-login',
    standalone: true,
    imports: [
        ReactiveFormsModule,
        MatCard,
        MatFormField,
        MatInput,
        MatLabel,
        MatButton
    ],
    templateUrl: './login.component.html',
    styleUrl: './login.component.scss'
})
export class LoginComponent implements OnInit {
    loginForm: any = {};
    returnUrl: string = '/shop';

    constructor(private fb: FormBuilder, private accountService: AccountService, private router: Router, private activatedRoute: ActivatedRoute) {
        const url = this.activatedRoute.snapshot.queryParams['returnUrl'];
        if (url) this.returnUrl = url;
    }

    ngOnInit(): void {
        this.loginForm = this.fb.group({
            email: ['', Validators.required],
            password: ['', Validators.required]
        });
    }

    onSubmit() {
        this.accountService.login(this.loginForm.value).subscribe({
            next: () => {
                this.accountService.getUserInfo().subscribe();
                this.router.navigateByUrl(this.returnUrl);
            }
        });
    }
}
