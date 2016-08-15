import { RouterConfig }          from '@angular/router';
import { StandardsComponent }   from './standards.component';

export const standardsRoutes: RouterConfig = [
  { path: 'standards', component: StandardsComponent },
  { path: 'standards/:section', component: StandardsComponent }
];
