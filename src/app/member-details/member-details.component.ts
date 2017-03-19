import { Component, OnInit } from '@angular/core';
import { ShurahService} from '../shurah.service';
import { AuthService } from '../auth.service'
import { Router, ActivatedRoute }       from '@angular/router';
import {NgbModal, ModalDismissReasons, NgbModalRef} from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-member-details',
  templateUrl: './member-details.component.html',
  styleUrls: ['./member-details.component.css'],
  providers:[ShurahService]
})
export class MemberDetailsComponent implements OnInit {

  constructor(private route:ActivatedRoute,
    private router:Router,
    private auth: AuthService,
    private modalService: NgbModal,
  	private shurahService:ShurahService) { }
  private getRouteParamsSubscribe:any;
  currentPage:number
  organisationId:number
  memberId:number
  member:any

  ngOnInit() {
  		this.getRouteParamsSubscribe=this.route.params.subscribe(params=>{
        this.organisationId=params['organisationId'];
        this.memberId=params['memberId'];
        if(params['page']!=null){
          this.currentPage=params['page'];
        }else{
          this.currentPage=1;
        }
      	this.refresh();
     });   
  }
   refresh(){
 	this.shurahService.memberDetails(this.memberId).subscribe(response=>{
 		var model=response.json();
 		if(model.hasError){
 			alert(model.error);
 		}else{
 			this.member=model;
 		}
 	})
 }
 follow(){
   this.shurahService.follow(this.memberId).subscribe(r=>{
     var response = r.json();
     if(response.hasError){
       alert(response.error);
     }else{
       this.refresh();
     }
   })
 }
 unfollow(){
   this.shurahService.unfollow(this.organisationId).subscribe(r=>{
     var response = r.json();
     if(response.hasError){
       alert(response.error);
     }else{
       this.refresh();
     }
   })
 }

}
