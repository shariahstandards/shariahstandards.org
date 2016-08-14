import { RouterConfig }          from '@angular/router';
import { QuranSearchComponent }   from './quran-search.component';

export const quranSearchRoutes: RouterConfig = [
  { path: 'quran', component: QuranSearchComponent },
  { path: 'quran/:searchText', component: QuranSearchComponent }
];