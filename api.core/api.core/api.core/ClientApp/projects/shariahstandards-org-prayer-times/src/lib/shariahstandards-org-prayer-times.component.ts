import { Component, OnInit,Input } from '@angular/core';
import {ShariahstandardsOrgPrayerTimesService} from './shariahstandards-org-prayer-times.service';
import {prayerTimesForDay} from './prayerTimesForDay'
@Component({
  selector: 'shariahstandards-org-prayer-times',
  templateUrl: "./shariahstandards-org-prayer-times.component.html",
  styleUrls: ["./shariahstandards-org-prayer-times.component.scss"]
})
export class ShariahstandardsOrgPrayerTimesComponent implements OnInit
 {

  prayerTimes:prayerTimesForDay;
  constructor(private service:ShariahstandardsOrgPrayerTimesService) { }
  @Input('hideLogo') 
  hideLogo: boolean;
   @Input('latitude') 
  latitude: number;
  @Input('longitude') 
  longitude: number;
  @Input('locationName') 
  locationName: string;
  @Input('date') 
  date: Date;
  ngOnInit() {
   this.reset();
  }
  reset(){
    this.service.getPrayerTimes(this.date==null?new Date():this.date,this.longitude,this.latitude).subscribe(p=>{
      this.prayerTimes=p;
    }) 
  }

}
