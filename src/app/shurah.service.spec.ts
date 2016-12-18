/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { ShurahService } from './shurah.service';

describe('Service: Shurah', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ShurahService]
    });
  });

  it('should ...', inject([ShurahService], (service: ShurahService) => {
    expect(service).toBeTruthy();
  }));
});
