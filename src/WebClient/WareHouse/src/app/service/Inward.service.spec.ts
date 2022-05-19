/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { InwardService } from './Inward.service';

describe('Service: Inward', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [InwardService]
    });
  });

  it('should ...', inject([InwardService], (service: InwardService) => {
    expect(service).toBeTruthy();
  }));
});
