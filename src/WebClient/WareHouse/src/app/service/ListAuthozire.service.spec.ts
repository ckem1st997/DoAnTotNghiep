/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { ListAuthozireService } from './ListAuthozire.service';

describe('Service: ListAuthozire', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ListAuthozireService]
    });
  });

  it('should ...', inject([ListAuthozireService], (service: ListAuthozireService) => {
    expect(service).toBeTruthy();
  }));
});
