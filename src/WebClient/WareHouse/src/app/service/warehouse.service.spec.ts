/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { WarehouseService } from './warehouse.service';

describe('Service: Warehouse', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [WarehouseService]
    });
  });

  it('should ...', inject([WarehouseService], (service: WarehouseService) => {
    expect(service).toBeTruthy();
  }));
});
