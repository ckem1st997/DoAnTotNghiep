/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { WareHouseItemCategoryService } from './WareHouseItemCategory.service';

describe('Service: WareHouseItemCategory', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [WareHouseItemCategoryService]
    });
  });

  it('should ...', inject([WareHouseItemCategoryService], (service: WareHouseItemCategoryService) => {
    expect(service).toBeTruthy();
  }));
});
