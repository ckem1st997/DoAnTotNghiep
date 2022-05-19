/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { OutwardEditComponent } from './OutwardEdit.component';

describe('OutwardEditComponent', () => {
  let component: OutwardEditComponent;
  let fixture: ComponentFixture<OutwardEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OutwardEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OutwardEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
