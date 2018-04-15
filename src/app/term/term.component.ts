import { Component, OnInit, Input } from '@angular/core';
 import { RouterModule } from '@angular/router';

@Component({
  selector: 'term',
  templateUrl: './term.component.html',
  styleUrls: ['./term.component.css'],
  // directives: [ROUTER_DIRECTIVES]
})
export class TermComponent implements OnInit {
  @Input("term") term:string	
  @Input("term-id") termId:string	
  @Input("organisation-id") organisationId:string	
  constructor() { }

  ngOnInit() {
//  	console.log("creating term component "+this.content);
  }

}
