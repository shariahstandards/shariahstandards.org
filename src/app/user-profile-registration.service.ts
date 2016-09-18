import { Injectable } from '@angular/core';
import {Http, Response} from '@angular/http';
import {Observable} from 'rxjs/Observable';
declare var shariahStandardsApiUrlBase:string;

@Injectable()
export class UserProfileRegistrationService {

  constructor(private http: Http) { }
  register(profile:Object){
  	return this.http.post(shariahStandardsApiUrlBase+"register",profile);
  }
}
