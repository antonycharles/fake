import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-product-cart',
  standalone: true,
  imports: [MatIconModule,MatButtonModule],
  templateUrl: './product.cart.component.html',
  styleUrl: './product.cart.component.scss'
})
export class ProductCartComponent {

}
