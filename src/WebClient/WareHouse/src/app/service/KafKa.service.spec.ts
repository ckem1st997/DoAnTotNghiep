/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { KafKaService } from './KafKa.service';

describe('Service: KafKa', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [KafKaService]
    });
  });

  it('should ...', inject([KafKaService], (service: KafKaService) => {
    expect(service).toBeTruthy();
  }));
});
