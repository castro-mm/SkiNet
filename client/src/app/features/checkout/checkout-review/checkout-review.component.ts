import { Component, Input } from '@angular/core';
import { CartService } from '../../../core/services/cart.service';
import { CurrencyPipe } from '@angular/common';
import { ConfirmationToken } from '@stripe/stripe-js';
import { AddressPipe } from '../../../shared/pipes/address.pipe';
import { PaymentDetailsPipe } from '../../../shared/pipes/payment-details.pipe';

@Component({
    selector: 'app-checkout-review',
    standalone: true,
    imports: [CurrencyPipe, AddressPipe, PaymentDetailsPipe],
    templateUrl: './checkout-review.component.html',
    styleUrl: './checkout-review.component.scss'
})
export class CheckoutReviewComponent {
    @Input() confirmationToken?: ConfirmationToken;

    constructor (public cartService: CartService) { }
}
