/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { FormSearchReportTotalComponent } from './FormSearchReportTotal.component';

describe('FormSearchReportTotalComponent', () => {
  let component: FormSearchReportTotalComponent;
  let fixture: ComponentFixture<FormSearchReportTotalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FormSearchReportTotalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FormSearchReportTotalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
