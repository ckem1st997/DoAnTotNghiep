/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { AuthozireService } from './Authozire.service';

describe('Service: Authozire', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [AuthozireService]
    });
  });

  it('should ...', inject([AuthozireService], (service: AuthozireService) => {
    expect(service).toBeTruthy();
  }));
});
