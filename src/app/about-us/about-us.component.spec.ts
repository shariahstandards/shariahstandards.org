/* tslint:disable:no-unused-variable */

import { By }           from '@angular/platform-browser';
import { DebugElement } from 'jasmine-core';

import {
  beforeEach, beforeEachProviders,
  describe, xdescribe,
  expect, it, xit,
  async, inject
} from 'jasmine-core';

import { AboutUsComponent } from './about-us.component';

describe('Component: AboutUs', () => {
  it('should create an instance', () => {
    let component = new AboutUsComponent();
    expect(component).toBeTruthy();
  });
});
