import { Injectable } from '@angular/core';
import {Http, Response} from '@angular/http';
import {Observable} from 'rxjs/Observable';
declare var shariahStandardsApiUrlBase:string;

@Injectable()
export class ShurahService {

	constructor(private http: Http) { 
		
	}
  	getRootOrganisation(){
  		return this.http.get(shariahStandardsApiUrlBase+"RootOrganisation");
  	}
	join(){
		return this.http.get(shariahStandardsApiUrlBase+"Join");
	}
	leave(){
		return this.http.get(shariahStandardsApiUrlBase+"Leave");
	}

}
