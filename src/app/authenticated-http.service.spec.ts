/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { AuthenticatedHttpService } from './authenticated-http.service';

describe('Service: AuthenticatedHttp', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [AuthenticatedHttpService]
    });
  });

  it('should ...', inject([AuthenticatedHttpService], (service: AuthenticatedHttpService) => {
    expect(service).toBeTruthy();
  }));
});
