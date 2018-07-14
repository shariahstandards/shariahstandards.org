import { Component, OnInit } from '@angular/core';
import { ShurahService} from '../shurah.service';
import { AuthService } from '../auth.service'
import { Router, ActivatedRoute }       from '@angular/router';
import 'moment';
declare var moment: any;

@Component({
  selector: 'app-suggestion-details',
  templateUrl: './suggestion-details.component.html',
  styleUrls: ['./suggestion-details.component.css'],
  providers:[ShurahService]

})
export class SuggestionDetailsComponent implements OnInit {

  constructor(
    private route:ActivatedRoute,
    private router:Router,
    private auth: AuthService,
 	private shurahService:ShurahService
  	) { }
  suggestion:any
  suggestionId:number
  organisationId:number
  private getRouteParamsSubscribe:any;
  formatDate(dateText:string){
  	var dateMoment=moment(dateText,"YYYYMMDDTHH:mm:ss");
    return "on "+dateMoment.format("ddd Do MMM YYYY")+"at "+dateMoment.format("HH:mm:ss") +" GMT";
  }
  ngOnInit() {
  	 this.getRouteParamsSubscribe=this.route.params.subscribe(params=>{
        this.suggestionId=params['id'];
      	this.organisationId=params['organisationId'];
      	this.refresh();
     });  
  }
  deleteSuggestion(){
  	this.shurahService.deleteSuggestion(this.suggestionId).subscribe(response=>{
 		var model=response;
 		if(!model.hasError){
 			this.router.navigateByUrl("/suggestions/"+this.organisationId);
 		}
 	})
  }
  vote(inFavour?:boolean){
  	this.shurahService.voteOnSuggestion(this.suggestionId, inFavour).subscribe(response=>{
 		var model=response;
 		if(model.hasError){
 			alert(model.error);
 		}
 		else{
 			this.refresh();
 		}
 	})
  }
   removeVote(voteId:number){
  	this.shurahService.removeVoteOnSuggestion(voteId).subscribe(response=>{
 		var model=response;
 		if(model.hasError){
 			alert(model.error);
 		}
 		else{
 			this.refresh();
 		}
 	})
  }
  refresh(){
	this.shurahService.suggestionDetails(this.suggestionId).subscribe(response=>{
 		var model=response;
 		if(model.hasError){
 			this.suggestion=null;
 		}else{
 			this.suggestion=model;
 		}
 	})  	
  }

}
