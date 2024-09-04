import { Component, OnInit } from '@angular/core';
import { OrderService } from '../../../core/services/order.service';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { Order } from '../../../shared/models/order';
import { MatCard, MatCardModule } from '@angular/material/card';
import { MatButton } from '@angular/material/button';
import { CurrencyPipe, DatePipe } from '@angular/common';
import { AddressPipe } from "../../../shared/pipes/address.pipe";
import { PaymentDetailsPipe } from "../../../shared/pipes/payment-details.pipe";

@Component({
    selector: 'app-order-detailed',
    standalone: true,
    imports: [MatCardModule, MatButton, DatePipe, CurrencyPipe, AddressPipe, PaymentDetailsPipe, RouterLink],
    templateUrl: './order-detailed.component.html',
    styleUrl: './order-detailed.component.scss'
})
export class OrderDetailedComponent implements OnInit {
    order?: Order;

    constructor(private orderService: OrderService, private activatedRoute: ActivatedRoute) { }

    ngOnInit(): void {
        this.loadOrder();
    }

    loadOrder() {
        const id = this.activatedRoute.snapshot.paramMap.get('id');
        if (!id) return;

        this.orderService.getOrderDetailed(+id).subscribe({
            next: (order: Order) => this.order = order
        });
    }
}
