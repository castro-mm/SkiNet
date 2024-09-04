import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment.development';
import { Order, OrderToCreate } from '../../shared/models/order';

@Injectable({
    providedIn: 'root'
})
export class OrderService {
    baseApiUrl: string = environment.apiUrl;
    orderCompleted: boolean = false;

    constructor(private http: HttpClient) { }

    createOrder(orderToCreate: OrderToCreate) {
        return this.http.post<Order>(`${this.baseApiUrl}orders`, orderToCreate);
    }

    getOrdersForUser() {
        return this.http.get<Order[]>(`${this.baseApiUrl}orders`);
    }

    getOrderDetailed(id: number) {
        return this.http.get<Order>(`${this.baseApiUrl}orders/${id}`);
    }
}
