import { Component } from '@angular/core';
import {MatIconModule} from '@angular/material/icon';
import {MatButtonModule} from '@angular/material/button';

@Component({
  selector: 'app-card-product-cart',
  standalone: true,
  imports: [MatIconModule,MatButtonModule],
  templateUrl: './card-product-cart.component.html',
  styleUrl: './card-product-cart.component.scss'
})
export class CardProductCartComponent {

}
