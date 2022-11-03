/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { ListRoleEditComponent } from './ListRoleEdit.component';

describe('ListRoleEditComponent', () => {
  let component: ListRoleEditComponent;
  let fixture: ComponentFixture<ListRoleEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ListRoleEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ListRoleEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
