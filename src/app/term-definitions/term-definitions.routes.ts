import { Routes }          from '@angular/router';
import { TermDefinitionsComponent }   from './term-definitions.component';

export const termDefinitionsRoutes: Routes = [
  { path: 'terms', component: TermDefinitionsComponent },
  { path: 'terms/:term', component: TermDefinitionsComponent },
];
