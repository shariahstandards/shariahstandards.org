import { Component, OnInit } from '@angular/core';
import { ShurahService} from '../shurah.service';
import { AuthService } from '../auth.service'
import { Router, ActivatedRoute }       from '@angular/router';
import {NgbModal, ModalDismissReasons, NgbModalRef} from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-member',
  templateUrl: './member.component.html',
  styleUrls: ['./member.component.css'],
  providers:[ShurahService]
  
})
export class MemberComponent implements OnInit {

  constructor(private route:ActivatedRoute,
    private router:Router,
    private auth: AuthService,
    private modalService: NgbModal,
  	private shurahService:ShurahService
) { }
  private getRouteParamsSubscribe:any;
  searchResults:any;
  currentPage:number
  organisationId:number

  ngOnInit() {
  	this.getRouteParamsSubscribe=this.route.params.subscribe(params=>{
        this.organisationId=params['organisationId'];
        if(params['page']!=null){
          this.currentPage=params['page'];
        }else{
          this.currentPage=1;
        }
      	this.refresh();
     });       
  }
  refresh(){
 	this.shurahService.searchForMembers(this.organisationId,this.currentPage).subscribe(response=>{
 		var model=response.json();
 		if(model.hasError){
 			alert(this.searchResults.error);
 		}else{
 			this.searchResults=model;
 		}
 	})
  }
}
