import { Component, OnInit,ChangeDetectorRef ,Input,Output,EventEmitter} from '@angular/core';
import { ShurahService,membershipRuleSection} from '../shurah.service';
import { AuthService } from '../auth.service'
import {NgbModal, ModalDismissReasons, NgbModalRef} from '@ng-bootstrap/ng-bootstrap';

export interface organisation{
  permissions:string[]
}
@Component({
  selector:'sub-section',
  templateUrl:'./shurah.component.sub-section.html',
  styleUrls: ['./shurah.component.sub-section.css'],
})
export class subSection{
  @Input('section') section:membershipRuleSection
  @Input('sub-sections') subSections:membershipRuleSection[]
  @Input('allow-edit') allowEdit:boolean
  @Input('enable-paste') enablePaste:boolean
  @Output() addSection:EventEmitter<membershipRuleSection>=new EventEmitter<membershipRuleSection>();
  onClickAdd(section:membershipRuleSection){
    this.addSection.emit(section);
  }
  @Output() cutSection:EventEmitter<membershipRuleSection>=new EventEmitter<membershipRuleSection>();
  cut(section:membershipRuleSection){
    this.cutSection.emit(section);
  }
  @Output() pasteInto:EventEmitter<membershipRuleSection>=new EventEmitter<membershipRuleSection>();
  paste(section:membershipRuleSection){
    this.pasteInto.emit(section);
  }
  deleteEnabled(){
    return this.subSections==null || this.subSections.length==0;
  }
 @Output() deleteSection:EventEmitter<membershipRuleSection>=new EventEmitter<membershipRuleSection>();
  delete(section:membershipRuleSection){
    this.deleteSection.emit(section);
  }
}

export class addSectionModel{
  constructor(
  public name:string
    ){
    this.errors=[]
  }
  parentSection:membershipRuleSection
  urlSlug(){
  var cleanedText = this.name.toLowerCase().replace(/[^a-zA-Z0-9]/g, '-');
      cleanedText = cleanedText.replace(/--/g, '-');
      return cleanedText;
  }
  public errors:string[]
}

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
  open(content, section:membershipRuleSection) {
   this.addSectionModel= new addSectionModel("");
   this.addSectionModel.parentSection=section;
   this.activeModal= this.modalService.open(content);
   document.getElementById('sectionName').focus();
   this.activeModal.result.then((result) => {
      this.closeResult = `Closed with: ${result}`;
    }, (reason) => {
      this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
    });
  }
  sectionToDrop:membershipRuleSection
  cutSection(section:membershipRuleSection){
    this.sectionToDrop = section;
  }
  deleteSection(section:membershipRuleSection){
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
  pasteIntoSection(section:membershipRuleSection){
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

  addSectionModel:addSectionModel
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
    this.addSectionModel.errors=[];
    var parentSectionId=null;
    if(this.addSectionModel.parentSection){
      parentSectionId=this.addSectionModel.parentSection.id;
    }
    this.shurahService.createRuleSection(this.addSectionModel,1,parentSectionId).subscribe(result=>{
      var response = result.json();
      if(response.hasError){
        this.addSectionModel.errors=[response.error];
      }
      else{
        this.addSectionModel=new addSectionModel("");
        this.activeModal.close('section added');
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
