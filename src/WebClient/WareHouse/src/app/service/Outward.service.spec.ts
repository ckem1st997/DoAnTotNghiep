/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { OutwardService } from './Outward.service';

describe('Service: Outward', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [OutwardService]
    });
  });

  it('should ...', inject([OutwardService], (service: OutwardService) => {
    expect(service).toBeTruthy();
  }));
});
