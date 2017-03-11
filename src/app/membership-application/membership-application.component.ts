import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute }       from '@angular/router';
import { ShurahService} from '../shurah.service';
import {membershipApplicationView} from './membership-application.view'
import {RejectMembershipApplicationModel} from './reject-membership-application.model'
import {NgbModal, ModalDismissReasons, NgbModalRef} from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-membership-application',
  templateUrl: './membership-application.component.html',
  styleUrls: ['./membership-application.component.css'],
  providers:[ShurahService]

})
export class MembershipApplicationComponent implements OnInit {

  constructor(
    private shurahService:ShurahService,
  	private route:ActivatedRoute,
    private modalService: NgbModal,
    private router:Router
 ) { }
	private getRouteParamsSubscribe:any;
  	organisationId:number;
  	page:number;
  	ngOnInit() {
  	  this.getRouteParamsSubscribe=this.route.params.subscribe(params=>{
        if(params['organisationId']){
            this.organisationId=params['organisationId'];
		  	if(params['page']){
	            this.page=params['page'];
	        }else{
	        	this.page=1;
	        }
	        this.refresh();
	    }
     });  
  	}
  	results:membershipApplicationView[]
  	rejectMembershipApplicationModel:RejectMembershipApplicationModel
  	refresh(){
  		this.shurahService.viewApplications(this.organisationId,this.page).subscribe(r=>{
  			var response =r.json();
  			if(response.hasError){
  				alert(response.error);
  			}else{
  				this.results=response.results;
  			}
  		})
  	}
  	accept(applicationId:number){
  		this.shurahService.acceptMembershipApplication(applicationId).subscribe(r=>{
  			var response =r.json();
  			if(response.hasError){
  				alert(response.error);
  			}else{
  				this.refresh();
  			}
  		})	
  	}
  	private activeModal:NgbModalRef
  
  	openModalToRejectApplication(content:any, applicationId:number){
    	this.rejectMembershipApplicationModel= new RejectMembershipApplicationModel(applicationId);
    	this.activeModal= this.modalService.open(content);
     	document.getElementById('reason').focus();
     	this.activeModal.result.then(()=>{
       		this.refresh();
    	});
	}

	reject(){
    	this.shurahService.rejectMembershipApplication(this.rejectMembershipApplicationModel).subscribe(result=>{
	      	var response = result.json();
	      	if(response.hasError){
	        	this.rejectMembershipApplicationModel.errors=[response.error];
	      	}
	      	else{
		        this.rejectMembershipApplicationModel=null;
		        this.activeModal.close('application rejected');
		        this.refresh();
	      	}
	    })
	}
}
