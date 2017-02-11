import { Component, OnInit,ChangeDetectorRef ,Input,Output,EventEmitter} from '@angular/core';
import { ShurahService} from '../shurah.service';
import { AuthService } from '../auth.service'
import {NgbModal, ModalDismissReasons, NgbModalRef} from '@ng-bootstrap/ng-bootstrap';
import {membershipRuleSectionModel} from './membership-rule-section.model'
import {addMembershipRuleSectionModel} from './add-membership-rule-section.model'
import {updateMembershipRuleSectionModel} from './update-membership-rule-section.model'
import {organisation} from './organisation.model'
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
  updateMembershipRuleSectionModel:updateMembershipRuleSectionModel

  openModalToAddMembershipRuleSection(content, section:membershipRuleSectionModel) {
   this.addMembershipRuleSectionModel= new addMembershipRuleSectionModel("");
   this.addMembershipRuleSectionModel.parentSection=section;
   this.activeModal= this.modalService.open(content);
   document.getElementById('sectionName').focus();
   this.activeModal.result.then(()=>{
     this.refresh();
   })
  }
  openModalToUpdateMembershipRuleSection(content, section:membershipRuleSectionModel) {
   this.updateMembershipRuleSectionModel= new updateMembershipRuleSectionModel(section);
   this.activeModal= this.modalService.open(content);
   document.getElementById('sectionName').focus();
   this.activeModal.result.then(()=>{
     this.refresh();
   })
  }
  sectionToDrop:membershipRuleSectionModel
  cutSection(section:membershipRuleSectionModel){
    this.sectionToDrop = section;
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
