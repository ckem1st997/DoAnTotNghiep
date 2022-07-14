/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { MasterGetService } from './MasterGet.service';

describe('Service: MasterGet', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [MasterGetService]
    });
  });

  it('should ...', inject([MasterGetService], (service: MasterGetService) => {
    expect(service).toBeTruthy();
  }));
});
