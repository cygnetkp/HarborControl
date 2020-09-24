import { TestBed } from '@angular/core/testing';

import { HarborControlService } from './harbor-control.service';

describe('HarborControlService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: HarborControlService = TestBed.get(HarborControlService);
    expect(service).toBeTruthy();
  });
});
