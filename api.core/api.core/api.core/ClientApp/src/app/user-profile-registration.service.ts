import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs/Observable';
import {environment} from './environments/environment';

@Injectable()
export class UserProfileRegistrationService {

  constructor(private http: HttpClient) { }
  getRecogniserUser(profile:Object){
  	return this.http.post(environment.shariahStandardsApiUrlBase+"UserProfile",profile);
  }
}
