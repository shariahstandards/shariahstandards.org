/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { AuthService } from './auth.service';
import { UserProfileRegistrationService } from './user-profile-registration.service'
import { Router } from '@angular/router';


describe('Service: Auth', () => {
  var userProfileRegistrationService={};
  var router={};

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [AuthService,
      {provide:UserProfileRegistrationService,useValue:userProfileRegistrationService},
      {provide:Router,useValue:router}
      ]
    });
  });

  it('should ...', inject([AuthService], (service: AuthService) => {
    expect(service).toBeTruthy();
  }));
});
