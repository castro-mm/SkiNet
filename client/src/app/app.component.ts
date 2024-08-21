import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from "./layout/header/header.component";
import { HttpClient } from '@angular/common/http';
import { Product } from './shared/models/product';
import { Pagination } from './shared/models/pagination';

@Component({
    selector: 'app-root',
    standalone: true,
    imports: [RouterOutlet, HeaderComponent],
    templateUrl: './app.component.html',
    styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit {
    baseUrl: string = 'https://localhost:5010/api';
    title: string = 'SkiNet';
    products: Product[] = [];

    constructor(private http: HttpClient) { }

    ngOnInit(): void {
        this.http.get<Pagination<Product>>(this.baseUrl + '/products').subscribe({
            next: (response: any) => this.products = response.data,
            error: (err) => console.log(err),
            complete: () => console.log('completed')
        }) 
    }
}
