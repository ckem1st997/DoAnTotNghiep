/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { WareHouseBookService } from './WareHouseBook.service';

describe('Service: WareHouseBook', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [WareHouseBookService]
    });
  });

  it('should ...', inject([WareHouseBookService], (service: WareHouseBookService) => {
    expect(service).toBeTruthy();
  }));
});
