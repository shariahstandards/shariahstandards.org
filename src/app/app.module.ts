import { BrowserModule } from '@angular/platform-browser';
import { NgModule, ApplicationRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ShariahStandardsAppComponent } from './shariah-standards.component';
import { ZakatCalculatorComponent } from './zakat-calculator/zakat-calculator.component';
import { InheritanceCalculatorComponent } from './inheritance-calculator/inheritance-calculator.component';

@NgModule({
  declarations: [
    ShariahStandardsAppComponent,
    ZakatCalculatorComponent,
    InheritanceCalculatorComponent
  ],
  imports: [
    BrowserModule,
    CommonModule,
    FormsModule
  ],
  providers: [],
  entryComponents: [ShariahStandardsAppComponent],
  bootstrap: [ShariahStandardsAppComponent]
})
export class ShariahStandardsModule {

}
