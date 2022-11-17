import { ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { InternalAPIService } from '../API-Service/internal-api.service';
import { NavBarComponent } from './nav-bar.component';

describe('NavBarComponent', () => {
  let component: NavBarComponent;
  let fixture: ComponentFixture<NavBarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NavBarComponent ],
      imports : [HttpClientTestingModule]
    })
    .compileComponents();
    
    const apiServes = TestBed.inject(InternalAPIService);
    fixture = TestBed.createComponent(NavBarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  // test certain element exist at HTML
  it('should render navbar', () => {
    const fixture = TestBed.createComponent(NavBarComponent);
    fixture.detectChanges();
    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.querySelector('.navbar-brand')?.textContent).toContain('Minion Master');
  });

  // testing number of routeLink
  it('should four links', () => {
    const fixture = TestBed.createComponent(NavBarComponent);
    fixture.detectChanges();
    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.querySelectorAll('.nav-link')?.length).toBe(4);
  });
  
});
