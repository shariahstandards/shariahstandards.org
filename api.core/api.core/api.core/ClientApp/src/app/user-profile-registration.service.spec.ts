/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { UserProfileRegistrationService } from './user-profile-registration.service';
import {HttpClient} from '@angular/common/http';

describe('Service: UserProfileRegistration', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [UserProfileRegistrationService,HttpClient]
    });
  });

  it('should ...', inject([UserProfileRegistrationService], (service: UserProfileRegistrationService) => {
    expect(service).toBeTruthy();
  }));
});
