import { async, ComponentFixture, TestBed } from '@angular/core/testing';
//import 'jasmine';
import { ShariahstandardsOrgPrayerTimesComponent } from './shariahstandards-org-prayer-times.component';
import {prayerTimesForDay} from './prayerTimesForDay'
import { ShariahstandardsOrgPrayerTimesService } from './shariahstandards-org-prayer-times.service';
import { Observable,of} from 'rxjs'

describe('ShariahstandardsOrgPrayerTimesComponent', () => {
  let component: ShariahstandardsOrgPrayerTimesComponent;
  let fixture: ComponentFixture<ShariahstandardsOrgPrayerTimesComponent>;
  var prayerTimesService={getPrayerTimes:undefined};
  var configureComponent;
  beforeEach(() => {
    configureComponent=()=>{
      TestBed.configureTestingModule({
        declarations: [ ShariahstandardsOrgPrayerTimesComponent ],
        providers:[
        {provide:ShariahstandardsOrgPrayerTimesService,useValue:prayerTimesService}]
      })
      .compileComponents();
    }
  });
  let times:Observable<prayerTimesForDay>;
  times=of<prayerTimesForDay>(new prayerTimesForDay())
  beforeEach(()=>{
    prayerTimesService.getPrayerTimes=jasmine.createSpy("get prayer times")
      .and.returnValue(times);
    configureComponent();
  }) 
  beforeEach(() => {
    fixture = TestBed.createComponent(ShariahstandardsOrgPrayerTimesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

});
