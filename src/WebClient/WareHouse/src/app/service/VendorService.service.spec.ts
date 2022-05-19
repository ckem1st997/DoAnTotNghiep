/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { VendorServiceService } from './VendorService.service';

describe('Service: VendorService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [VendorServiceService]
    });
  });

  it('should ...', inject([VendorServiceService], (service: VendorServiceService) => {
    expect(service).toBeTruthy();
  }));
});
