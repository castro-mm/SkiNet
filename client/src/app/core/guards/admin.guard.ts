import { CanActivateFn, Router } from '@angular/router';
import { AccountService } from '../services/account.service';
import { inject } from '@angular/core';
import { SnackbarService } from '../services/snackbar.service';

export const adminGuard: CanActivateFn = (route, state) => {
    const accountService = inject(AccountService);
    const router = inject(Router);
    const snackbar = inject(SnackbarService);

    if (!accountService.isAdmin()) {
        snackbar.error('Access Forbidden');
        router.navigateByUrl('/shop');
        return false;
    } 

    return true;
};
