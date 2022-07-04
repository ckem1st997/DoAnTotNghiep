/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { HttpCancelService } from './HttpCancel.service';

describe('Service: HttpCancel', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [HttpCancelService]
    });
  });

  it('should ...', inject([HttpCancelService], (service: HttpCancelService) => {
    expect(service).toBeTruthy();
  }));
});
