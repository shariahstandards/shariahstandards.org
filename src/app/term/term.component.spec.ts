/* tslint:disable:no-unused-variable */

import { By }           from '@angular/platform-browser';
import { DebugElement } from '@angular/core';
import { addProviders, async, inject } from 'jasmine-core';
import { TermComponent } from './term.component';

describe('Component: Term', () => {
  it('should create an instance', () => {
    let component = new TermComponent();
    expect(component).toBeTruthy();
  });
});
