/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { AuthenticatedHttpService } from './authenticated-http.service';
import { JwtHelperService } from '@auth0/angular-jwt';
import {ConnectionBackend,RequestOptions} from '@angular/http';

describe('Service: AuthenticatedHttp', () => {
	var jwtHelperService={};
	var connectionBackend={};
	var requestOptions={}
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [AuthenticatedHttpService,
      {provide:JwtHelperService,useValue:jwtHelperService},
      {provide:ConnectionBackend,useValue:connectionBackend},
      {provide:RequestOptions,useValue:requestOptions}
      ]
    });
  });

  it('should ...', inject([AuthenticatedHttpService], (service: AuthenticatedHttpService) => {
    expect(service).toBeTruthy();
  }));
});
