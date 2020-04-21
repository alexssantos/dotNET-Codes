import { TestBed } from '@angular/core/testing';

import { SensorSerivceService } from './sensor.service';

describe('SensorSerivceService', () => {
  let service: SensorSerivceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SensorSerivceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
