import { Component, OnInit, OnDestroy,Input} from '@angular/core';
import { Router, ActivatedRoute }       from '@angular/router';
import { TermComponent } from '../term/term.component';
import { ShurahService} from '../shurah.service';
import { addTermDefinitionModel} from './add-term-definition.model'
import {termDefinitionModel} from './term-definition.model'
import { AuthService } from '../auth.service';
import {NgbModal, ModalDismissReasons, NgbModalRef} from '@ng-bootstrap/ng-bootstrap';

@Component({
  //moduleId: module.id,
  selector: 'app-term-definitions',
  templateUrl: './term-definitions.component.html',
  styleUrls: ['./term-definitions.component.css'],
  providers:[ShurahService]
//  directives:[TermComponent]
})
export class TermDefinitionsComponent implements OnInit, OnDestroy  {
  //private route:ActivatedRoute;
  constructor( 
  	private route:ActivatedRoute,
    private shurahService:ShurahService,
    private auth: AuthService,
    private modalService: NgbModal,
    private router:Router
  	) {
    this.refresh();
  }
  termDefinitionModel:termDefinitionModel
  addTermDefinitionModel:addTermDefinitionModel
  permissions:string[]=[]
  private getRouteParamsSubscribe:any;
  allow(action:string)
  {
    if(this.permissions.some(p=>{
      return p==action;
    })){
      return true;
    }
    return false;
  }
  ngOnInit() {
    this.shurahService.getPermissionsForOrganisation(1).subscribe(res=>{
      this.permissions = res.json();
    });
	  this.getRouteParamsSubscribe=this.route.params.subscribe(params=>{
	  	this.termId=params['termId'];
	  	this.refresh();
	 });
  }
  private activeModal:NgbModalRef
  
  openModalToAddTermDefinition(content) {
   this.addTermDefinitionModel= new addTermDefinitionModel();
   this.activeModal= this.modalService.open(content);
   document.getElementById('term').focus();
   this.activeModal.result.then(()=>{
     this.ngOnInit();
   })
  }
  addTermDefinition(){
    this.addTermDefinitionModel.errors=[];
    this.shurahService.createTermDefinition(this.addTermDefinitionModel,1).subscribe(result=>{
      var response = result.json();
      if(response.hasError){
       this.addTermDefinitionModel.errors.push(response.error);
      }
      else{
        this.activeModal.close('rule added');
        this.router.navigateByUrl("/terms/"+response.id+"/"+response.term);
      }
    })
  }
  termId:number
  refresh(){
    if(this.termId!=null){
      this.shurahService.getTermDefinition(this.termId,1).subscribe(result=>{
        var response = result.json();
        if(response.hasError){
         alert(response.error);
        }else{
          this.termDefinitionModel=response
        }
      })
    }
  }
  ngOnDestroy() {
  	this.getRouteParamsSubscribe.unsubscribe();
  }
}
