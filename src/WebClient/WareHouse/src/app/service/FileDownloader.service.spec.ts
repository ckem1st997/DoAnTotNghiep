/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { FileDownloaderService } from './FileDownloader.service';

describe('Service: FileDownloader', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [FileDownloaderService]
    });
  });

  it('should ...', inject([FileDownloaderService], (service: FileDownloaderService) => {
    expect(service).toBeTruthy();
  }));
});
