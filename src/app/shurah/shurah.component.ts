import { Component, OnInit,ChangeDetectorRef } from '@angular/core';
import { ShurahService } from '../shurah.service';
import { AuthService } from '../auth.service'

export interface organisation{
  permissions:string[]
}
export class addSectionModel{
  constructor(
  public name:string
    ){
    this.errors=[]
  }
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
    private auth: AuthService
) { 
    this.addSectionModel= new addSectionModel("");
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
    this.shurahService.createRuleSection(this.addSectionModel,1).subscribe(result=>{
      var response = result.json();
      if(response.hasError){
        this.addSectionModel.errors=[response.error];
      }
      else{
        this.addSectionModel=new addSectionModel("");
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
