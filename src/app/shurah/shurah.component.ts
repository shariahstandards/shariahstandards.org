import { Component, OnInit,ChangeDetectorRef ,Input,Output,EventEmitter} from '@angular/core';
import { ShurahService} from '../shurah.service';
import { AuthService } from '../auth.service'
import {NgbModal, ModalDismissReasons, NgbModalRef} from '@ng-bootstrap/ng-bootstrap';
import {membershipRuleSectionModel} from './membership-rule-section.model'
import {membershipRuleModel} from './membership-rule.model'
import {addMembershipRuleSectionModel} from './add-membership-rule-section.model'
import {updateMembershipRuleSectionModel} from './update-membership-rule-section.model'
import {updateMembershipRuleModel} from './update-membership-rule.model'
import {organisation} from './organisation.model'
import {addMembershipRuleModel} from './add-membership-rule.model'
@Component({
  selector: 'app-shurah',
  templateUrl: './shurah.component.html',
  styleUrls: ['./shurah.component.css'],
  providers:[ShurahService]
})
export class ShurahComponent implements OnInit {

  constructor(
	//private route:ActivatedRoute,
    //private router:Router,
    private changeDetectorRef:ChangeDetectorRef,
    private shurahService:ShurahService,
    private auth: AuthService,
    private modalService: NgbModal,
) { 

  }
  private activeModal:NgbModalRef
  closeResult: string;
  addMembershipRuleSectionModel:addMembershipRuleSectionModel
  openModalToAddMembershipRuleSection(content, section:membershipRuleSectionModel) {
   this.addMembershipRuleSectionModel= new addMembershipRuleSectionModel("");
   this.addMembershipRuleSectionModel.parentSection=section;
   this.activeModal= this.modalService.open(content);
   document.getElementById('sectionName').focus();
   this.activeModal.result.then(()=>{
     this.refresh();
   })
  }
  updateMembershipRuleSectionModel:updateMembershipRuleSectionModel
  openModalToUpdateMembershipRuleSection(content, section:membershipRuleSectionModel) {
   this.updateMembershipRuleSectionModel= new updateMembershipRuleSectionModel(section);
   this.activeModal= this.modalService.open(content);
   document.getElementById('sectionName').focus();
   this.activeModal.result.then(()=>{
     this.refresh();
   })
  }
  updateMembershipRuleModel:updateMembershipRuleModel
  openModalToUpdateMembershipRule(content, rule:membershipRuleModel) {
   this.updateMembershipRuleModel= new updateMembershipRuleModel(rule);
   this.activeModal= this.modalService.open(content);
   document.getElementById('ruleStatement').focus();
   this.activeModal.result.then(()=>{
     this.refresh();
   })
  }
  sectionToDrop:membershipRuleSectionModel
  cutSection(section:membershipRuleSectionModel){
    this.sectionToDrop = section;
    this.ruleToDrop = null
  }
  ruleToDrop:membershipRuleModel
  cutRule(rule:membershipRuleModel){
   this.sectionToDrop=null;
    this.ruleToDrop = rule
  }

