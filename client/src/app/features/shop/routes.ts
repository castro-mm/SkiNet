import { Route } from "@angular/router";
import { ProductDetailsComponent } from "./product-details/product-details.component";
import { ShopComponent } from "./shop.component";

export const shopRoutes: Route[] = [
    { path: '', component: ShopComponent },
    { path: ':id', component: ProductDetailsComponent }
];