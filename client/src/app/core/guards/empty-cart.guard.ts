import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { CartService } from '../services/cart.service';
import { SnackbarService } from '../services/snackbar.service';

export const emptyCartGuard: CanActivateFn = (route, state) => {
    const cartService = inject(CartService);
    const snackbar = inject(SnackbarService);
    const router = inject(Router);
    
    if (!cartService.cart() || cartService.cart()?.items.length === 0) {
        let ref = snackbar.error('Cart is empty');
        router.navigateByUrl('/cart'); // to secure that the route will not be redirected to the route of checkout page.

        ref.onAction().subscribe({
            next: () => {
                router.navigateByUrl('/shop');
                return false;        
            }
        })
    }
    return true;
};
