/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { ListAuthozireComponent } from './ListAuthozire.component';

describe('ListAuthozireComponent', () => {
  let component: ListAuthozireComponent;
  let fixture: ComponentFixture<ListAuthozireComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ListAuthozireComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ListAuthozireComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
