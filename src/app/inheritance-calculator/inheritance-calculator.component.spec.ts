/* tslint:disable:no-unused-variable */

import { By }           from '@angular/platform-browser';
import { DebugElement } from '@angular/core';
import { addProviders, async, inject } from 'jasmine-core';
import { InheritanceCalculatorComponent } from './inheritance-calculator.component';

describe('Component: InheritanceCalculator', () => {
  it('should create an instance', () => {
    let component = new InheritanceCalculatorComponent();
    expect(component).toBeTruthy();
  });
});
