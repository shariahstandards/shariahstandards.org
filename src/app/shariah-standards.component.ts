import { Component, ViewContainerRef,ChangeDetectorRef} from '@angular/core';
import { PrayerTimesComponent } from './prayer-times/prayer-times.component'
import { AuthService } from './auth.service'
import { ShurahService} from './shurah.service';
import {organisationModel} from './shurah/organisation.model'
import {NgbModal, ModalDismissReasons, NgbModalRef} from '@ng-bootstrap/ng-bootstrap';
import {applyToJoinOrganisationModel} from './shurah/apply-to-join-organisation.model'
import { Router, NavigationStart } from '@angular/router';
// import { Routes, RouterModule,RouterLinkActive,RouterLink, } from '@angular/router';

// import {MODAL_DIRECTVES, BS_VIEW_PROVIDERS} from 'ng2-bootstrap/ng2-bootstrap';
// import {CORE_DIRECTIVES} from '@angular/common';
import {environment} from './environments/environment';
@Component({
  selector: 'shariah-standards-app',
  templateUrl: './shariah-standards.component.html',
  styleUrls: ['./shariah-standards.component.scss'],
  // directives: [PrayerTimesComponent, MODAL_DIRECTVES, CORE_DIRECTIVES,ROUTER_DIRECTIVES],
  // viewProviders: [BS_VIEW_PROVIDERS]

})
export class ShariahStandardsAppComponent {
  title = 'Muslim Prayer Times and Directions';
  organisationId=1;
  public constructor(
    private _router: Router,
    private viewContainerRef: ViewContainerRef,
    private changeDetectorRef:ChangeDetectorRef,
    private shurahService:ShurahService,
    private modalService: NgbModal,
    public auth: AuthService) {
    auth.handleAuthentication();
    auth.scheduleRenewal();
    auth.loggedIn.subscribe((loggedIn)=>{
      console.log("event subscribed")
      this.onLoggedInUpdated(loggedIn);

    });
    if(auth.isAuthenticated()){
    	auth.getProfile(()=>{
    		this.refresh();
    	});
    }else{
      let status;
      const token = localStorage.getItem('access_token');
      const expiresAt = JSON.parse(localStorage.getItem('expires_at'));
      const isExpired= new Date().getTime() > expiresAt;
      if(token!=null && isExpired){
        auth.renewToken(false);
      }
    }
    _router.events.subscribe ( event => {
      if( event instanceof NavigationStart ){
        this.showMobileNavigation=false;
      }
    } );
  }
  showMobileNavigation:boolean=false;
  showNavigationToggle(){
    this.showMobileNavigation=!this.showMobileNavigation;
  }
  showTitleInMobile:boolean=false;
  showTitleToggle(){
    this.showTitleInMobile=!this.showTitleInMobile;
  }
  onLoggedInUpdated(loggedIn){
    console.log("event received")

    this.refresh();
  }
  private activeModal:NgbModalRef
  closeResult: string;

  applyToJoinOrganisationModel:applyToJoinOrganisationModel
  openModalToJoinOrganisation(content){
    this.applyToJoinOrganisationModel=new applyToJoinOrganisationModel(this.organisation.name,this.organisation.id);
    this.activeModal= this.modalService.open(content);
    document.getElementById('publicName').focus();
    this.activeModal.result.then(()=>{
      this.refresh();
    },(reason)=>{
    });
    
  }
  applyToJoin(){
     this.shurahService.applyToJoinOrganisation(this.applyToJoinOrganisationModel).subscribe(result=>{
      
      var response = result;
      if(response.hasError){
        this.applyToJoinOrganisationModel.errors=[response.error];
        this.changeDetectorRef.detectChanges();
      }
      else{
        this.applyToJoinOrganisationModel=null;
        this.activeModal.close('member joined');
        this.refresh();
      }
    })
  }
  organisation:organisationModel
  refresh(){
    this.organisation=null;
    this.changeDetectorRef.detectChanges();
    this.shurahService.getOrganisation(this.organisationId).subscribe(result=>{
      this.organisation=result;
      this.changeDetectorRef.detectChanges();
      // this.changeDetectorRef.detectChanges();
      // console.log(JSON.stringify(this.organisation));
     })
  }
   hasPermission(permission:string){
    if(this.organisation==null)
    {
      return false
    }
    // console.log(JSON.stringify(this.organisation));
    return this.organisation.permissions.indexOf(permission)>=0
  }
}
