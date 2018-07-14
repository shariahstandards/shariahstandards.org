import { Injectable, Output,EventEmitter } from '@angular/core';
import { AUTH_CONFIG } from './auth0-variables';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/filter';
import auth0 from 'auth0-js';
import { UserProfileRegistrationService } from './user-profile-registration.service'

@Injectable()
export class AuthService {

  userProfile: any;
  refreshSubscription: any;
  requestedScopes: string = 'openid profile read:timesheets create:timesheets';

  auth0 = new auth0.WebAuth({
    clientID: AUTH_CONFIG.clientID,
    domain: AUTH_CONFIG.domain,
    responseType: 'token id_token',
    audience: AUTH_CONFIG.audience,
    redirectUri: AUTH_CONFIG.callbackURL,
    scope: this.requestedScopes,
    leeway: 30
  });
 @Output() loggedIn:EventEmitter<boolean>=new EventEmitter<boolean>();

  constructor(private userProfileService:UserProfileRegistrationService,public router: Router) { }
  public static token:string;

  public login(): void {
    this.auth0.authorize();
  }

  public handleAuthentication(): void {
    this.handlingAuthentication=true;
    this.auth0.parseHash((err, authResult) => {
      if (authResult && authResult.accessToken && authResult.idToken) {
        window.location.hash = '';
        this.setSession(authResult);
        this.router.navigate(['/']);
      } else if (err) {
        this.router.navigate(['/']);
        console.log(err);
        alert(`Error: ${err.error}. Check the console for further details.`);
      }
      this.handlingAuthentication=false;
    });
  }

  public getProfile(cb): void {
    const accessToken = localStorage.getItem('access_token');
    if (!accessToken) {
      throw new Error('Access token must exist to fetch profile');
    }

    const self = this;
    this.auth0.client.userInfo(accessToken, (err, profile) => {
      	if (profile) {
	        self.userProfile = profile;
	 		self.userProfileService.getRecogniserUser(profile).subscribe(recognisedUser=>{
		    	var result = recognisedUser;
		        self.userProfile = result;      
		    });
 		}
      cb(err, profile);
    });
  }

  private setSession(authResult): void {
    // Set the time that the access token will expire at
    const expiresAt = JSON.stringify((authResult.expiresIn * 1000) + new Date().getTime());

    // If there is a value on the `scope` param from the authResult,
    // use it to set scopes in the session for the user. Otherwise
    // use the scopes as requested. If no scopes were requested,
    // set it to nothing
    const scopes = authResult.scope || this.requestedScopes || '';

    localStorage.setItem('access_token', authResult.accessToken);
   	AuthService.token=authResult.idToken;
   // localStorage.setItem('id_token', authResult.idToken);
    localStorage.setItem('expires_at', expiresAt);
    localStorage.setItem('scopes', JSON.stringify(scopes));
    this.scheduleRenewal();
    this.getProfile(()=>{
        console.log("session set and logged in")
        this.loggedIn.emit(true);
    });
  }
  public logout(): void {

    this.userProfile=null;
    // Remove tokens and expiry time from localStorage
    localStorage.removeItem('access_token');
    AuthService.token=null;
    localStorage.removeItem('expires_at');
    localStorage.removeItem('scopes');
    this.unscheduleRenewal();
    // Go back to the home route
    this.router.navigate(['/']);
    console.log("logged out")

    this.loggedIn.emit(false);
  }

  public isAuthenticated(): boolean {
    // Check whether the current time is past the
    // access token's expiry time
    const expiresAt = JSON.parse(localStorage.getItem('expires_at'));
    return new Date().getTime() < expiresAt;
  }

  public userHasScopes(scopes: Array<string>): boolean {
    const grantedScopes = JSON.parse(localStorage.getItem('scopes')).split(' ');
    return scopes.every(scope => grantedScopes.includes(scope));
  }
  handlingAuthentication:boolean=false;
  public renewToken(authorizeOnError:boolean) {
    if(!this.handlingAuthentication){
      var self=this;
      this.auth0.checkSession({
        }, function (err, result) {
          if (err) {
            if(authorizeOnError){
          //    alert(`Could not get a new token using silent authentication (${err.error}). Redirecting to login page...`);
              self.auth0.authorize();
            }
          } else {
              self.setSession(result);
          }
      });
      
      // this.auth0.renewAuth({
      //   audience: AUTH_CONFIG.audience,
      //   redirectUri: AUTH_CONFIG.silentCallbackURL,
      //   usePostMessage: true
      // }, (err, result) => {
      //   if (err) {
      //     //alert(`Could not get a new token using silent authentication (${err.error}).`);
      //   } else {
      //     //alert(`Successfully renewed auth!`);
      //     this.setSession(result);
      //   }
      // });
    }
  }

