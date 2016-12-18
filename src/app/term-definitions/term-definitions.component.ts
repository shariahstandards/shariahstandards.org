import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router, ActivatedRoute }       from '@angular/router';
import { TermComponent } from '../term/term.component';

export interface TermDefinition{
	term:string,
	definition:DefinitionText,
  relatedTerms:string[]
}
export interface DefinitionSequentialContent{
  text:string,
  isADefinedTerm:boolean
}
export interface DefinitionText{
  contents:[DefinitionSequentialContent],
}


@Component({
  //moduleId: module.id,
  selector: 'app-term-definitions',
  templateUrl: './term-definitions.component.html',
  styleUrls: ['./term-definitions.component.css'],
//  directives:[TermComponent]
})
export class TermDefinitionsComponent implements OnInit, OnDestroy  {
  public term:string;
  public definition:DefinitionText;
  //private route:ActivatedRoute;
  constructor( 
  	private route:ActivatedRoute,
    private router:Router
  	) {
    this.termChanged();
  }
 ;

  private getRouteParamsSubscribe:any;
  ngOnInit() {
  	 this.definitions=[
  	 { term:"Allah",
       definition:{
         contents:[{
           text:"God, the all caring the all giving, the lord of everyone, the master of the day of judgement",
           isADefinedTerm:false
         }]
       },
       relatedTerms:[]
     },
    { term:"Muslim",
       definition:{
         contents:[{
           text:"A human being who is freely following the religion of",
           isADefinedTerm:false
         },
         {
           text:"Islam",
           isADefinedTerm:true
         }
         ,
         {
           text:"by declaring the",
           isADefinedTerm:false
         },,
         {
           text:"Shahada",
           isADefinedTerm:true
         }]
       },
     relatedTerms:[]},
    { term:"Islam",
       definition:{
         contents:[{
           text:"To be in a state of surrender to the will of",
           isADefinedTerm:false
         },
          {
           text:"Allah",
           isADefinedTerm:true
         },]
       },relatedTerms:[]},
       ,
    { term:"Shahada",
       definition:{
         contents:[{
           text:"A declaration made by an",
           isADefinedTerm:false
         },
         {
           text:"adult",
           isADefinedTerm:true
         },
         {
           text:"in front of at least 2 witnesses free from any coersion with the following words 'I see that there is no god except",
           isADefinedTerm:false
         },
         {
           text:"Allah",
           isADefinedTerm:true
         },
         {
           text:"and I see that Muhammad is the messenger of",
           isADefinedTerm:false
         }
         ,
         {
           text:"Allah",
           isADefinedTerm:true
         }
         ]
       },
       relatedTerms:[]},
    { term:"Adult",
       definition:{
         contents:[{
           text:"A human being over the age of 18 years",
           isADefinedTerm:false
         }]
       }
     ,relatedTerms:[]},
      { term:"Man",
       definition:{
         contents:[{
           text:"A male human being having reached the age of sexual maturity capable of having children",
           isADefinedTerm:false
         }]
       }
     ,relatedTerms:[]},
     { term:"Woman",
       definition:{
         contents:[{
           text:"A female human being having reached the age of sexual maturity capable of having children"
           +"and being fully physically developed",
           isADefinedTerm:false
         }]
       }
     ,relatedTerms:[]},
      { term:"Guardian",
       definition:{
         contents:[{
           text:"An adult with responsibility to act in the best interest of a another human being on their behalf",
           isADefinedTerm:false
         }]
       }
     ,relatedTerms:[]},
     ]

  	  this.getRouteParamsSubscribe=this.route.params.subscribe(params=>{
  	  	this.term=params['term'];
  	  	this.termChanged();
  	 });
  }
  termChanged(){
  	if(!this.term || this.term==''){
  		this.definition={
        contents:[{
          text:"no term specified",
          isADefinedTerm:false
        }]
      };
  	}else{
  		if(this.definitions.some((defn)=>{
  			return this.term && defn.term.toLowerCase()==this.term.toLowerCase()
  		})){
  			this.definition = this.definitions.filter((defn)=>{
  					return this.term && defn.term.toLowerCase()==this.term.toLowerCase()
  				})[0].definition;
          this.router.navigate(["/terms/"+this.term]);
     	}else{
		  	this.definition=
        {
          contents:[{
            text: "'"+this.term+"' is not defined",
            isADefinedTerm:false
          }]
        };
       ;
  		}
  	}
  }
  goToTerm(text:string){
    this.term=text;
    this.termChanged();
  }
  ngOnDestroy() {
  	this.getRouteParamsSubscribe.unsubscribe();
  }
  public definitions:[TermDefinition];

}
