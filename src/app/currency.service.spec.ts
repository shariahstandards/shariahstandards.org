/* tslint:disable:no-unused-variable */

import { addProviders, async, inject } from '@angular/core/testing';
import { CurrencyService } from './currency.service';

describe('Service: Currency', () => {
  beforeEach(() => {
    addProviders([CurrencyService]);
  });

  it('should ...',
    inject([CurrencyService],
      (service: CurrencyService) => {
        expect(service).toBeTruthy();
      }));
});
