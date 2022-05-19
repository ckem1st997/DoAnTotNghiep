/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { WareHouseItemService } from './WareHouseItem.service';

describe('Service: WareHouseItem', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [WareHouseItemService]
    });
  });

  it('should ...', inject([WareHouseItemService], (service: WareHouseItemService) => {
    expect(service).toBeTruthy();
  }));
});
