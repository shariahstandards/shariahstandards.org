import { Routes }          from '@angular/router';
import { SuggestionComponent }   from './suggestion.component';
import { SuggestionDetailsComponent } from '../suggestion-details/suggestion-details.component'

export const suggestionRoutes: Routes = [
  { path: 'suggestions', component: SuggestionComponent },
  { path: 'suggestion/details/:id', component: SuggestionDetailsComponent },
  { path: 'suggestions/page/:page', component: SuggestionComponent }
];
