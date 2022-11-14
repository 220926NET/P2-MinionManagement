import { TestBed } from '@angular/core/testing';

import { InternalAPIService } from './internal-api.service';

describe('InternalAPIService', () => {
  let service: InternalAPIService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(InternalAPIService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
