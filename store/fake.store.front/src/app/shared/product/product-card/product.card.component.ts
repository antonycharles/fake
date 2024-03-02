import { Component, Input, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { Router, RouterModule } from '@angular/router';
import { Product } from '../../../core/Types/product';

@Component({
  selector: 'app-product-card',
  standalone: true,
  imports: [MatCardModule, MatButtonModule,RouterModule],
  templateUrl: './product.card.component.html',
  styleUrl: './product.card.component.scss'
})
export class ProductCardComponent implements OnInit {
  @Input() product!: Product;

  constructor(private router: Router){}

  ngOnInit(): void {
    this.router.routeReuseStrategy.shouldReuseRoute = () => { return false; };
  }
}
