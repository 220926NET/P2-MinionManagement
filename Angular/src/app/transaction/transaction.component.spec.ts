import { ComponentFixture, TestBed } from '@angular/core/testing';
import { InternalAPIService } from '../API-Service/internal-api.service';
import { TransactionComponent } from './transaction.component';
import { HttpClient} from '@angular/common/http'

describe('TransactionComponent', () => {
  let component: TransactionComponent;
  let fixture: ComponentFixture<TransactionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TransactionComponent ],
    })
    .compileComponents();

    fixture = TestBed.createComponent(TransactionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
