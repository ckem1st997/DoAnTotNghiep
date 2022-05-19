/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { FormSearchWareHouseBookComponent } from './formSearchWareHouseBook.component';

describe('FormSearchWareHouseBookComponent', () => {
  let component: FormSearchWareHouseBookComponent;
  let fixture: ComponentFixture<FormSearchWareHouseBookComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FormSearchWareHouseBookComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FormSearchWareHouseBookComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
