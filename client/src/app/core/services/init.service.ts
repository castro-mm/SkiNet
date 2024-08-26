import { Injectable } from '@angular/core';
import { CartService } from './cart.service';
import { forkJoin, of } from 'rxjs';
import { AccountService } from './account.service';

@Injectable({
    providedIn: 'root'
})
export class InitService {

    constructor(private cartService: CartService, private accountService: AccountService) { }

    init() {
        const cartId = localStorage.getItem('cart_id');
        const cart$ = cartId ? this.cartService.getCart(cartId) : of(null);

        // allows us to wait for multiple observables to complete and then emit their values in array at once.
        return forkJoin({
            cart: cart$,
            user: this.accountService.getUserInfo()
        });
    }
}
