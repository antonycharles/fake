import { Component, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { ContainerComponent } from '../../shared/container/container.component';
import {MatButtonModule} from '@angular/material/button';
import { ActivatedRoute, NavigationEnd, Router, RouterModule } from '@angular/router';
import { ProductService } from '../../core/services/product/product.service';
import { Product } from '../../core/Types/product';
import { ProductListComponent } from '../../shared/product/product-list/product.list.component';

@Component({
  selector: 'app-product',
  standalone: true,
  imports: [ProductListComponent, ContainerComponent, MatButtonModule,RouterModule],
  templateUrl: './product.component.html',
  styleUrl: './product.component.scss'
})
export class ProductComponent implements OnInit, OnChanges {
  product!: Product

  constructor(
    private service: ProductService,
    private route: ActivatedRoute,
    private router:Router
  ){
  }
  ngOnChanges(changes: SimpleChanges): void {

    var slug = this.route.snapshot.paramMap.get('slug');
    console.log(slug);
  }

  ngOnInit(): void {
    var slug = this.route.snapshot.paramMap.get('slug');
    console.log(slug);
    this.service.getBySlug(slug!).subscribe(
      product => {
        this.product = product;
      }
    )
  }

}
