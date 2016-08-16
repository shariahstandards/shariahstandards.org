import { provideRouter, RouterConfig } from '@angular/router';

import {PrayerTimesComponent} from './prayer-times/prayer-times.component'
import {AboutUsComponent} from './about-us/about-us.component'
import {TermDefinitionsComponent} from './term-definitions/term-definitions.component'
import {termDefinitionsRoutes} from './term-definitions/term-definitions.routes'
import {standardsRoutes} from './standards/standards.component.routes'
import {quranSearchRoutes} from './quran-search/quran-search.component.routes'
import {zakatRoutes} from './zakat-calculator/zakat-calculator.component.routes'
export const routes: RouterConfig = [
  ...termDefinitionsRoutes,
  ...standardsRoutes,
  ...zakatRoutes,
//  ...quranSearchRoutes,
  { path: '', component: PrayerTimesComponent },
  { path: 'about-us', component: AboutUsComponent }
];

export const APP_ROUTER_PROVIDERS = [
  provideRouter(routes)
];

