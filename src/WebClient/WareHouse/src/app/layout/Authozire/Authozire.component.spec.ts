/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { AuthozireComponent } from './Authozire.component';

describe('AuthozireComponent', () => {
  let component: AuthozireComponent;
  let fixture: ComponentFixture<AuthozireComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AuthozireComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AuthozireComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
