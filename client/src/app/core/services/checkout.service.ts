import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { DeliveryMethod } from '../../shared/models/delivery-method';
import { map, of } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class CheckoutService {
    baseApiUrl: string = environment.apiUrl;
    deliveryMethods: DeliveryMethod[] = [];

    constructor(private http: HttpClient) { }

    getDeliveryMethods() {
        if (this.deliveryMethods.length > 0) return of(this.deliveryMethods);
        return this.http.get<DeliveryMethod[]>(`${this.baseApiUrl}payments/delivery-methods`).pipe(
            map(methods => {
                this.deliveryMethods = methods.sort((a,b) => b.price - a.price);
                return methods;
            })
        )
    }
}
