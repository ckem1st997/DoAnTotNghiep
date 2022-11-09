/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { ListAuthozireCreateComponentComponent } from './ListAuthozireCreateComponent.component';

describe('ListAuthozireCreateComponentComponent', () => {
  let component: ListAuthozireCreateComponentComponent;
  let fixture: ComponentFixture<ListAuthozireCreateComponentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ListAuthozireCreateComponentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ListAuthozireCreateComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
