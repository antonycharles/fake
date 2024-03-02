import { Component } from '@angular/core';
import { ContainerComponent } from '../../shared/container/container.component';
import { BannerComponent } from '../../shared/banner/banner.component';
import { ProductListComponent } from '../../shared/product/product-list/product.list.component';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [ProductListComponent,ContainerComponent,BannerComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {

}
