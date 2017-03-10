import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute }       from '@angular/router';
import { ShurahService} from '../shurah.service';
import {membershipApplicationView} from './membership-application.view'
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

}
