/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { GetDataToGprcService } from './GetDataToGprc.service';

describe('Service: GetDataToGprc', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [GetDataToGprcService]
    });
  });

  it('should ...', inject([GetDataToGprcService], (service: GetDataToGprcService) => {
    expect(service).toBeTruthy();
  }));
});
