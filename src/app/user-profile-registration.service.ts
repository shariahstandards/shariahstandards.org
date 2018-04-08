import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs/Observable';
declare var shariahStandardsApiUrlBase:string;

@Injectable()
export class UserProfileRegistrationService {

  constructor(private http: HttpClient) { }
  getRecogniserUser(profile:Object){
  	return this.http.post(shariahStandardsApiUrlBase+"UserProfile",profile);
  }
}
