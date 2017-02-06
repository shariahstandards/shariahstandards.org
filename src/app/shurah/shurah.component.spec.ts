/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { ShurahComponent } from './shurah.component';

describe('ShurahComponent', () => {
  let component: ShurahComponent;
  let fixture: ComponentFixture<ShurahComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ShurahComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ShurahComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
