import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PurchaseTroopsComponent } from './purchase-troops.component';

describe('PurchaseTroopsComponent', () => {
  let component: PurchaseTroopsComponent;
  let fixture: ComponentFixture<PurchaseTroopsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PurchaseTroopsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PurchaseTroopsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
