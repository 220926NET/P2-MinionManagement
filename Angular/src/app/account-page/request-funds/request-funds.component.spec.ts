import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RequestFundsComponent } from './request-funds.component';

describe('RequestFundsComponent', () => {
  let component: RequestFundsComponent;
  let fixture: ComponentFixture<RequestFundsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RequestFundsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RequestFundsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
