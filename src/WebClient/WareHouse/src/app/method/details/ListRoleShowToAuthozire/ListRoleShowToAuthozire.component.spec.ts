/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { ListRoleShowToAuthozireComponent } from './ListRoleShowToAuthozire.component';

describe('ListRoleShowToAuthozireComponent', () => {
  let component: ListRoleShowToAuthozireComponent;
  let fixture: ComponentFixture<ListRoleShowToAuthozireComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ListRoleShowToAuthozireComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ListRoleShowToAuthozireComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
