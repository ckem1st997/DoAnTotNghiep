/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { WareHouseLimitService } from './WareHouseLimit.service';

describe('Service: WareHouseLimit', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [WareHouseLimitService]
    });
  });

  it('should ...', inject([WareHouseLimitService], (service: WareHouseLimitService) => {
    expect(service).toBeTruthy();
  }));
});
