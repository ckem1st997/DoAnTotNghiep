/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { OutwardetailsEditByServiceComponent } from './OutwardetailsEditByService.component';

describe('OutwardetailsEditByServiceComponent', () => {
  let component: OutwardetailsEditByServiceComponent;
  let fixture: ComponentFixture<OutwardetailsEditByServiceComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OutwardetailsEditByServiceComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OutwardetailsEditByServiceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
