import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { MatButton } from '@angular/material/button';
import { environment } from '../../../environments/environment';

@Component({
    selector: 'app-test-error',
    standalone: true,
    imports: [MatButton],
    templateUrl: './test-error.component.html',
    styleUrl: './test-error.component.scss'
})
export class TestErrorComponent {
    baseApiUrl: string = environment.apiUrl;
    validationErrors?: string[];

    constructor (private http: HttpClient) { }

    get404Error() {
        this.http.get(`${this.baseApiUrl}buggy/notfound`).subscribe({
            next: (response) => console.log(response),
            error: (err) => console.log(err)
        });
    }
    get400Error() {
        this.http.get(`${this.baseApiUrl}buggy/badrequest`).subscribe({
            next: (response) => console.log(response),
            error: (err) => console.log(err)
        });
    }
    get401Error() {
        this.http.get(`${this.baseApiUrl}buggy/unauthorized`).subscribe({
            next: (response) => console.log(response),
            error: (err) => console.log(err)
        });
    }
    get500Error() {
        this.http.get(`${this.baseApiUrl}buggy/internalerror`).subscribe({
            next: (response) => console.log(response),
            error: (err) => console.log(err)
        });
    }
    get400ValidationError() {
        this.http.post(`${this.baseApiUrl}buggy/validationerror`, {}).subscribe({
            next: (response) => console.log(response),
            error: (err) => this.validationErrors = err
        });
    }
}
