import "./polyfills.ts";
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import {ShariahStandardsModule} from './app/shariah-standards.module'
import { enableProdMode } from '@angular/core';
import { environment } from './app/environments/environment';
if (environment.production) {
  enableProdMode();
}

platformBrowserDynamic().bootstrapModule(ShariahStandardsModule);
