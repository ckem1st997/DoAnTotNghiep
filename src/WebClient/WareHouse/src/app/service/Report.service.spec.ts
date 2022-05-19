/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { ReportService } from './Report.service';

describe('Service: Report', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ReportService]
    });
  });

  it('should ...', inject([ReportService], (service: ReportService) => {
    expect(service).toBeTruthy();
  }));
});
