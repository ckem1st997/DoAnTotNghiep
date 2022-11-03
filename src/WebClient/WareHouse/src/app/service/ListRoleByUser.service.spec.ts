/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { ListRoleByUserService } from './ListRoleByUser.service';

describe('Service: ListRoleByUser', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ListRoleByUserService]
    });
  });

  it('should ...', inject([ListRoleByUserService], (service: ListRoleByUserService) => {
    expect(service).toBeTruthy();
  }));
});