  deleteRule(rule:membershipRuleModel){
     this.shurahService.deleteRule(rule).subscribe(result=>{
      var response = result.json();
       if(this.ruleToDrop!=null && this.ruleToDrop.id==rule.id){
        this.ruleToDrop=null;
      }
      if(response.hasError){
        alert(response.error);
      }else{
          this.refresh();
        }

    })
  }
  updateRule(){
    this.shurahService.updateMembershipRule(this.updateMembershipRuleModel).subscribe(result=>{
      var response = result.json();
      if(response.hasError){
        this.updateMembershipRuleModel.errors=[response.error];
      }
      else{
        this.updateMembershipRuleModel=null;
        this.activeModal.close('rule updated');
        this.refresh();
      }
    })
  }
  deleteSection(section:membershipRuleSectionModel){
    this.shurahService.deleteRuleSection(section).subscribe(result=>{
      var response = result.json();
      if(this.sectionToDrop!=null && this.sectionToDrop.id==section.id){
        this.sectionToDrop=null;
      }
      if(response.hasError){
        alert(response.error);
      }else{
          this.refresh();
        }

    })
  }
  pasteIntoSection(section:membershipRuleSectionModel){
    this.shurahService.dragDropRuleSection(this.sectionToDrop,section).subscribe(result=>{
      var response = result.json();
      this.sectionToDrop=null;
      if(response.hasError){
        alert(response.error);
      }else{
          this.refresh();
        }

    })
  }
  pasteRule(rule:membershipRuleModel){
     this.shurahService.dragDropRule(this.ruleToDrop,rule).subscribe(result=>{
      var response = result.json();
      this.ruleToDrop=null;
      if(response.hasError){
        alert(response.error);
      }else{
          this.refresh();
        }

    })
  }
  private getDismissReason(reason: any): string {
    if (reason === ModalDismissReasons.ESC) {
      return 'by pressing ESC';
    } else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
      return 'by clicking on a backdrop';
    } else {
      return  `with: ${reason}`;
    }
  }
  ngOnInit() {
    this.refresh();
  }

  allow(action:string)
  {
    if(this.rootOrganisation.permissions.some(p=>{
      return p==action;
    })){
      return true;
    }
    return false;
  }
  addMembershipRuleModel:addMembershipRuleModel
  openModalToCreateRuleInSection(content,section:membershipRuleSectionModel){
     this.addMembershipRuleModel= new addMembershipRuleModel(section);
     this.activeModal= this.modalService.open(content);
     document.getElementById('ruleStatement').focus();
     this.activeModal.result.then(()=>{
       this.refresh();
     })
  }
  addRule(){
     this.addMembershipRuleModel.errors=[];
   
    this.shurahService.createRule(this.addMembershipRuleModel).subscribe(result=>{
      var response = result.json();
      if(response.hasError){
        this.addMembershipRuleModel.errors=[response.error];
      }
      else{
        this.addMembershipRuleModel=new addMembershipRuleModel(this.addMembershipRuleModel.section);
        this.activeModal.close('rule added');
        this.refresh();
      }
    })
  }

  beginAddRuleSection(){
    this.addMembershipRuleSectionModel.errors=[];
    var parentSectionId=null;
    if(this.addMembershipRuleSectionModel.parentSection){
      parentSectionId=this.addMembershipRuleSectionModel.parentSection.id;
    }
    this.shurahService.createRuleSection(this.addMembershipRuleSectionModel,1,parentSectionId).subscribe(result=>{
      var response = result.json();
      if(response.hasError){
        this.addMembershipRuleSectionModel.errors=[response.error];
      }
      else{
        this.addMembershipRuleSectionModel=new addMembershipRuleSectionModel("");
        this.activeModal.close('section added');
        this.refresh();
      }
    })
  }
  beginUpdateMembershipRuleSection(){
    this.updateMembershipRuleSectionModel.errors=[];
    this.shurahService.updateMembershipRuleSection(this.updateMembershipRuleSectionModel).subscribe(result=>{
      var response = result.json();
      if(response.hasError){
        this.updateMembershipRuleSectionModel.errors=[response.error];
      }
      else{
        this.updateMembershipRuleSectionModel=null;
        this.activeModal.close('section updated');
        this.refresh();
      }
    }) 
  }
  refresh(){
    this.rootOrganisation=null;
    this.changeDetectorRef.detectChanges();
    this.shurahService.getRootOrganisation().subscribe(result=>{
      this.rootOrganisation=result.json();
      this.hideButtons=false;

      this.changeDetectorRef.detectChanges();
      // this.changeDetectorRef.detectChanges();
      // console.log(JSON.stringify(this.rootOrganisation));
     })
  }
  join(){
      this.hideButtons=true;

      this.shurahService.join().subscribe(result=>{
        var response = result.json();
        if(response.hasError){
          alert(response.error);
        }else{
          this.refresh();
        }
      });
    }
  hideButtons:boolean=false;
  leave(){
      this.hideButtons=true;
       this.shurahService.leave().subscribe(result=>{
        var response = result.json();
        if(response.hasError){
          alert(response.error);
        }else{
          this.refresh();
        }
      });
    }
  rootOrganisation:organisation
}
