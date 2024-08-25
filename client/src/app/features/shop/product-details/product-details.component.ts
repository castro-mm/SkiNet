import { Component, OnInit } from '@angular/core';
import { ShopService } from '../../../core/services/shop.service';
import { ActivatedRoute } from '@angular/router';
import { Product } from '../../../shared/models/product';
import { CurrencyPipe } from '@angular/common';
import { MatButton } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';
import { MatFormField, MatLabel } from '@angular/material/form-field';
import { MatInput } from '@angular/material/input';
import { MatDivider } from '@angular/material/divider';
import { CartService } from '../../../core/services/cart.service';
import { FormsModule } from '@angular/forms';

@Component({
    selector: 'app-product-details',
    standalone: true,
    imports: [
        CurrencyPipe,
        MatButton,
        MatIcon,
        MatFormField,
        MatInput,
        MatLabel,
        MatDivider,
        FormsModule
    ],
    templateUrl: './product-details.component.html',
    styleUrl: './product-details.component.scss'
})
export class ProductDetailsComponent implements OnInit {
    product?: Product;
    quantityInCart: number = 0;
    quantity: number = 1;

    constructor(private shopService: ShopService, private cartService: CartService, private activatedRoute: ActivatedRoute) { }

    ngOnInit(): void {
        this.loadProduct();
    }

    loadProduct() {
        const id = this.activatedRoute.snapshot.paramMap.get('id');
        if (!id) return;

        this.shopService.getProduct(+id).subscribe({
            next: (response) => {
                this.product = response;
                this.updateQuantityInCart();
            },
            error: (err) => console.log(err)
        });
    }

    updateQuantityInCart() {
        this.quantityInCart = this.cartService.cart()?.items.find(x => x.productId == this.product?.id)?.quantity || 0;
        this.quantity = this.quantityInCart || 1;
    }

    getButtonText() {
        return this.quantityInCart > 0 ? 'Update Cart' : 'Add to Cart';
    }

    updateCart() {
        if (!this.product) return;
        
        if (this.quantity > this.quantityInCart) {
            const quantityItemsToAdd = this.quantity - this.quantityInCart;
            this.quantityInCart += quantityItemsToAdd;
            this.cartService.addItemToCart(this.product, quantityItemsToAdd);
        } else {
            const quantityItemsToRemove = this.quantityInCart - this.quantity;
            this.quantityInCart -= quantityItemsToRemove;
            this.cartService.removeItemFromCart(this.product.id, quantityItemsToRemove);
        }
    }
}
