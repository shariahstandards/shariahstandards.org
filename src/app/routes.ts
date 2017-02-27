import { RouterModule, Routes} from '@angular/router';
import { ModuleWithProviders }  from '@angular/core';
import {PrayerTimesComponent} from './prayer-times/prayer-times.component'
import {AboutUsComponent} from './about-us/about-us.component'
import {TermDefinitionsComponent} from './term-definitions/term-definitions.component'
import {termDefinitionsRoutes} from './term-definitions/term-definitions.routes'
// import {standardsRoutes} from './standards/standards.component.routes'
import {shurahRoutes} from './shurah/shurah.component.routes'
import {quranSearchRoutes} from './quran-search/quran-search.component.routes'
import {zakatRoutes} from './zakat-calculator/zakat-calculator.component.routes'
import {inheritanceRoutes} from './inheritance-calculator/inheritance-calculator.component.routes'
import {suggestionRoutes} from './suggestion/suggestion.component.routes'
const appRoutes: Routes = [
  ...termDefinitionsRoutes,
  // ...standardsRoutes,
  ...shurahRoutes,
  ...zakatRoutes,
  ...inheritanceRoutes,
  ...quranSearchRoutes,
  ...suggestionRoutes,
  { path: '', component: PrayerTimesComponent },
  { path: 'about-us', component: AboutUsComponent }
];
export const appRoutingProviders: any[] = [

];
export const routing: ModuleWithProviders = RouterModule.forRoot(appRoutes);

