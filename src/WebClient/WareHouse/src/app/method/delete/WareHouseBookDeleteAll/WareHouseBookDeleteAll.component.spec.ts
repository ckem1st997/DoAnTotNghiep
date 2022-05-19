/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { WareHouseBookDeleteAllComponent } from './WareHouseBookDeleteAll.component';

describe('WareHouseBookDeleteAllComponent', () => {
  let component: WareHouseBookDeleteAllComponent;
  let fixture: ComponentFixture<WareHouseBookDeleteAllComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ WareHouseBookDeleteAllComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WareHouseBookDeleteAllComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
