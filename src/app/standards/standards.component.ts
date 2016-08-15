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
  collapsedSections:string[]=[]
  ngOnInit() {
  }
  toggleCollapse(sectionName:string){
  	var index=this.collapsedSections.indexOf(sectionName);
  	if(index>=0){
  		this.collapsedSections.splice(index,1);
  	}else{
  		this.collapsedSections.push(sectionName);
  	}
  }
  expand(sectionName:string){
  	var index=this.collapsedSections.indexOf(sectionName);
 	return index<0;
  }
}
