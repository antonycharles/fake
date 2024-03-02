import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { Product } from '../../Types/product';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor() { }

  private  products:Product[] = [
    {
      id:1,
      title:"Robot Moka",
      description:"Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s.",
      slug:"robot-moka",
      image:"http://localhost:4200/assets/imagens/050-hand.png",
      price:25
    },
    {
      id:2,
      title:"Kita",
      description:"Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s.",
      slug:"kita",
      image:"http://localhost:4200/assets/imagens/050-hand.png",
      price:35
    },
    {
      id:3,
      title:"Types robot",
      description:"Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s.",
      slug:"types-robot",
      image:"http://localhost:4200/assets/imagens/050-hand.png",
      price:211
    },
    {
      id:4,
      title:"Pito",
      description:"Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s.",
      slug:"pito",
      image:"http://localhost:4200/assets/imagens/050-hand.png",
      price:45
    },
    {
      id:5,
      title:"Camero",
      description:"Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s.",
      slug:"camero",
      image:"http://localhost:4200/assets/imagens/050-hand.png",
      price:78
    }
  ];

  get() : Observable<Product[]> {
    return of(this.products);
  }

  getBySlug(slug:string) : Observable<Product>{
    let product = this.products.find(x => x.slug == slug);
    return of(product!);
  }
}
