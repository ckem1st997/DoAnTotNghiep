/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { WareHouseItemUnitService } from './WareHouseItemUnit.service';

describe('Service: WareHouseItemUnit', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [WareHouseItemUnitService]
    });
  });

  it('should ...', inject([WareHouseItemUnitService], (service: WareHouseItemUnitService) => {
    expect(service).toBeTruthy();
  }));
});
