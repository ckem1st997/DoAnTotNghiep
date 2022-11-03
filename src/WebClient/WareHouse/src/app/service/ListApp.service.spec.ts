/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { ListAppService } from './ListApp.service';

describe('Service: ListApp', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ListAppService]
    });
  });

  it('should ...', inject([ListAppService], (service: ListAppService) => {
    expect(service).toBeTruthy();
  }));
});
