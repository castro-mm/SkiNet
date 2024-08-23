import { Component, OnInit } from '@angular/core';
import { Product } from '../../shared/models/product';
import { ShopService } from '../../core/services/shop.service';
import { ProductItemComponent } from "./product-item/product-item.component";
import { MatDialog } from '@angular/material/dialog';
import { FiltersDialogComponent } from './filters-dialog/filters-dialog.component';
import { MatButton } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';
import { MatMenu, MatMenuTrigger } from '@angular/material/menu';
import { MatListOption, MatSelectionList, MatSelectionListChange } from '@angular/material/list';
import { ShopParams } from '../../shared/models/shopParams';
import { Pagination } from '../../shared/models/pagination';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { FormsModule } from '@angular/forms';

@Component({
    selector: 'app-shop',
    standalone: true,
    imports: [
        ProductItemComponent,
        MatButton,
        MatIcon,
        MatMenu,
        MatSelectionList,
        MatListOption,
        MatMenuTrigger,
        MatPaginator,
        FormsModule

    ],
    templateUrl: './shop.component.html',
    styleUrl: './shop.component.scss'
})
export class ShopComponent implements OnInit {
    products?: Pagination<Product>;
    shopParams = new ShopParams();
    pageSizeOptions: number[] = [5,10,15,20];

    sortOptions: any[] = [
        { name: 'Alphabetical', value: 'name' },
        { name: 'Price: Low-High', value: 'priceAsc' },
        { name: 'Price: High-Low', value: 'priceDesc' },
    ];

    constructor(private shopService: ShopService, private dialogService: MatDialog) { }

    ngOnInit(): void {
        this.initialiseShop();
    }

    initialiseShop() {
        this.shopService.getBrands();
        this.shopService.getTypes();
        this.getProducts();
    }

    getProducts() {
        this.shopService.getProducts(this.shopParams)
            .subscribe({
                next: (response) => this.products = response,
                error: (err) => console.error(err)
            })
    }

    onSearchChange() {
        this.shopParams.pageIndex = 1;
        this.getProducts();
    }

    handlePageEvent(event: PageEvent) 
    {
        this.shopParams.pageIndex = event.pageIndex + 1;
        this.shopParams.pageSize = event.pageSize;
        this.getProducts();
    }

    onSortChange(event: MatSelectionListChange) {
        const selectedOption = event.options[0];
        if (selectedOption) {
            this.shopParams.sort = selectedOption.value;
            this.shopParams.pageIndex = 1;
            this.getProducts();
        }
    }

    openFiltersDialog() {
        const dialogRef = this.dialogService.open(FiltersDialogComponent, {
                minWidth: '500px',
                data: {
                    selectedBrands: this.shopParams.brands,
                    selectedTypes: this.shopParams.types
                }
            });

        dialogRef.afterClosed().subscribe({
            next: (result) => {
                if (result) {
                    console.log(result);
                    this.shopParams.brands = result.selectedBrands;
                    this.shopParams.types = result.selectedTypes;
                    this.shopParams.pageIndex = 1;
                    
                    // apply filters
                    this.getProducts();
                }
            }
        })
    }
}
