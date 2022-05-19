/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { WareHouseItemCategoryDelelteComponent } from './WareHouseItemCategoryDelelte.component';

describe('WareHouseItemCategoryDelelteComponent', () => {
  let component: WareHouseItemCategoryDelelteComponent;
  let fixture: ComponentFixture<WareHouseItemCategoryDelelteComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ WareHouseItemCategoryDelelteComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WareHouseItemCategoryDelelteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
