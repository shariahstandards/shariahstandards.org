import { BrowserModule } from '@angular/platform-browser';
import { NgModule, ApplicationRef } from '@angular/core';
import { HttpModule }       from '@angular/http'
// import {  } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ChartsModule } from 'ng2-charts/ng2-charts';
import { ShariahStandardsAppComponent } from './shariah-standards.component';
import { ZakatCalculatorComponent } from './zakat-calculator/zakat-calculator.component';
import { InheritanceCalculatorComponent } from './inheritance-calculator/inheritance-calculator.component';
import { TermDefinitionsComponent} from './term-definitions/term-definitions.component';
import { StandardsComponent } from './standards/standards.component';
import { QuranSearchComponent } from './quran-search/quran-search.component';
import { PrayerTimesComponent } from './prayer-times/prayer-times.component';
import { AboutUsComponent } from './about-us/about-us.component';

import {AlertModule, DatepickerModule} from 'ng2-bootstrap/ng2-bootstrap';
import {routing,appRoutingProviders} from './routes';
import { Routes, RouterModule,RouterLinkActive,RouterLink} from '@angular/router';
import { ArabicKeyboardComponent } from './arabic-keyboard/arabic-keyboard.component';


@NgModule({
  declarations: [
    ShariahStandardsAppComponent,
    ZakatCalculatorComponent,
    InheritanceCalculatorComponent,
    TermDefinitionsComponent,
    StandardsComponent,
    QuranSearchComponent,
    PrayerTimesComponent,
    AboutUsComponent,
    ArabicKeyboardComponent
      ],
  imports: [
    BrowserModule,
    HttpModule,
    CommonModule,
    FormsModule,
    ChartsModule,
    DatepickerModule,
    routing
  ],
   providers: [
    appRoutingProviders
  ],
  entryComponents: [ShariahStandardsAppComponent],
  bootstrap: [ShariahStandardsAppComponent]
})
export class ShariahStandardsModule {

}
