import "./polyfills.ts";

import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import {ShariahStandardsModule} from './app/shariah-standards.module'
// import { bootstrap } from '@angular/platform-browser-dynamic';
import { enableProdMode } from '@angular/core';
// import {AlertComponent, DatepickerModule} from 'ng2-bootstrap/ng2-bootstrap';
// import { HTTP_PROVIDERS } from '@angular/http';
import { environment } from './app/';
// import { ShariahStandardsAppComponent, environment } from './app/';
// import { APP_ROUTER_PROVIDERS } from './app/routes';
if (environment.production) {
  enableProdMode();
}

platformBrowserDynamic().bootstrapModule(ShariahStandardsModule);

