import { Component, ViewContainerRef} from '@angular/core';
import {PrayerTimesComponent} from './prayer-times/prayer-times.component'
import { ROUTER_DIRECTIVES } from '@angular/router';

import {MODAL_DIRECTVES, BS_VIEW_PROVIDERS} from 'ng2-bootstrap/ng2-bootstrap';
import {CORE_DIRECTIVES} from '@angular/common';

@Component({
 // moduleId: module.id,
  selector: 'angular2-salah-times-app',
  templateUrl: './angular2-salah-times.component.html',
  styleUrls: ['./angular2-salah-times.component.css'],
  directives: [PrayerTimesComponent, MODAL_DIRECTVES, CORE_DIRECTIVES,ROUTER_DIRECTIVES],
  viewProviders: [BS_VIEW_PROVIDERS]
})
export class Angular2SalahTimesAppComponent {
  title = 'Muslim Prayer Times and Directions';
  public constructor(private viewContainerRef: ViewContainerRef) {
  }
}
