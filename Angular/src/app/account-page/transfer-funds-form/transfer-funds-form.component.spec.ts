import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TransferFundsFormComponent } from './transfer-funds-form.component';

describe('TransferFundsFormComponent', () => {
  let component: TransferFundsFormComponent;
  let fixture: ComponentFixture<TransferFundsFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TransferFundsFormComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TransferFundsFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
