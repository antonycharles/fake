import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CardProductCartComponent } from './card-product-cart.component';

describe('CardProductCartComponent', () => {
  let component: CardProductCartComponent;
  let fixture: ComponentFixture<CardProductCartComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CardProductCartComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CardProductCartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
