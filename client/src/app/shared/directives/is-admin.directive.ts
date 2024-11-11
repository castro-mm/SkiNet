import { Directive, effect, inject, TemplateRef, ViewContainerRef } from '@angular/core';
import { AccountService } from '../../core/services/account.service';

@Directive({
    selector: '[appIsAdmin]', //*appIsAdmin
    standalone: true
})
export class IsAdminDirective {
    private templateRef = inject(TemplateRef);

    constructor(private accountService: AccountService, private viewContainerRef: ViewContainerRef) {
        effect(() => {
            if (this.accountService.isAdmin()) {
                this.viewContainerRef.createEmbeddedView(this.templateRef);
            } else {
                this.viewContainerRef.clear();
            }
        })
    }
}
