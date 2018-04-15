import { Routes }          from '@angular/router';
import { QuranSearchComponent }   from './quran-search.component';

export const quranSearchRoutes: Routes = [
  { path: '', component: QuranSearchComponent },
  { path: 'search/:searchText', component: QuranSearchComponent },
  { path: 'surah/:surahNumber/verse/:verseNumber', component: QuranSearchComponent },
  { path: 'surah/:surahNumber/verse/:verseNumber/:searchText', component: QuranSearchComponent },
  { path: 'surah/:surahNumber/verse/:verseNumber/:searchText/:wordToHighlight', component: QuranSearchComponent },
];