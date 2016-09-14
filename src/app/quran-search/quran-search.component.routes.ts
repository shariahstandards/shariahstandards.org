import { Routes }          from '@angular/router';
import { QuranSearchComponent }   from './quran-search.component';

export const quranSearchRoutes: Routes = [
  { path: 'quran', component: QuranSearchComponent },
  { path: 'quran/search/:searchText', component: QuranSearchComponent },
  { path: 'quran/surah/:surahNumber/verse/:verseNumber', component: QuranSearchComponent },
  { path: 'quran/surah/:surahNumber/verse/:verseNumber/:searchText', component: QuranSearchComponent },
];