import { Injectable } from '@angular/core';
import {Http, Response,ConnectionBackend,RequestOptions,RequestOptionsArgs} from '@angular/http';
import {Observable} from 'rxjs/Observable';
import { JwtHelperService } from '@auth0/angular-jwt';
import {AuthService} from './auth.service';
import {environment} from './environments/environment';
@Injectable()
export class AuthenticatedHttpService extends Http{

  	constructor(private jwtHelperService:JwtHelperService, backend: ConnectionBackend, private defaultOptions: RequestOptions) { 
		super(backend, defaultOptions);
	}

	setAuthHeader(url:string){
		this.defaultOptions.headers.delete("Authorization");
		if((this.jwtHelperService.isTokenExpired()==false) && url.indexOf(environment.shariahStandardsApiUrlBase)==0){
	    	this.defaultOptions.headers.append("Authorization","Bearer "+AuthService.token);
		}
	}
	get(url: string, options?: RequestOptionsArgs): Observable<Response> {
		this.setAuthHeader(url)
		return super.get(url, options);
  	}
  	post(url: string, body: any, options?: RequestOptionsArgs): Observable<Response> {   
  		this.setAuthHeader(url);
  		return super.post(url,body,options);
    }
  	

}
