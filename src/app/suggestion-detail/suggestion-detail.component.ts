import { Component, OnInit ,Input, ChangeDetectorRef} from '@angular/core';
import {Suggestion} from './suggestion.interface';
import { ShurahService} from '../shurah.service';
import { Router, ActivatedRoute }       from '@angular/router';
import { AuthService } from '../auth.service'

import 'moment';
declare var moment: any;

@Component({
	selector: 'suggestion-detail',
	templateUrl: './suggestion-detail.component.html',
	styleUrls: ['./suggestion-detail.component.css'],
  	providers:[ShurahService]
})
export class SuggestionDetailComponent implements OnInit {

	constructor( 
		private changeDetectorRef:ChangeDetectorRef, 	
		private shurahService:ShurahService,
		private route:ActivatedRoute,
    	private router:Router,
    	private auth: AuthService,
	){

	}
    private getRouteParamsSubscribe:any;
	suggestion:Suggestion
  	suggestionId:number
  	organisationId:number
	ngOnInit() {
		this.getRouteParamsSubscribe=this.route.params.subscribe(params=>{
        this.suggestionId=params['id'];
      	this.organisationId=params['organisationId'];
      	this.refresh();
     });
	}
	@Input("suggestion")
	@Input("has-edit-permissions")
	hasEditPermissions:boolean
	addCommentModel:any={};
	formatDate(dateText:string){
		return moment(dateText,"YYYYMMDDTHH:mm:ss").format("ddd Do MMM HH:mm:ss");
	}
	addComment(){
		this.shurahService.commentOnSuggestion(this.addCommentModel,this.suggestion.id).subscribe(r=>
			{
				var response = r;
				if(response.hasError){
					alert(response.error);
				}
				else{
					this.refresh();
					this.addCommentModel={};
				}
			});
	}
	showingComments:boolean=true;
	toggleShowComments(){
		this.showingComments=!this.showingComments;
		if(this.comments==null && this.showingComments){
			this.refresh();
		}else{
			this.changeDetectorRef.detectChanges();
		}
	}
	getPieChartData(suggestion:Suggestion){
		return [
		{
			label:"For",
			percentage:suggestion.percentFor,
			colour:'#00AA00'
		},
		{
			label:"Abstaining",
			percentage:suggestion.percentAbstaining,
			colour:'#ffc107'
		},
		{
			label:"Against",
			percentage:suggestion.percentAgainst,
			colour:'#AA0000'
		}
		]
	}
	vote(suggestionId:number,inFavour?:boolean){
		this.shurahService.voteOnSuggestion(suggestionId, inFavour).subscribe(response=>{
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
	comments:any[]
	
	refresh(){
		this.shurahService.suggestionDetails(this.suggestionId).subscribe(response=>{
	 		var model=response;
	 		if(model.hasError){
	 			this.suggestion=null;
	 		}else{
	 			this.suggestion=model.suggestionSummary;
	  			this.comments = model.comments;
	  			this.hasEditPermissions = model.usersOwnSuggestion || model.memberPermissions.indexOf("RemoveSuggestion")>=0;
	  			this.changeDetectorRef.detectChanges();

	 		}
	 	})  	
	}
	// refresh(){
	//  	this.shurahService.suggestionDetails(this.suggestion.id).subscribe(response=>{
	//  		var model=response;
	//  		if(model.hasError){
	//  			alert(model.error);
	//  		}else{
	//  			this.suggestion=model.suggestionSummary;
	//  			this.comments = model.comments;
	//  		}
	//  	})
	// }
	canDelete(){
		return this.hasEditPermissions || (this.suggestion!=null && this.suggestion.userIsSuggestionAuthor);
	}
	deleted:boolean=false;
	deleteSuggestion(){
		this.shurahService.deleteSuggestion(this.suggestion.id).subscribe(r=>{
			var response = r;
			if(response.hasError){
				alert(response.error);
			}
			this.deleted=true;
		})
	}
}
