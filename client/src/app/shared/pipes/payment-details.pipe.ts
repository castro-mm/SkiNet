import { Pipe, PipeTransform } from '@angular/core';
import { ConfirmationToken } from '@stripe/stripe-js';
import { PaymentSummary } from '../models/order';

@Pipe({
    name: 'paymentDetails',
    standalone: true
})
export class PaymentDetailsPipe implements PipeTransform {

    transform(value: ConfirmationToken['payment_method_preview'] | PaymentSummary | undefined, ...args: unknown[]): unknown {
        if(value && 'card' in value) {
            const paymentDetails = value.card;
            if(paymentDetails) {
                return `
                    ${paymentDetails?.brand.toLocaleUpperCase()} ${paymentDetails.funding.toLocaleUpperCase()}, 
                    **** **** **** ${paymentDetails.last4}, 
                    Exp.: ${paymentDetails.exp_month}/${paymentDetails.exp_year} - 
                    ${paymentDetails.country}
                    `;
            } else {
                return 'Unknown payment details';
            }
        } else if(value && 'last4' in value) {
            const paymentDetails = value as PaymentSummary;
            if (paymentDetails){
                return `${paymentDetails?.brand.toLocaleUpperCase()}, **** **** **** ${paymentDetails.last4}, Exp.: ${paymentDetails.expMonth}/${paymentDetails.expYear}`;
            } else {
                return 'Unknown payment details';
            }
        } else {
            return 'Unknown Payment Method'
        }
    }
}
