import { Component, OnInit } from '@angular/core';
import { Product } from '../../../core/Types/product';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ProductService } from '../../../core/services/product/product.service';
import { ProductCardComponent } from '../product-card/product.card.component';

@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [CommonModule,ProductCardComponent,RouterModule],
  templateUrl: './product.list.component.html',
  styleUrl: './product.list.component.scss'
})
export class ProductListComponent implements OnInit {
  products!: Product[];

  constructor(
    private service: ProductService
  ){}

  ngOnInit(): void {
    this.service.get().subscribe(
      res => {
        this.products = res;
      }
    )
  }

}
