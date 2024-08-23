import { Component, inject, OnInit } from '@angular/core';
import { ShopService } from '../../../core/services/shop.service';
import { MatDivider } from '@angular/material/divider';
import { MatListOption, MatSelectionList } from '@angular/material/list';
import { MatButton } from '@angular/material/button';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FormsModule } from '@angular/forms';

@Component({
    selector: 'app-filters-dialog',
    standalone: true,
    imports: [
        MatDivider,
        MatSelectionList,
        MatListOption,
        MatButton,
        FormsModule
    ],
    templateUrl: './filters-dialog.component.html',
    styleUrl: './filters-dialog.component.scss'
})
export class FiltersDialogComponent {
    data = inject(MAT_DIALOG_DATA);
    
    selectedBrands: string[] = this.data.selectedBrands;
    selectedTypes: string[] = this.data.selectedTypes;
    
    constructor(public shopService: ShopService, private dialogRef: MatDialogRef<FiltersDialogComponent>) { }

    applyFilters() {
        this.dialogRef.close({
            selectedBrands: this.selectedBrands,
            selectedTypes: this.selectedTypes
        })
    }
}