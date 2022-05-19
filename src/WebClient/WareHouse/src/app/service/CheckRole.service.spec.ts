/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { CheckRoleService } from './CheckRole.service';

describe('Service: CheckRole', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [CheckRoleService]
    });
  });

  it('should ...', inject([CheckRoleService], (service: CheckRoleService) => {
    expect(service).toBeTruthy();
  }));
});
