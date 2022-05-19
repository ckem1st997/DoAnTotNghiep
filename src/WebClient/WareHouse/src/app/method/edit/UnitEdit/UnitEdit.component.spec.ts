/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { UnitEditComponent } from './UnitEdit.component';

describe('UnitEditComponent', () => {
  let component: UnitEditComponent;
  let fixture: ComponentFixture<UnitEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UnitEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UnitEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
