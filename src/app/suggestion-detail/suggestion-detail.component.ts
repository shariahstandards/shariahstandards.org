import { Component, OnInit ,Input, ChangeDetectorRef} from '@angular/core';
import {Suggestion} from './suggestion.interface';
import { ShurahService} from '../shurah.service';

import 'moment';
declare var moment: any;

@Component({
	selector: 'suggestion-detail',
	templateUrl: './suggestion-detail.component.html',
	styleUrls: ['./suggestion-detail.component.css'],
  	providers:[ShurahService]
})
export class SuggestionDetailComponent implements OnInit {

	constructor( private changeDetectorRef:ChangeDetectorRef, 	
		private shurahService:ShurahService
	){

	}

	ngOnInit() {
	}
	@Input("suggestion")
	suggestion:Suggestion
	@Input("has-edit-permissions")
	hasEditPermissions:boolean
	addCommentModel:any={};
	formatDate(dateText:string){
		return moment(dateText,"YYYYMMDDTHH:mm:ss").format("ddd Do MMM HH:mm:ss");
	}
	addComment(){
		// this.shurahService.commentOnSuggestion(this.addCommentModel,this.suggestion.id).subscribe(r=>
		// 	{
		// 		var response = r.json();
		// 		if(response.hasError){
		// 			alert(response.error);
		// 		}
		// 		else{
		// 			this.refresh();
		// 			this.addCommentModel={};
		// 		}
		// 	});
	}
	showingComments:boolean=false;
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
			label:"Against",
			percentage:suggestion.percentAgainst,
			colour:'#AA0000'
		},
		{
			label:"Abstaining",
			percentage:suggestion.percentAbstaining,
			colour:'#00AAAA'
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
	 	this.shurahService.suggestionDetails(this.suggestion.id).subscribe(response=>{
	 		var model=response;
	 		if(model.hasError){
	 			alert(model.error);
	 		}else{
	 			this.suggestion=model.suggestionSummary;
	 			this.comments = model.comments;
	 		}
	 	})
	}
	canDelete(){
		return this.hasEditPermissions || this.suggestion.userIsSuggestionAuthor;
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
