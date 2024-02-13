import { Component } from '@angular/core';
import { ContainerComponent } from '../../shared/container/container.component';
import {MatButtonModule} from '@angular/material/button';
import { CardProductComponent } from '../../shared/card-product/card-product.component';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-product',
  standalone: true,
  imports: [ContainerComponent, MatButtonModule,CardProductComponent,RouterModule],
  templateUrl: './product.component.html',
  styleUrl: './product.component.scss'
})
export class ProductComponent {

}
