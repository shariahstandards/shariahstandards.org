import { Component, OnInit } from '@angular/core';
import { TermComponent } from '../term/term.component';
import { Router, ActivatedRoute }       from '@angular/router';
import { QuranReferenceComponent } from '../quran-reference/quran-reference.component';

@Component({
  selector: 'app-standards',
  templateUrl: 'standards.component.html',
  styleUrls: ['standards.component.css'],
  directives:[TermComponent,QuranReferenceComponent]
})
export class StandardsComponent implements OnInit {

  constructor(   
    private route:ActivatedRoute,
    private router:Router
 ) { }
  collapsedSections:string[]=["website-management","prayer-times-rules","zakat-rules","inheritance-rules"]
  ngOnInit() {
     this.getRouteParamsSubscribe=this.route.params.subscribe(params=>{
        this.toggleCollapse(params['section']);
     });
  }

  toggleCollapse(sectionName:string){
  	var index=this.collapsedSections.indexOf(sectionName);
  	if(index>=0){
  		this.collapsedSections.splice(index,1);
  	}else{
  		this.collapsedSections.push(sectionName);
  	}
  }
  private getRouteParamsSubscribe:any;
  ngOnDestroy() {
    this.getRouteParamsSubscribe.unsubscribe();
  }
  expand(sectionName:string){
  	var index=this.collapsedSections.indexOf(sectionName);
 	return index<0;
  }
}
