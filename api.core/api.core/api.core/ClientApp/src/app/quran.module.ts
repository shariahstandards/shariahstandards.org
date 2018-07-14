import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { QuranSearchComponent } from './quran-search/quran-search.component';
import { QuranReferenceComponent} from './quran-reference/quran-reference.component';
import { FormsModule } from '@angular/forms';
import { ArabicKeyboardComponent } from './arabic-keyboard/arabic-keyboard.component';


// containers
import {quranSearchRoutes} from './quran-search/quran-search.component.routes'

// routes
  
export const ROUTES: Routes = [...quranSearchRoutes];

@NgModule({
  imports: [CommonModule,FormsModule, RouterModule.forChild(ROUTES)],
  declarations: [
  	QuranSearchComponent,
  	ArabicKeyboardComponent,
  	QuranReferenceComponent
  	],
})
export class QuranModule {}