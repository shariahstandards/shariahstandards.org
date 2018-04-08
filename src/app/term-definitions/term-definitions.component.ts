import { Component, OnInit, OnDestroy,Input} from '@angular/core';
import { Router, ActivatedRoute }       from '@angular/router';
import { TermComponent } from '../term/term.component';
import { ShurahService} from '../shurah.service';
import { addTermDefinitionModel} from './add-term-definition.model'
import {termDefinitionModel} from './term-definition.model'
import { AuthService } from '../auth.service';
import {NgbModal, ModalDismissReasons, NgbModalRef} from '@ng-bootstrap/ng-bootstrap';
import {updateTermDefinitionModel} from './update-term-definition.model'
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
    public auth: AuthService,
    private modalService: NgbModal,
    private router:Router
  	) {
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
  organisationId:number
  ngOnInit() {

    
	  this.getRouteParamsSubscribe=this.route.params.subscribe(params=>{
	    if(params['organisationId']!=null){
        this.organisationId=params['organisationId'];
      }else{
        this.organisationId=1;
      }
    	this.termId=params['termId'];
      this.shurahService.getPermissionsForOrganisation(this.organisationId).subscribe(res=>{
        this.permissions = res;
      });
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
    this.shurahService.createTermDefinition(this.addTermDefinitionModel,this.organisationId).subscribe(result=>{
      var response = result;
      if(response.hasError){
       this.addTermDefinitionModel.errors.push(response.error);
      }
      else{
        this.activeModal.close('term added');
        this.router.navigateByUrl("/terms/"+response.id+"/"+response.term);
      }
    })
  }
  updateTermDefinitionModel:updateTermDefinitionModel
  openModalToUpdateTermDefinition(content,definitionModel:any) {
   this.updateTermDefinitionModel= new updateTermDefinitionModel(definitionModel);
   this.activeModal= this.modalService.open(content);
   document.getElementById('term').focus();
   this.activeModal.result.then(()=>{
     this.refresh();
   })
  }
  deleteTerm(termId:number){
     this.shurahService.deleteTermDefinition(termId).subscribe(result=>{
      var response = result;
      if(response.hasError){
         alert(response.error);
      }
      else{
        this.router.navigateByUrl("/terms/"+this.organisationId);
      }
    }) 
  }
  updateTermDefinition(){
    this.updateTermDefinitionModel.errors=[];
    this.shurahService.updateTermDefinition(this.updateTermDefinitionModel).subscribe(result=>{
      var response = result;
      if(response.hasError){
       this.updateTermDefinitionModel.errors.push(response.error);
      }
      else{
        this.activeModal.close('term updater');
        this.router.navigateByUrl("/terms/"+this.organisationId+"/"
          +response.termId+"/"+response.term);
      }
    }) 
  }
  termId:number
  termList:any[]
  refresh(){
    if(this.termId!=null){
      this.shurahService.getTermDefinition(this.termId,this.organisationId).subscribe(result=>{
        var response = result;
        if(response.hasError){
         alert(response.error);
        }else{
          this.termDefinitionModel=response
        }
      })
    }
    this.shurahService.getTermList(this.organisationId).subscribe(result=>{
      var response = result;
      if(response.hasError){
       alert(response.error);
      }else{
        this.termList=response
      }
    })
  
  }
  ngOnDestroy() {
  	this.getRouteParamsSubscribe.unsubscribe();
  }
}
