import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { OrderParams } from '../../shared/models/order-params';
import { Pagination } from '../../shared/models/pagination';
import { Order } from '../../shared/models/order';

@Injectable({
    providedIn: 'root'
})
export class AdminService {
    baseApiUrl: string = environment.apiUrl;

    constructor(private http: HttpClient) { }

    getOrders(orderParams: OrderParams) {
        let params = new HttpParams();
        if (orderParams.filter && orderParams.filter !== 'All') {
            params = params.append('status', orderParams.filter);
        }
        params = params.append('pageIndex', orderParams.pageNumber);
        params = params.append('pageSize', orderParams.pageSize);

        return this.http.get<Pagination<Order>>(`${this.baseApiUrl}admin/orders`, { params });
    }

    getOrder(id: number) {
        return this.http.get<Order>(`${this.baseApiUrl}admin/orders/${id}`);
    }

    refundOrder(id: number) {
        return this.http.post<Order>(`${this.baseApiUrl}admin/orders/refund/${id}`, {});
    }
}
