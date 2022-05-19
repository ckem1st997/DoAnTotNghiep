/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { VendorEditComponent } from './VendorEdit.component';

describe('VendorEditComponent', () => {
  let component: VendorEditComponent;
  let fixture: ComponentFixture<VendorEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ VendorEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VendorEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
