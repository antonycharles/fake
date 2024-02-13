import { Component } from '@angular/core';
import { ContainerComponent } from '../../shared/container/container.component';
import { CardProductComponent } from '../../shared/card-product/card-product.component';
import { BannerComponent } from '../../shared/banner/banner.component';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CardProductComponent,ContainerComponent,BannerComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {

}
