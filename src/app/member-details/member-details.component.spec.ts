/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';
import { Router, ActivatedRoute ,Params}       from '@angular/router';
import { MemberDetailsComponent } from './member-details.component';
import { ShurahService} from '../shurah.service';
import { HttpClient } from '@angular/common/http';
import { AuthService } from '../auth.service'
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import {ShurahNavigationComponent} from '../shurah-navigation/shurah-navigation.component'
import { RouterTestingModule } from '@angular/router/testing';
import {NO_ERRORS_SCHEMA} from '@angular/core';
import {Observable,of} from 'rxjs';


fdescribe('testing MemberDetailsComponent', () => {
  let component: MemberDetailsComponent;
  let fixture: ComponentFixture<MemberDetailsComponent>;
  var fakeActivatedRoute={params:undefined};
  var fakeAuthService={};
  var fakeShurahService={};
  var fakeNgbModal={};
  var fakeHttpClient={};
  var createComponent;
  beforeEach(() => {
    createComponent=()=>{
      TestBed.configureTestingModule({
        declarations: [ MemberDetailsComponent ,ShurahNavigationComponent],
        imports: [RouterTestingModule],
         providers:[
          {provide:ActivatedRoute,useValue:fakeActivatedRoute},
          {provide:ShurahService,useValue:fakeShurahService},
          {provide:AuthService,useValue:fakeAuthService},
          {provide:NgbModal,useValue:fakeNgbModal},
          {provide:HttpClient,useValue:fakeHttpClient}
          ],
         schemas:[NO_ERRORS_SCHEMA]
      })
      .compileComponents();
      fixture = TestBed.createComponent(MemberDetailsComponent);
      component = fixture.componentInstance;
      return component;
    }
  });
  describe("when creating the component",()=>{
    beforeEach(() => {
      component = createComponent();
    });
    it('should not be null', () => {
      expect(component).not.toBeNull();
    });
  });

  describe("testing method ngOnInit",()=>{
    let params:Params;
    params={"organisationId":99};
    let paramsObservable:Observable<Params>;
    beforeEach(()=>{
      paramsObservable = of<Params>(params);
      fakeActivatedRoute.params=paramsObservable;
      component = createComponent();
    })
    describe("when calling ngOnInit",()=>{
      beforeEach(()=>{
        component._routeParamsReady=jasmine.createSpy("route params ready");
        component.ngOnInit();
      })
      it("calls through to set the parameters",()=>{
        expect(component._routeParamsReady).toHaveBeenCalledWith(params);
      })
    })
  })
  beforeEach(()=>{
//    fixture.detectChanges();
  })
});
