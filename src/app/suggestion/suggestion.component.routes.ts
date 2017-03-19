import { Routes }          from '@angular/router';
import { SuggestionComponent }   from './suggestion.component';
import { SuggestionDetailsComponent } from '../suggestion-details/suggestion-details.component'

export const suggestionRoutes: Routes = [
  { path: 'suggestions/:organisationId', component: SuggestionComponent },
  { path: 'suggestions/:organisationId/details/:id', component: SuggestionDetailsComponent },
  { path: 'suggestions/:organisationId/:memberId', component: SuggestionComponent }
];
