import { Routes }          from '@angular/router';
import { SuggestionComponent }   from './suggestion.component';
import { SuggestionDetailComponent } from '../suggestion-detail/suggestion-detail.component'

export const suggestionRoutes: Routes = [
  { path: 'suggestions/:organisationId', component: SuggestionComponent },
  { path: 'suggestions/:organisationId/details/:id', component: SuggestionDetailComponent },
  { path: 'suggestions/:organisationId/:memberId', component: SuggestionComponent }
];
