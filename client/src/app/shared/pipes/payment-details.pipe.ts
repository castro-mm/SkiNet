import { Pipe, PipeTransform } from '@angular/core';
import { ConfirmationToken } from '@stripe/stripe-js';

@Pipe({
    name: 'paymentDetails',
    standalone: true
})
export class PaymentDetailsPipe implements PipeTransform {

    transform(value: ConfirmationToken['payment_method_preview'] | undefined, ...args: unknown[]): unknown {
        if(value?.type) {
            if (value.type === 'card') {
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
            } else {
                return 'Invalid Payment Type'
            }
        }
        else {
            return 'Unknown Payment Type'
        }
    }
}
