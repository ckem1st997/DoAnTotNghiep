/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { WareHouseBenginingCreateDeleteComponent } from './WareHouseBenginingCreateDelete.component';

describe('WareHouseBenginingCreateDeleteComponent', () => {
  let component: WareHouseBenginingCreateDeleteComponent;
  let fixture: ComponentFixture<WareHouseBenginingCreateDeleteComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ WareHouseBenginingCreateDeleteComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WareHouseBenginingCreateDeleteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
