import { Routes }          from '@angular/router';
import { TermDefinitionsComponent }   from './term-definitions.component';

export const termDefinitionsRoutes: Routes = [
  { path: 'terms/:organisationId', component: TermDefinitionsComponent },
  { path: 'terms/:organisationId/:termId/:term', component: TermDefinitionsComponent },
];
