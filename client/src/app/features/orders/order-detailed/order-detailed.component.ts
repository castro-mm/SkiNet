import { Component, OnInit } from '@angular/core';
import { OrderService } from '../../../core/services/order.service';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { Order } from '../../../shared/models/order';
import { MatCard, MatCardModule } from '@angular/material/card';
import { MatButton } from '@angular/material/button';
import { CurrencyPipe, DatePipe } from '@angular/common';
import { AddressPipe } from "../../../shared/pipes/address.pipe";
import { PaymentDetailsPipe } from "../../../shared/pipes/payment-details.pipe";
import { AccountService } from '../../../core/services/account.service';
import { AdminService } from '../../../core/services/admin.service';

@Component({
    selector: 'app-order-detailed',
    standalone: true,
    imports: [MatCardModule, MatButton, DatePipe, CurrencyPipe, AddressPipe, PaymentDetailsPipe, RouterLink],
    templateUrl: './order-detailed.component.html',
    styleUrl: './order-detailed.component.scss'
})
export class OrderDetailedComponent implements OnInit {
    order?: Order;
    buttonText: string = '';

    constructor(private orderService: OrderService, private activatedRoute: ActivatedRoute, private accountService: AccountService, private adminService: AdminService, private router: Router) { 
    }

    ngOnInit(): void {
        this.buttonText = this.accountService.isAdmin() ? 'Return to Admin' : 'Return to Orders';
        this.loadOrder();
    }

    onReturnClick() {
        this.accountService.isAdmin()
            ? this.router.navigateByUrl('/admin')
            : this.router.navigateByUrl('/orders');
    }

    loadOrder() {
        const id = this.activatedRoute.snapshot.paramMap.get('id');
        if (!id) return;

        const loadOrderData = this.accountService.isAdmin()
            ? this.adminService.getOrder(+id)
            : this.orderService.getOrderDetailed(+id);

        loadOrderData.subscribe({
            next: (order: Order) => this.order = order
        });
    }
}
