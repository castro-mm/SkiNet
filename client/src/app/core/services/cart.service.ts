import { computed, Injectable, signal } from '@angular/core';
import { environment } from '../../../environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { Cart } from '../../shared/models/cart';
import { CartItem } from '../../shared/models/cart-item';
import { Product } from '../../shared/models/product';
import { map } from 'rxjs';
import { DeliveryMethod } from '../../shared/models/delivery-method';

@Injectable({
    providedIn: 'root'
})
export class CartService {
    baseApiUrl: string = environment.apiUrl;
    cart = signal<Cart|null>(null);
    selectedDelivery = signal<DeliveryMethod | null>(null);

    constructor(private http: HttpClient) { }

    get itemsCount() {
        return computed(() => {
            return this.cart()?.items.reduce((sum, item) => sum + item.quantity, 0);
        })
    }

    get totals() {
        return computed(() => {
            const cart = this.cart();
            const delivery = this.selectedDelivery();
            if (!cart) return;
    
            const subtotal: number = cart.items.reduce((sum, item) => (sum + item.price) * item.quantity, 0);
            const shipping: number = delivery ? delivery.price : 0;
            const discount: number = 0;
    
            return { subtotal, shipping, discount, total: subtotal + shipping - discount }
        });
    }

    getCart(id: string) {
        return this.http.get<Cart>(`${this.baseApiUrl}cart?id=${id}`).pipe(
            map((cart) => {
                this.cart.set(cart);
                return cart;
            })
        );
    }

    setCart(cart: Cart) {
        return this.http.post<Cart>(`${this.baseApiUrl}cart`, cart).subscribe({
            next: (cart: Cart) => this.cart.set(cart)
        });
    }

    deleteCart() {
        this.http.delete(`${this.baseApiUrl}cart?id=${this.cart()?.id}`).subscribe({
            next: () => {
                localStorage.removeItem('cart_id');
                this.cart.set(null);
            }
        })
    }

    addItemToCart(item: CartItem|Product, quantity: number = 1) {
        const cart = this.cart() ?? this.createCart();
        if (this.isProduct(item)) {
            item = this.mapProductToCartItem(item);
        }
        cart.items = this.addOrUpdateItem(cart.items, item, quantity);
        this.setCart(cart);
    }

    removeItemFromCart(productId: number, quantity: number = 1) {
        const cart = this.cart();
        if (!cart) return;

        const index: number = cart.items.findIndex(x => x.productId == productId);
        if(index !== -1){
            if (cart.items[index].quantity > quantity) {
                cart.items[index].quantity -= quantity;
            } else {
                cart.items.splice(index, 1);
            }

            if (cart.items.length === 0) {
                this.deleteCart();
            } else {
                this.setCart(cart);
            }
        }
    }
    
    private addOrUpdateItem(items: CartItem[], item: CartItem, quantity: number): CartItem[] {
        const index = items.findIndex(x => x.productId == item.productId);
        if (index === -1) {
            item.quantity = quantity;
            items.push(item);
        } else {
            items[index].quantity += quantity;            
        }
        return items;
    }

    private mapProductToCartItem(item: Product): CartItem {
        return {
            productId: item.id,
            productName: item.name,
            price: item.price,
            quantity: 0,
            pictureUrl: item.pictureUrl,
            brand: item.brand,
            type: item.type
        };
    }

    private isProduct(item: CartItem|Product): item is Product {
        return (item as Product).id !== undefined;
    }

    private createCart(): Cart {
        const cart = new Cart();
        localStorage.setItem('cart_id', cart.id);

        return cart;
    }
}
