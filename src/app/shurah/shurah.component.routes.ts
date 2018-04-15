import { Routes }          from '@angular/router';
import { ShurahComponent }   from './shurah.component';

export const shurahRoutes: Routes = [
  { path: 'rules/:organisationId', component: ShurahComponent},
  { path: 'rules/:organisationId/section/:section', component: ShurahComponent},
  { path: 'standards', component: ShurahComponent },
  { path: 'standards/:section', component: ShurahComponent }
];
