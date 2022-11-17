import { ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { InternalAPIService } from '../API-Service/internal-api.service';
import { RaidComponent } from './raid.component';

describe('RaidComponent', () => {
  let component: RaidComponent;
  let fixture: ComponentFixture<RaidComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RaidComponent ],
      imports : [HttpClientTestingModule]
    })
    .compileComponents();

    const apiServes = TestBed.inject(InternalAPIService);
    fixture = TestBed.createComponent(RaidComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
