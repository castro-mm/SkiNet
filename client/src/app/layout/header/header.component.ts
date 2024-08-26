import { Component } from '@angular/core';
import { MatBadge } from '@angular/material/badge';
import { MatButton } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';
import { MatProgressBar } from '@angular/material/progress-bar';
import { Router, RouterLink, RouterLinkActive, RouterModule } from '@angular/router';
import { BusyService } from '../../core/services/busy.service';
import { CartService } from '../../core/services/cart.service';
import { AccountService } from '../../core/services/account.service';
import { MatMenu, MatMenuItem, MatMenuTrigger } from '@angular/material/menu';
import { MatDivider } from '@angular/material/divider';

@Component({
    selector: 'app-header',
    standalone: true,
    imports: [ MatIcon, MatButton, MatBadge, RouterLink, RouterLinkActive, RouterModule, MatProgressBar, MatMenu, MatMenuTrigger, MatDivider, MatMenuItem ],
    templateUrl: './header.component.html',
    styleUrl: './header.component.scss'
})
export class HeaderComponent {
    constructor (public busyService: BusyService, public cartService: CartService, public accountService: AccountService, private router: Router) { }

    logout() {
        this.accountService.logout().subscribe({
            next: () => {
                this.accountService.currentUser.set(null);
                this.router.navigateByUrl('/');
            }
        })
    }
}
