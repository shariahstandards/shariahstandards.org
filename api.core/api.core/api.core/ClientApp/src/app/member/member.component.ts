import { Component, OnInit } from '@angular/core';
import { ShurahService} from '../shurah.service';
import { AuthService } from '../auth.service'
import { Router, ActivatedRoute }       from '@angular/router';
import {NgbModal, ModalDismissReasons, NgbModalRef} from '@ng-bootstrap/ng-bootstrap';
import {ShurahNavigationComponent} from '../shurah-navigation/shurah-navigation.component'

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
  searchResultPages:any[]=[];
  lastPageLoaded:number=1
  organisationId:number

  ngOnInit() {
  	this.getRouteParamsSubscribe=this.route.params.subscribe(params=>{
        this.organisationId=params['organisationId'];
      	this.refresh();
     });       
  }
  refresh(){
 	  this.shurahService.searchForMembers(this.organisationId,this.lastPageLoaded).subscribe(response=>{
   		var model=response;
   		if(model.hasError){
   			alert(model.error);
   		}else{
   			this.searchResultPages=[model];
   		}
   	})
  }
  showMore(){
    this.lastPageLoaded++;
    this.shurahService.searchForMembers(this.organisationId,this.lastPageLoaded).subscribe(response=>{
       var model=response;
       if(model.hasError){
         alert(model.error);
       }else{
         this.searchResultPages.push(model);
       }
     })
  }
}
