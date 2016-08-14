import { RouterConfig }          from '@angular/router';
import { TermDefinitionsComponent }   from './term-definitions.component';

export const termDefinitionsRoutes: RouterConfig = [
  { path: 'terms', component: TermDefinitionsComponent },
  { path: 'terms/:term', component: TermDefinitionsComponent },
];
