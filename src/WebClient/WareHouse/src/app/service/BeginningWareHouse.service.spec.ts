/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { BeginningWareHouseService } from './BeginningWareHouse.service';

describe('Service: BeginningWareHouse', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [BeginningWareHouseService]
    });
  });

  it('should ...', inject([BeginningWareHouseService], (service: BeginningWareHouseService) => {
    expect(service).toBeTruthy();
  }));
});
