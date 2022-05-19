/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { PagesForbieComponent } from './PagesForbie.component';

describe('PagesForbieComponent', () => {
  let component: PagesForbieComponent;
  let fixture: ComponentFixture<PagesForbieComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PagesForbieComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PagesForbieComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
