import { Component } from '@angular/core';
import { ContainerComponent } from '../../shared/container/container.component';
import {MatListModule} from '@angular/material/list';
import { CardProductCartComponent } from '../../shared/card-product-cart/card-product-cart.component';
import {MatDividerModule} from '@angular/material/divider';

@Component({
  selector: 'app-cart',
  standalone: true,
  imports: [ContainerComponent,MatListModule,CardProductCartComponent,MatDividerModule],
  templateUrl: './cart.component.html',
  styleUrl: './cart.component.scss'
})
export class CartComponent {

}
