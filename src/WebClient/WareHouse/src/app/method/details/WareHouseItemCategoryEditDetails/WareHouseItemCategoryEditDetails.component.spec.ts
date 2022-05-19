/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { WareHouseItemCategoryEditDetailsComponent } from './WareHouseItemCategoryEditDetails.component';

describe('WareHouseItemCategoryEditDetailsComponent', () => {
  let component: WareHouseItemCategoryEditDetailsComponent;
  let fixture: ComponentFixture<WareHouseItemCategoryEditDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ WareHouseItemCategoryEditDetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WareHouseItemCategoryEditDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
