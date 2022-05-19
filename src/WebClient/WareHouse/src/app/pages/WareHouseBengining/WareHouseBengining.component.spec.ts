/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { WareHouseBenginingComponent } from './WareHouseBengining.component';

describe('WareHouseBenginingComponent', () => {
  let component: WareHouseBenginingComponent;
  let fixture: ComponentFixture<WareHouseBenginingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ WareHouseBenginingComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WareHouseBenginingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
