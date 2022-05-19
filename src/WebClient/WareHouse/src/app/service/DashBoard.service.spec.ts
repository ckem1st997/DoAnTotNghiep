/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { DashBoardService } from './DashBoard.service';

describe('Service: DashBoard', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [DashBoardService]
    });
  });

  it('should ...', inject([DashBoardService], (service: DashBoardService) => {
    expect(service).toBeTruthy();
  }));
});
