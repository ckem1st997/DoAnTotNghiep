/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { InwardEditComponent } from './InwardEdit.component';

describe('InwardEditComponent', () => {
  let component: InwardEditComponent;
  let fixture: ComponentFixture<InwardEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ InwardEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(InwardEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
