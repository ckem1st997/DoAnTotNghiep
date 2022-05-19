/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { WareHouseItemUnitDelelteComponent } from './WareHouseItemUnitDelelte.component';

describe('WareHouseItemUnitDelelteComponent', () => {
  let component: WareHouseItemUnitDelelteComponent;
  let fixture: ComponentFixture<WareHouseItemUnitDelelteComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ WareHouseItemUnitDelelteComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WareHouseItemUnitDelelteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
