/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { InwarDetailsEditByServiceComponent } from './InwarDetailsEditByService.component';

describe('InwarDetailsEditByServiceComponent', () => {
  let component: InwarDetailsEditByServiceComponent;
  let fixture: ComponentFixture<InwarDetailsEditByServiceComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ InwarDetailsEditByServiceComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(InwarDetailsEditByServiceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