  public scheduleRenewal() {
    if (!this.isAuthenticated()) return;

    const expiresAt = JSON.parse(window.localStorage.getItem('expires_at'));

    const source = Observable.of(expiresAt).flatMap(
      expiresAt => {

        const now = Date.now();

        // Use the delay in a timer to
        // run the refresh at the proper time
        var refreshAt = expiresAt - (1000 * 30); // Refresh 30 seconds before expiry
        return Observable.timer(Math.max(1, refreshAt - now));
      });

    // Once the delay time from above is
    // reached, get a new JWT and schedule
    // additional refreshes
    this.refreshSubscription = source.subscribe(() => {
      this.renewToken(true);
    });
  }

  public unscheduleRenewal() {
    if (!this.refreshSubscription) return;
    this.refreshSubscription.unsubscribe();
  }
}



/*
diff --git a/src/app/auth.service.ts b/src/app/auth.service.ts
index 918ab1e..d137cec 100644
--- a/src/app/auth.service.ts
+++ b/src/app/auth.service.ts
@@ -1,80 +1,144 @@
-import { Injectable }      from '@angular/core';
-import { tokenNotExpired } from 'angular2-jwt';
-import { UserProfileRegistrationService } from './user-profile-registration.service'
-import { HttpModule, XHRBackend,RequestOptions,Http }       from '@angular/http'
-import { AuthenticatedHttpService } from './authenticated-http.service';
-import { Router, ActivatedRoute }       from '@angular/router';
-
-declare var Auth0Lock: any;
+import { Injectable } from '@angular/core';
+import { AUTH_CONFIG } from './auth0-variables';
+import { Router } from '@angular/router';
+import { Observable } from 'rxjs';
+import 'rxjs/add/operator/filter';
+import auth0 from 'auth0-js';
 
 @Injectable()
 export class AuthService {
 
-    // Configure Auth0
-	lock = new Auth0Lock('4ui2TCsc9ZEtwWviLTZAKC6hwHoOP2RQ', 'shariahstandards.eu.auth0.com', {
-		auth:{
-			redirectUrl:window.location.origin,
-			responseType :'token'
-		}
-	});
-	userProfile: Object;
-
-	constructor(private userProfileService:UserProfileRegistrationService,
-	    private route:ActivatedRoute,
-	    private router:Router,
- 
-		) {
-		var self=this;
-		//self.userProfile = JSON.parse(localStorage.getItem('profile'));
-
-		// Add callback for lock `authenticated` event
-		self.lock.on("authenticated", (authResult) => {
-			localStorage.setItem('id_token', authResult.idToken);
-		   	self.loadUserProfile();
-		});
-		if(tokenNotExpired()){
-			self.loadUserProfile();
-		}
-	}
-	loadUserProfile(){
-		var self=this;
-		var idToken = localStorage.getItem('id_token');
-		if(tokenNotExpired()){
-	 		self.lock.getProfile(idToken, (error, profile) => {
-		        if (error) {
-		          // Handle error
-		          alert(error);
-		          return;
-		        }
-		      //  self.userProfile = profile;
-		        self.userProfileService.getRecogniserUser(profile).subscribe(recognisedUser=>{
-		        	var result = recognisedUser.json();
-		        self.userProfile = result;
-		        //localStorage.setItem('profile', JSON.stringify(profile));
-		        	console.log(result);
-		        });
-	  		});
-	 	}
-	}
-	public login() {
-		// Call the show method to display the widget.
-		this.lock.show();
-	};
-	public authenticated() {
-    	// Check if there's an unexpired JWT
-    	// This searches for an item in localStorage with key == 'id_token'
-    	return tokenNotExpired() && this.userProfile!=null;
-  	};
-
-  	public logout() {
-    	// Remove token from localStorage
-    	localStorage.removeItem('id_token');
-		//localStorage.removeItem('profile');
-    	this.userProfile = undefined;
-    	this.router.navigateByUrl("/");
-  	};
+  userProfile: any;
+  refreshSubscription: any;
+  requestedScopes: string = 'openid profile read:timesheets create:timesheets';
+
+  auth0 = new auth0.WebAuth({
+    clientID: AUTH_CONFIG.clientID,
+    domain: AUTH_CONFIG.domain,
+    responseType: 'token id_token',
+    audience: AUTH_CONFIG.audience,
+    redirectUri: AUTH_CONFIG.callbackURL,
+    scope: this.requestedScopes,
+    leeway: 30
+  });
+
+  constructor(public router: Router) { }
+  public static token:string;
+
+  public login(): void {
+    this.auth0.authorize();
+  }
+
+  public handleAuthentication(): void {
+    this.auth0.parseHash((err, authResult) => {
+      if (authResult && authResult.accessToken && authResult.idToken) {
+        window.location.hash = '';
+        this.setSession(authResult);
+        this.router.navigate(['/']);
+      } else if (err) {
+        this.router.navigate(['/']);
+        console.log(err);
+        alert(`Error: ${err.error}. Check the console for further details.`);
+      }
+    });
+  }
+
+  public getProfile(cb): void {
+    const accessToken = localStorage.getItem('access_token');
+    if (!accessToken) {
+      throw new Error('Access token must exist to fetch profile');
+    }
+
+    const self = this;
+    this.auth0.client.userInfo(accessToken, (err, profile) => {
+      if (profile) {
+        self.userProfile = profile;
+      }
+      cb(err, profile);
+    });
+  }
+
+  private setSession(authResult): void {
+    // Set the time that the access token will expire at
+    const expiresAt = JSON.stringify((authResult.expiresIn * 1000) + new Date().getTime());
+
+    // If there is a value on the `scope` param from the authResult,
+    // use it to set scopes in the session for the user. Otherwise
+    // use the scopes as requested. If no scopes were requested,
+    // set it to nothing
+    const scopes = authResult.scope || this.requestedScopes || '';
+
+    localStorage.setItem('access_token', authResult.accessToken);
+   	AuthService.token=authResult.idToken;
+   // localStorage.setItem('id_token', authResult.idToken);
+    localStorage.setItem('expires_at', expiresAt);
+    localStorage.setItem('scopes', JSON.stringify(scopes));
+    this.scheduleRenewal();
+  }
+  public logout(): void {
+    // Remove tokens and expiry time from localStorage
+    localStorage.removeItem('access_token');
+    AuthService.token=null;
+    localStorage.removeItem('expires_at');
+    localStorage.removeItem('scopes');
+    this.unscheduleRenewal();
+    // Go back to the home route
+    this.router.navigate(['/']);
+  }
+
+  public isAuthenticated(): boolean {
+    // Check whether the current time is past the
+    // access token's expiry time
+    const expiresAt = JSON.parse(localStorage.getItem('expires_at'));
+    return new Date().getTime() < expiresAt;
+  }
+
+  public userHasScopes(scopes: Array<string>): boolean {
+    const grantedScopes = JSON.parse(localStorage.getItem('scopes')).split(' ');
+    return scopes.every(scope => grantedScopes.includes(scope));
+  }
+
+  public renewToken() {
+    this.auth0.renewAuth({
+      audience: AUTH_CONFIG.audience,
+      redirectUri: AUTH_CONFIG.silentCallbackURL,
+      usePostMessage: true
+    }, (err, result) => {
+      if (err) {
+        //alert(`Could not get a new token using silent authentication (${err.error}).`);
+      } else {
+        //alert(`Successfully renewed auth!`);
+        this.setSession(result);
+      }
+    });
+  }
+
+  public scheduleRenewal() {
+    if (!this.isAuthenticated()) return;
+
+    const expiresAt = JSON.parse(window.localStorage.getItem('expires_at'));
+
+    const source = Observable.of(expiresAt).flatMap(
+      expiresAt => {
+
+        const now = Date.now();
+
+        // Use the delay in a timer to
+        // run the refresh at the proper time
+        var refreshAt = expiresAt - (1000 * 30); // Refresh 30 seconds before expiry
+        return Observable.timer(Math.max(1, refreshAt - now));
+      });
+
+    // Once the delay time from above is
+    // reached, get a new JWT and schedule
+    // additional refreshes
+    this.refreshSubscription = source.subscribe(() => {
+      this.renewToken();
+    });
+  }
 
+  public unscheduleRenewal() {
+    if (!this.refreshSubscription) return;
+    this.refreshSubscription.unsubscribe();
+  }
 }
-export function getAuthenticationFactory(backend: XHRBackend, defaultOptions: RequestOptions){ 
-        return new AuthenticatedHttpService(backend, defaultOptions);
-    };

*/