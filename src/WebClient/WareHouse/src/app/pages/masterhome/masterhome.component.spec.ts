/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { MasterhomeComponent } from './masterhome.component';

describe('MasterhomeComponent', () => {
  let component: MasterhomeComponent;
  let fixture: ComponentFixture<MasterhomeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MasterhomeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MasterhomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
