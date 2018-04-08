
import { Component, OnInit ,ChangeDetectorRef} from '@angular/core';
import { ShurahService} from '../shurah.service';
import { AuthService } from '../auth.service'
import { Router, ActivatedRoute }       from '@angular/router';
import {NgbModal, ModalDismissReasons, NgbModalRef} from '@ng-bootstrap/ng-bootstrap';
import {addSuggestionModel} from './add-suggestion-model'
import 'moment';
declare var moment: any;
@Component({
  selector: 'app-suggestion',
  templateUrl: './suggestion.component.html',
  styleUrls: ['./suggestion.component.css'],
  providers:[ShurahService]

})
export class SuggestionComponent implements OnInit {

  constructor(
    private route:ActivatedRoute,
    private router:Router,
    private auth: AuthService,
    private modalService: NgbModal,
  	private shurahService:ShurahService,
    private changeDetectorRef:ChangeDetectorRef
  	) { }
  private getRouteParamsSubscribe:any;
  searchResultPages:any[]=[];
  memberId?:number
  organisationId:number
  ngOnInit() {
      this.getRouteParamsSubscribe=this.route.params.subscribe(params=>{
        this.organisationId=params['organisationId'];
        if(params['memberId']!=null){
          this.memberId=params['memberId'];
        }
      	this.refresh();
     });       
  }
  getPieChartData(suggestion:any){
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
  formatDate(dateText:string){
    return moment(dateText,"YYYYMMDDTHH:mm:ss").format("ddd Do MMM YYYY");
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
  refresh(){
    this.lastPageShown=1;
	 	this.shurahService.searchSuggestions(this.organisationId,1,this.sortingByMostRecentFirst,this.memberId).subscribe(response=>{
	 		var model=response;
	 		if(model.hasError){
	 			alert(model.error);
	 		}else{
	 			this.searchResultPages=[model];
         this.changeDetectorRef.detectChanges();
	 		}
	 	})
  }
  AddSuggestion(){
    this.shurahService.addSuggestion(this.organisationId,this.addSuggestionModel).subscribe(response=>{
    	var data = response;
    	if(data.hasError){
    		this.addSuggestionModel.errors=[data.error];
    	}
    	else{
        this.activeModal.close('suggestion added');
    		this.refresh();
    	}
    })
  }
  sortingByMostRecentFirst:boolean=false;
  showMostSupportedSuggestionsFirst(){
    this.sortingByMostRecentFirst=false;
    this.refresh();
  }
  showMostRecentSuggestionsFirst(){
   this.sortingByMostRecentFirst=true;
    this.refresh(); 
  }
  lastPageShown:number
  showMore(){
   if(this.searchResultPages.length==0 || this.lastPageShown>=this.searchResultPages[0].pageCount){
      return;
   }
   this.lastPageShown++;
   this.shurahService.searchSuggestions(this.organisationId,this.lastPageShown,this.sortingByMostRecentFirst,this.memberId).subscribe(response=>{
       var model=response;
       if(model.hasError){
         alert(model.error);
       }else{
         this.searchResultPages.push(model);
       }
     }) 
  }
  private activeModal:NgbModalRef
  private addSuggestionModel:addSuggestionModel
  openModalToAddSuggestion(content) {
   this.addSuggestionModel= new addSuggestionModel();
   this.activeModal= this.modalService.open(content);
   document.getElementById('subject').focus();
   this.activeModal.result.then(()=>{
     this.refresh();
   })
  }
}
