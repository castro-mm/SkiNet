import { Component, OnDestroy } from '@angular/core';
import { MatButton } from '@angular/material/button';
import { RouterLink } from '@angular/router';
import { SignalrService } from '../../../core/services/signalr.service';
import { MatProgressSpinner } from '@angular/material/progress-spinner';
import { CurrencyPipe, DatePipe, NgIf } from '@angular/common';
import { AddressPipe } from '../../../shared/pipes/address.pipe';
import { PaymentDetailsPipe } from '../../../shared/pipes/payment-details.pipe';
import { OrderService } from '../../../core/services/order.service';

@Component({
    selector: 'app-checkout-success',
    standalone: true,
    imports: [MatButton, RouterLink, MatProgressSpinner, DatePipe, AddressPipe, CurrencyPipe, PaymentDetailsPipe, NgIf],
    templateUrl: './checkout-success.component.html',
    styleUrl: './checkout-success.component.scss'
})
export class CheckoutSuccessComponent implements OnDestroy {

    constructor(public signalrService: SignalrService, private orderService: OrderService) {}
 
    ngOnDestroy(): void {
        this.orderService.orderCompleted = false;
        this.signalrService.orderSignal.set(null);
    }
}
