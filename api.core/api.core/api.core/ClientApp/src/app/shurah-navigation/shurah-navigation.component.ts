import { Component, OnInit,ChangeDetectorRef ,Input,Output,EventEmitter, OnDestroy} from '@angular/core';
import { ShurahService} from '../shurah.service';
import { AuthService } from '../auth.service';
import { RouterModule } from '@angular/router';


@Component({
  selector: 'app-shurah-navigation',
  templateUrl: './shurah-navigation.component.html',
  styleUrls: ['./shurah-navigation.component.css'],
  providers:[ShurahService]

})
export class ShurahNavigationComponent implements OnInit {

  constructor(
    private shurahService:ShurahService,
    private auth: AuthService) { 
  }
  hasPermission(permission:string){
    if(this.organisation==null)
    {
      return false
    }
    // console.log(JSON.stringify(this.organisation));
    return this.organisation.permissions.indexOf(permission)>=0
  }
  organisation:any
  ngOnInit(){
    if(this.organisationId){
       this.shurahService.getOrganisationSummary(this.organisationId).subscribe(result=>{
          this.organisation=result;
         })
     }
  }
 @Input('organisation-id') 
  organisationId:number
}