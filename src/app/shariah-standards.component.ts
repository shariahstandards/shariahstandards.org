import { Component, ViewContainerRef} from '@angular/core';
import { PrayerTimesComponent } from './prayer-times/prayer-times.component'
import { AuthService } from './auth.service'
// import { Routes, RouterModule,RouterLinkActive,RouterLink, } from '@angular/router';

// import {MODAL_DIRECTVES, BS_VIEW_PROVIDERS} from 'ng2-bootstrap/ng2-bootstrap';
// import {CORE_DIRECTIVES} from '@angular/common';
declare var shariahStandardsApiUrlBase:string;
@Component({
  selector: 'shariah-standards-app',
  templateUrl: './shariah-standards.component.html',
  styleUrls: ['./shariah-standards.component.css'],
  // directives: [PrayerTimesComponent, MODAL_DIRECTVES, CORE_DIRECTIVES,ROUTER_DIRECTIVES],
  // viewProviders: [BS_VIEW_PROVIDERS]
})
export class ShariahStandardsAppComponent {
  title = 'Muslim Prayer Times and Directions';
  public constructor(private viewContainerRef: ViewContainerRef,private auth: AuthService) {
  }
}
