import { Component } from '@angular/core';
import { MatBadge } from '@angular/material/badge';
import { MatButton } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';
import { MatProgressBar } from '@angular/material/progress-bar';
import { RouterLink, RouterLinkActive, RouterModule } from '@angular/router';
import { BusyService } from '../../core/services/busy.service';
import { CartService } from '../../core/services/cart.service';

@Component({
    selector: 'app-header',
    standalone: true,
    imports: [
        MatIcon, 
        MatButton,
        MatBadge,
        RouterLink,
        RouterLinkActive,
        RouterModule,
        MatProgressBar
    ],
    templateUrl: './header.component.html',
    styleUrl: './header.component.scss'
})
export class HeaderComponent {
    constructor (public busyService: BusyService, public cartService: CartService) { }

}
