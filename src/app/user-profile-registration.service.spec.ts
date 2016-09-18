/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { UserProfileRegistrationService } from './user-profile-registration.service';

describe('Service: UserProfileRegistration', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [UserProfileRegistrationService]
    });
  });

  it('should ...', inject([UserProfileRegistrationService], (service: UserProfileRegistrationService) => {
    expect(service).toBeTruthy();
  }));
});
