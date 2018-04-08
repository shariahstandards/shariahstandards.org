declare var shariahStandardsApiUrlBase;
import { BrowserModule } from '@angular/platform-browser';
import { NgModule, ApplicationRef,Provider } from '@angular/core';
import { HttpModule,ConnectionBackend, XHRBackend,RequestOptions,Http }       from '@angular/http'
// import {  } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
// import { ChartsModule } from 'ng2-charts/ng2-charts';
import { ShariahStandardsAppComponent } from './shariah-standards.component';
import { ZakatCalculatorComponent } from './zakat-calculator/zakat-calculator.component';
import { InheritanceCalculatorComponent } from './inheritance-calculator/inheritance-calculator.component';
import { TermDefinitionsComponent} from './term-definitions/term-definitions.component';
import { TermComponent } from './term/term.component';
import { StandardsComponent } from './standards/standards.component';
// import { QuranSearchComponent } from './quran-search/quran-search.component';
// import {QuranReferenceComponent} from './quran-reference/quran-reference.component';
import { PrayerTimesComponent } from './prayer-times/prayer-times.component';
import { AboutUsComponent } from './about-us/about-us.component';
// import { AUTH_PROVIDERS }      from 'angular2-jwt';
import {routing,appRoutingProviders} from './routes';
import { Routes, RouterModule,RouterLinkActive,RouterLink} from '@angular/router';
// import { ArabicKeyboardComponent } from './arabic-keyboard/arabic-keyboard.component';
import { AuthService} from './auth.service'
import {NgbModule} from '@ng-bootstrap/ng-bootstrap'
// import {NgBootstrapModule} from './ng-bootstrap-module'
import {UserProfileRegistrationService} from './user-profile-registration.service'
import {AuthenticatedHttpService} from './authenticated-http.service';
import { ShurahComponent } from './shurah/shurah.component'
import {OrganisationComponent} from './organisation/organisation.component';
import {RegisterComponent} from './register/register.component';
import {SuggestionDetailComponent} from './suggestion-detail/suggestion-detail.component';
import { MembershipRuleSectionComponent } from './shurah/membership-rule-section.component';
import { SuggestionComponent } from './suggestion/suggestion.component';
import { SuggestionDetailsComponent } from './suggestion-details/suggestion-details.component';
import { MembershipApplicationComponent } from './membership-application/membership-application.component';
import { ShurahNavigationComponent } from './shurah-navigation/shurah-navigation.component';
import { MemberComponent } from './member/member.component';
import { MemberDetailsComponent } from './member-details/member-details.component';
import { InfiniteScrollModule } from 'angular2-infinite-scroll';
import { PieChartComponent } from './pie-chart/pie-chart.component';
//import { AuthHttp, AuthConfig } from 'angular2-jwt';
import { JwtModule } from '@auth0/angular-jwt';
import { HttpClientModule } from '@angular/common/http';
//export function authHttpServiceFactory(http: Http, options: RequestOptions) {
  
  //return new AuthenticatedHttpService(new (),options)

  //return new AuthHttp(new AuthConfig({
  //   tokenGetter: (() => localStorage.getItem('access_token')),

  //   globalHeaders: [{'Content-Type': 'application/json'}],
  // }), http, options);
//    return new AuthHttp(new AuthConfig({
//      tokenName: 'token',
//      // noClientCheck: true,
//      tokenGetter: (() => AuthService.token),
//      globalHeaders: [{'Content-Type':'application/json'}],
//    }), http, options);
//}
// JwtModule.forRoot({
//   config: {
//     // ...
//     blacklistedRoutes: ['localhost:3001/auth/', 'foo.com/bar/']
//   }
// });
export function tokenGetter() {
  return localStorage.getItem('access_token');
}
@NgModule({
  declarations: [
    ShariahStandardsAppComponent,
    ZakatCalculatorComponent,
    InheritanceCalculatorComponent,
    TermDefinitionsComponent,
    TermComponent,
    StandardsComponent,
    // QuranSearchComponent,
    OrganisationComponent,
    // QuranReferenceComponent,
    PrayerTimesComponent,
    AboutUsComponent,
    // ArabicKeyboardComponent,
    ShurahComponent,
    MembershipRuleSectionComponent,
    SuggestionComponent,
    SuggestionDetailsComponent,
    MembershipApplicationComponent,
    ShurahNavigationComponent,
    MemberComponent,
    MemberDetailsComponent,
    PieChartComponent,
    RegisterComponent,
    SuggestionDetailComponent
      ],
  imports: [
    BrowserModule,
    HttpModule,
    CommonModule,
    FormsModule,
    // ChartsModule,
    InfiniteScrollModule,
    NgbModule.forRoot(),
    routing,
    HttpClientModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        whitelistedDomains: ['localhost:4500']
        // blacklistedRoutes: ['localhost:3001/auth/']
      }
    })
  ],
   providers: [
    appRoutingProviders,
    UserProfileRegistrationService,
    // AUTH_PROVIDERS,
    AuthService,
    // AuthenticatedHttpService,
    // {
    //   provide:Http,
    //   useFactory: null,
    //   deps: [XHRBackend, RequestOptions]
    // }
  ],
  entryComponents: [ShariahStandardsAppComponent],
  bootstrap: [ShariahStandardsAppComponent]
})
export class ShariahStandardsModule {

}
