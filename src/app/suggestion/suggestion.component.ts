
import { Component, OnInit } from '@angular/core';
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
  	private shurahService:ShurahService
  	) { }
  private getRouteParamsSubscribe:any;
  searchResults:any;
  currentPage:number
  ngOnInit() {
      this.getRouteParamsSubscribe=this.route.params.subscribe(params=>{
      	this.currentPage=params['page'] || 1;
      	this.refresh();
     });       
  }
  formatDate(dateText:string){
    return moment(dateText,"YYYYMMDDTHH:mm:ss").format("HH:mm:ss ddd Do MMM YYYY");
  }
  refresh(){
	 	this.shurahService.searchSuggestions(1,this.currentPage).subscribe(response=>{
	 		var model=response.json();
	 		if(model.hasError){
	 			alert(this.searchResults.error);
	 		}else{
	 			this.searchResults=model;
	 		}
	 	})
  }
    AddSuggestion(){
    this.shurahService.addSuggestion(1,this.addSuggestionModel).subscribe(response=>{
    	var data = response.json();
    	if(data.hasError){
    		this.addSuggestionModel.errors=[data.error];
    	}
    	else{
    		this.refresh();
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
