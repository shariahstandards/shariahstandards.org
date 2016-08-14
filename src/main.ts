import { bootstrap } from '@angular/platform-browser-dynamic';
import { enableProdMode,provide } from '@angular/core';
import {AlertComponent, DATEPICKER_DIRECTIVES} from 'ng2-bootstrap/ng2-bootstrap';

import { HTTP_PROVIDERS } from '@angular/http';
import { Angular2SalahTimesAppComponent, environment } from './app/';
import { APP_ROUTER_PROVIDERS } from './app/routes';
if (environment.production) {
  enableProdMode();
}

bootstrap(Angular2SalahTimesAppComponent, [
	HTTP_PROVIDERS, 
	DATEPICKER_DIRECTIVES,
	APP_ROUTER_PROVIDERS

]);

