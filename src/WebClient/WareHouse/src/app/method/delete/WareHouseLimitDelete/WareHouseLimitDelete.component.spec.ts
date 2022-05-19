/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { WareHouseLimitDeleteComponent } from './WareHouseLimitDelete.component';

describe('WareHouseLimitDeleteComponent', () => {
  let component: WareHouseLimitDeleteComponent;
  let fixture: ComponentFixture<WareHouseLimitDeleteComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ WareHouseLimitDeleteComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WareHouseLimitDeleteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
