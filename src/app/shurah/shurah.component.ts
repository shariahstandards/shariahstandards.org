import { Component, OnInit } from '@angular/core';
import { ShurahService } from '../shurah.service';
import { AuthService } from '../auth.service'

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
    //private changeDetectorRef:ChangeDetectorRef,
    private shurahService:ShurahService,
    private auth: AuthService
) { }

  ngOnInit() {
    this.refresh();
  }
  refresh(){
    this.shurahService.getRootOrganisation().subscribe(result=>{
       this.rootOrganisation=result.json();
      // this.changeDetectorRef.detectChanges();
      // console.log(JSON.stringify(this.rootOrganisation));
     })
  }
  join(){
      this.shurahService.join().subscribe(result=>{
        var response = result.json();
        if(response.hasError){
          alert(response.error);
        }else{
          this.refresh();
        }
      });
    }
  leave(){
       this.shurahService.leave().subscribe(result=>{
        var response = result.json();
        if(response.hasError){
          alert(response.error);
        }else{
          this.refresh();
        }
      });
    }
  rootOrganisation:{}
}
