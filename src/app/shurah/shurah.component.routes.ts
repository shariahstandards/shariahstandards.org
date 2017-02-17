import { Routes }          from '@angular/router';
import { ShurahComponent }   from './shurah.component';

export const shurahRoutes: Routes = [
  { path: 'standards', component: ShurahComponent },
  { path: 'standards/:section', component: ShurahComponent }
];
