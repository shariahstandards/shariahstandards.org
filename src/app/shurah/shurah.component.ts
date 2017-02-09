import { Component, OnInit,ChangeDetectorRef ,Input,Output,EventEmitter} from '@angular/core';
import { ShurahService } from '../shurah.service';
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
  @Input('section') section:any
  @Input('sub-sections') subSections:any[]
  @Input('allow-edit') allowEdit:boolean
  @Output() addSection:EventEmitter<any>=new EventEmitter<any>();
  onClickAdd(section){
    this.addSection.emit(section);
  }
}


export class addSectionModel{
  constructor(
  public name:string
    ){
    this.errors=[]
  }
  parentSection:any
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
  open(content, section) {
   this.addSectionModel= new addSectionModel("");
   this.addSectionModel.parentSection=section;
   this.activeModal= this.modalService.open(content);
   this.activeModal.result.then((result) => {
      this.closeResult = `Closed with: ${result}`;
    }, (reason) => {
      this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
    });
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
