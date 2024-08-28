import { CartItem } from "./cart-item";
import { nanoid } from 'nanoid'

export class Cart {
    id: string = nanoid(); // generates a random id
    items: CartItem[] = [];
    deliveryMethodId?: number;
    paymentIntentId?: string;
    clientSecret?: string;
}