import { Injectable }      from '@angular/core';
import { tokenNotExpired } from 'angular2-jwt';
import { UserProfileRegistrationService } from './user-profile-registration.service'
import { HttpModule, XHRBackend,RequestOptions,Http }       from '@angular/http'
import { AuthenticatedHttpService } from './authenticated-http.service';
import { Router, ActivatedRoute }       from '@angular/router';

declare var Auth0Lock: any;

@Injectable()
export class AuthService {

    // Configure Auth0
	lock = new Auth0Lock('4ui2TCsc9ZEtwWviLTZAKC6hwHoOP2RQ', 'shariahstandards.eu.auth0.com', {
		auth:{
			redirectUrl:window.location.origin,
			responseType :'token'
		}
	});
	userProfile: Object;

	constructor(private userProfileService:UserProfileRegistrationService,
	    private route:ActivatedRoute,
	    private router:Router,
 
		) {
		var self=this;
		//self.userProfile = JSON.parse(localStorage.getItem('profile'));

		// Add callback for lock `authenticated` event
		self.lock.on("authenticated", (authResult) => {
			localStorage.setItem('id_token', authResult.idToken);
		   	self.loadUserProfile();
		});
		if(tokenNotExpired()){
			self.loadUserProfile();
		}
	}
	loadUserProfile(){
		var self=this;
		var idToken = localStorage.getItem('id_token');
		if(tokenNotExpired()){
	 		self.lock.getProfile(idToken, (error, profile) => {
		        if (error) {
		          // Handle error
		          alert(error);
		          return;
		        }
		      //  self.userProfile = profile;
		        self.userProfileService.getRecogniserUser(profile).subscribe(recognisedUser=>{
		        	var result = recognisedUser.json();
		        self.userProfile = result;
		        //localStorage.setItem('profile', JSON.stringify(profile));
		        	console.log(result);
		        });
	  		});
	 	}
	}
	public login() {
		// Call the show method to display the widget.
		this.lock.show();
	};
	public authenticated() {
    	// Check if there's an unexpired JWT
    	// This searches for an item in localStorage with key == 'id_token'
    	return tokenNotExpired() && this.userProfile!=null;
  	};

  	public logout() {
    	// Remove token from localStorage
    	localStorage.removeItem('id_token');
		//localStorage.removeItem('profile');
    	this.userProfile = undefined;
    	this.router.navigateByUrl("/");
  	};

}
export function getAuthenticationFactory(backend: XHRBackend, defaultOptions: RequestOptions){ 
        return new AuthenticatedHttpService(backend, defaultOptions);
    };
