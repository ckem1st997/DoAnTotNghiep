/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { ListRoleService } from './ListRole.service';

describe('Service: ListRole', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ListRoleService]
    });
  });

  it('should ...', inject([ListRoleService], (service: ListRoleService) => {
    expect(service).toBeTruthy();
  }));
});
