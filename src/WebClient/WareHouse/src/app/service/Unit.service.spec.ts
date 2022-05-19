/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { UnitService } from './Unit.service';

describe('Service: Unit', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [UnitService]
    });
  });

  it('should ...', inject([UnitService], (service: UnitService) => {
    expect(service).toBeTruthy();
  }));
});
