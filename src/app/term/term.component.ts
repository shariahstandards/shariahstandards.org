import { Component, OnInit, Input } from '@angular/core';
import { ROUTER_DIRECTIVES } from '@angular/router';

@Component({
  selector: '[term]',
  templateUrl: 'term.component.html',
  styleUrls: ['term.component.css'],
  directives: [ROUTER_DIRECTIVES]
})
export class TermComponent implements OnInit {
  @Input('term') name:string;
  constructor() { }

  ngOnInit() {
  	console.log("creating term component "+this.name);
  }

}
