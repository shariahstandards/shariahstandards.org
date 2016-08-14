import { Component, OnInit } from '@angular/core';
import {TermComponent} from '../term/term.component';
import {QuranReferenceComponent} from '../quran-reference/quran-reference.component';

@Component({
  selector: 'app-standards',
  templateUrl: 'standards.component.html',
  styleUrls: ['standards.component.css'],
  directives:[TermComponent,QuranReferenceComponent]
})
export class StandardsComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
