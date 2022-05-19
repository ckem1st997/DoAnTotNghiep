/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { LoadingService } from './Loading.service';

describe('Service: Loading', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [LoadingService]
    });
  });

  it('should ...', inject([LoadingService], (service: LoadingService) => {
    expect(service).toBeTruthy();
  }));
});
