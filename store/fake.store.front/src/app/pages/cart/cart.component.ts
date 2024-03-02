import { Component } from '@angular/core';
import { ContainerComponent } from '../../shared/container/container.component';
import {MatListModule} from '@angular/material/list';
import {MatDividerModule} from '@angular/material/divider';
import { ProductCartComponent } from '../../shared/product/product-cart/product.cart.component';

@Component({
  selector: 'app-cart',
  standalone: true,
  imports: [ContainerComponent,MatListModule,ProductCartComponent,MatDividerModule],
  templateUrl: './cart.component.html',
  styleUrl: './cart.component.scss'
})
export class CartComponent {

}
