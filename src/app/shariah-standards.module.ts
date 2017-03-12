import { BrowserModule } from '@angular/platform-browser';
import { NgModule, ApplicationRef,Provider } from '@angular/core';
import { HttpModule, XHRBackend,RequestOptions,Http }       from '@angular/http'
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
import { QuranSearchComponent } from './quran-search/quran-search.component';
import { PrayerTimesComponent } from './prayer-times/prayer-times.component';
import { AboutUsComponent } from './about-us/about-us.component';
import { AUTH_PROVIDERS }      from 'angular2-jwt';
import {AlertModule, DatepickerModule} from 'ng2-bootstrap/ng2-bootstrap';
import {routing,appRoutingProviders} from './routes';
import { Routes, RouterModule,RouterLinkActive,RouterLink} from '@angular/router';
import { ArabicKeyboardComponent } from './arabic-keyboard/arabic-keyboard.component';
import { AuthService,getAuthenticationFactory} from './auth.service'
import {NgbModule} from '@ng-bootstrap/ng-bootstrap'
// import {NgBootstrapModule} from './ng-bootstrap-module'
import {UserProfileRegistrationService} from './user-profile-registration.service'
import {AuthenticatedHttpService} from './authenticated-http.service';
import { ShurahComponent } from './shurah/shurah.component'
import { MembershipRuleSectionComponent } from './shurah/membership-rule-section.component';
import { SuggestionComponent } from './suggestion/suggestion.component';
import { SuggestionDetailsComponent } from './suggestion-details/suggestion-details.component';
import { MembershipApplicationComponent } from './membership-application/membership-application.component';
import { ShurahNavigationComponent } from './shurah-navigation/shurah-navigation.component';
import { MemberComponent } from './member/member.component';
import { MemberDetailsComponent } from './member-details/member-details.component'
@NgModule({
  declarations: [
    ShariahStandardsAppComponent,
    ZakatCalculatorComponent,
    InheritanceCalculatorComponent,
    TermDefinitionsComponent,
    TermComponent,
    StandardsComponent,
    QuranSearchComponent,
    PrayerTimesComponent,
    AboutUsComponent,
    ArabicKeyboardComponent,
    ShurahComponent,
    MembershipRuleSectionComponent,
    SuggestionComponent,
    SuggestionDetailsComponent,
    MembershipApplicationComponent,
    ShurahNavigationComponent,
    MemberComponent,
    MemberDetailsComponent
      ],
  imports: [
    BrowserModule,
    HttpModule,
    CommonModule,
    FormsModule,
    // ChartsModule,
    DatepickerModule,
    NgbModule.forRoot(),
   routing
  ],
   providers: [
    appRoutingProviders,
    UserProfileRegistrationService,
    AUTH_PROVIDERS,
    AuthService,
    AuthenticatedHttpService,
    {
      provide:Http,
      useFactory: getAuthenticationFactory,
      deps: [XHRBackend, RequestOptions]
    }
  ],
  entryComponents: [ShariahStandardsAppComponent],
  bootstrap: [ShariahStandardsAppComponent]
})
export class ShariahStandardsModule {

}
