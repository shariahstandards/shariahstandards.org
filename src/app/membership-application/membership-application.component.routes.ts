import { Routes }          from '@angular/router';
import { MembershipApplicationComponent }   from './membership-application.component';

export const membershipApplicationRoutes: Routes = [
  { path: 'membership-applications/organisation/:organisationId', component: MembershipApplicationComponent },
  { path: 'membership-applications/organisation/:organisationId/page/:page', component: MembershipApplicationComponent }
];
