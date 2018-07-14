import { RouterModule, Routes} from '@angular/router';
import { ModuleWithProviders }  from '@angular/core';
import {PrayerTimesComponent} from './prayer-times/prayer-times.component'
import {AboutUsComponent} from './about-us/about-us.component'
import {TermDefinitionsComponent} from './term-definitions/term-definitions.component'
import {termDefinitionsRoutes} from './term-definitions/term-definitions.routes'
// import {standardsRoutes} from './standards/standards.component.routes'
import {shurahRoutes} from './shurah/shurah.component.routes'
// import {quranSearchRoutes} from './quran-search/quran-search.component.routes'
import {zakatRoutes} from './zakat-calculator/zakat-calculator.component.routes'
import {inheritanceRoutes} from './inheritance-calculator/inheritance-calculator.component.routes'
import {suggestionRoutes} from './suggestion/suggestion.component.routes'
import {membershipApplicationRoutes} from './membership-application/membership-application.component.routes'
import {memberRoutes} from './member/member.component.routes'
import {PrivacyTermsComponent} from './privacy-terms/privacy-terms.component'
import {TermsAndConditionsComponent} from './terms-and-conditions/terms-and-conditions.component'
const appRoutes: Routes = [
  ...termDefinitionsRoutes,
  // ...standardsRoutes,
  // ...shurahRoutes,
  ...zakatRoutes,
  ...inheritanceRoutes,
  ...suggestionRoutes,
  ...membershipApplicationRoutes,
  ...memberRoutes,
  { path: '', component: PrayerTimesComponent },
  { path: 'about-us', component: AboutUsComponent },
  { path: 'privacy', component: PrivacyTermsComponent },
  { path: 'ts-and-cs', component: TermsAndConditionsComponent },
  { path: 'quran', loadChildren: './quran.module#QuranModule'}
];
export const appRoutingProviders: any[] = [

];
export const routing: ModuleWithProviders = RouterModule.forRoot(appRoutes);

