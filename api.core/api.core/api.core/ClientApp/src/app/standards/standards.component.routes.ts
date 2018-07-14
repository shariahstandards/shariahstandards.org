import { Routes }          from '@angular/router';
import { StandardsComponent }   from './standards.component';

export const standardsRoutes: Routes = [
  { path: 'standards', component: StandardsComponent },
  { path: 'standards/:section', component: StandardsComponent }
];
