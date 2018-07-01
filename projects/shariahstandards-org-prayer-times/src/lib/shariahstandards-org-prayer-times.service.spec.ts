//import  "jasmine"
import { TestBed, inject } from '@angular/core/testing';
import { timeZoneInfo } from './timeZoneInfo.interface';
import { prayerTimesForDay } from './prayerTimesForDay'
import { Observable,of} from 'rxjs'
import { ShariahstandardsOrgPrayerTimesService } from './shariahstandards-org-prayer-times.service';
import { Http, Response,ResponseOptions } from '@angular/http';
declare var require: any;
var moment = require('moment-timezone');


describe('ShariahstandardsOrgPrayerTimesService', () => {
  let prayerTimesService:ShariahstandardsOrgPrayerTimesService;
  var http;
  var configureService;
  beforeEach(() => {
      configureService=()=>{
      TestBed.configureTestingModule({
        providers: [
          ShariahstandardsOrgPrayerTimesService,
          {provide:Http,useValue:http}
        ]
      });
  }});
  describe("creating the service",()=>{
    beforeEach(()=>{
      http={};
      configureService();
    })
    beforeEach((inject([ShariahstandardsOrgPrayerTimesService], (service: ShariahstandardsOrgPrayerTimesService)=>{
      prayerTimesService=service;
    })));
    it('should be created', () => {
      expect(prayerTimesService).toBeTruthy();
    });
  });
  describe("testing method getPrayerTimes",()=>{
  	var lat =55.0;
  	var lng = 2.1;
  	var date= new Date();
    let zone:timeZoneInfo;
    zone={
      rawOffset:1,
      dstOffset:2,
      timeZoneName:"some zone",
      timeZoneId:"some id"};
  	var timeZoneResult = of<timeZoneInfo>(zone);
    let times:prayerTimesForDay;
    times=new prayerTimesForDay();
    beforeEach(()=>{
      http={};
      configureService();
    })
  	beforeEach((inject([ShariahstandardsOrgPrayerTimesService], (service: ShariahstandardsOrgPrayerTimesService)=>{
      prayerTimesService=service;
    })));
    beforeEach(()=>{
  		prayerTimesService.getTimeZone=jasmine.createSpy("get time zone info")
  			.and.returnValue(timeZoneResult);
      prayerTimesService.getPrayerTimesForTimeZone = jasmine.createSpy("get prayer times for time zone")
        .and.returnValue(times);
  	});
  	describe("when calling method getPrayerTimes",()=>{
  		let result:Observable<prayerTimesForDay>;
  		beforeEach(()=>{
  			result = prayerTimesService.getPrayerTimes(date,lng,lat);
  		});
      it("gets the time zone",()=>{
        result.subscribe(r=>{
          expect(prayerTimesService.getTimeZone).toHaveBeenCalledWith(date,lat,lng);
        })
      })
      it("builds the prayer times object using the time zone",()=>{
        result.subscribe(r=>{
          expect(prayerTimesService.getPrayerTimesForTimeZone).toHaveBeenCalledWith(date,zone,lng,lat);
          expect(r).toEqual(times);
        })
      })
  	});
  });
  describe("testing method getTimeZone",()=>{
    var lat =55.0;
    var lng = 2.1;
    var date= new Date();
    var middayMoment;
    let response:Response;
    let timeZoneInfo:timeZoneInfo;
    describe("when calling method getTimeZone",()=>{
      var result;
      //first mock injected dependencies
      beforeEach(()=>{
        response=new Response(new ResponseOptions());
        var observableResponse = of<Response>(response);
        http={get:jasmine.createSpy("get http")
          .and.returnValue(observableResponse)};
        configureService();
      })
      //then inject the service with its mocked dependencies and override methods with mocks
      beforeEach((inject([ShariahstandardsOrgPrayerTimesService], (service: ShariahstandardsOrgPrayerTimesService)=>{
        prayerTimesService=service;
        middayMoment=moment();
        prayerTimesService._getMiddayMoment=jasmine.createSpy("get midday").and.returnValue(middayMoment);
        timeZoneInfo={rawOffset:1,dstOffset:2,timeZoneName:"Berlin",timeZoneId:"CET"};
        prayerTimesService._getResponseObject=jasmine.createSpy("get response object")
          .and.returnValue(timeZoneInfo);
      })));

      beforeEach(()=>{
        prayerTimesService.getTimeZone(date,lat,lng).subscribe(r=>{
          result=r;
        })
      });
      it("gets the midday moment for the selected date",()=>{
        expect(prayerTimesService._getMiddayMoment).toHaveBeenCalledWith(date);
      })
      it("calls google's api to get the timezone information",()=>{
        expect(http.get).toHaveBeenCalledWith(
          'https://maps.googleapis.com/maps/api/timezone/json?location=' + lat + "," + lng + "&timestamp=" + middayMoment.unix());
      })
      it("converts the response to a timeZone info",()=>{
        expect(prayerTimesService._getResponseObject).toHaveBeenCalledWith(response);
      })
      it("returns the time zone info",()=>{
        expect(result).toEqual(timeZoneInfo);
      });
    });
  });
  
});
