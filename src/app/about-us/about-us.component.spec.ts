/* tslint:disable:no-unused-variable */

import { By }           from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import {
  beforeEach, beforeEachProviders,
  describe, xdescribe,
  expect, it, xit,
  async, inject
} from '@angular/core/testing';

import { AboutUsComponent } from './about-us.component';

describe('Component: AboutUs', () => {
  it('should create an instance', () => {
    let component = new AboutUsComponent();
    expect(component).toBeTruthy();
  });
});
