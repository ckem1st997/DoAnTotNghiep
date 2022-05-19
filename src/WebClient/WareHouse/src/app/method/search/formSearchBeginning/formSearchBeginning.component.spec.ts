/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { FormSearchBeginningComponent } from './formSearchBeginning.component';

describe('FormSearchBeginningComponent', () => {
  let component: FormSearchBeginningComponent;
  let fixture: ComponentFixture<FormSearchBeginningComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FormSearchBeginningComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FormSearchBeginningComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
