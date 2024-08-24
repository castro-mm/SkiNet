import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { MatButton } from '@angular/material/button';

@Component({
    selector: 'app-test-error',
    standalone: true,
    imports: [MatButton],
    templateUrl: './test-error.component.html',
    styleUrl: './test-error.component.scss'
})
export class TestErrorComponent {
    baseUrl: string = 'https://localhost:5010/api/';
    validationErrors?: string[];

    constructor (private http: HttpClient) { }

    get404Error() {
        this.http.get(`${this.baseUrl}buggy/notfound`).subscribe({
            next: (response) => console.log(response),
            error: (err) => console.log(err)
        });
    }
    get400Error() {
        this.http.get(`${this.baseUrl}buggy/badrequest`).subscribe({
            next: (response) => console.log(response),
            error: (err) => console.log(err)
        });
    }
    get401Error() {
        this.http.get(`${this.baseUrl}buggy/unauthorized`).subscribe({
            next: (response) => console.log(response),
            error: (err) => console.log(err)
        });
    }
    get500Error() {
        this.http.get(`${this.baseUrl}buggy/internalerror`).subscribe({
            next: (response) => console.log(response),
            error: (err) => console.log(err)
        });
    }
    get400ValidationError() {
        this.http.post(`${this.baseUrl}buggy/validationerror`, {}).subscribe({
            next: (response) => console.log(response),
            error: (err) => this.validationErrors = err
        });
    }
}
