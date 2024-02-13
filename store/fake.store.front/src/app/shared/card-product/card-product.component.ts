import { Component } from '@angular/core';
import {MatButtonModule} from '@angular/material/button';
import {MatCardModule} from '@angular/material/card';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-card-product',
  standalone: true,
  imports: [MatCardModule, MatButtonModule,RouterModule],
  templateUrl: './card-product.component.html',
  styleUrl: './card-product.component.scss'
})
export class CardProductComponent {

}
