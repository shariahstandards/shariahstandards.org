import { Injectable } from '@angular/core';
import {timeZoneInfo} from './timeZoneInfo.interface'
import {prayerTimesForDay} from './prayerTimesForDay'
declare var require: any;
var SunCalc = require("SunCalc");
var moment = require('moment-timezone');

import { Http, Response } from '@angular/http';
import {Observable} from 'rxjs';
import { catchError, map } from "rxjs/operators";
import { of } from "rxjs/observable/of";


@Injectable({
  providedIn: 'root'
})
export class ShariahstandardsOrgPrayerTimesService {

  	constructor(private http: Http) { }

  	_getMiddayMoment(date:Date):any{
    	// throw "not implemented";
		return moment(date).startOf('d').add(12, 'h');
  	}
  	_getResponseObject<T>(response:Response){
  		return <T>response.json();
  	}
    getTimeZone(date: Date, latitude: number, longitude: number): Observable<timeZoneInfo> {
    	var defaultZoneInfo={rawOffset: 0,	dstOffset: 0,	timeZoneName: "Greewich mean time",	timeZoneId:"GMT",status:"OK"};
    	var dateMoment = this._getMiddayMoment(date);
		 var url = 'https://maps.googleapis.com/maps/api/timezone/json?location=' + latitude + "," + longitude
		 	+ "&timestamp=" + dateMoment.unix();
		 return this.http.get(url).pipe(
		 	map(r=>this._getResponseObject<timeZoneInfo>(r)),
		 	map(model=>{
		 		if(model.status=="ZERO_RESULTS")
		 		{
		 			return defaultZoneInfo;
			 	}
			 	return model;

			 }),
			catchError(error=>of(defaultZoneInfo))
		 );
	};
	getPrayerTimes(date:Date,lng:number,lat:number):Observable<prayerTimesForDay> {
        var self = this;

        return self.getTimeZone(date,lat,lng).pipe(
        	map(z=>self.getPrayerTimesForTimeZone(date,z,lng,lat))
        	);
		
	}
	
	getAdjustedTimes(latitude: number, longitude: number, date: Date, utcOffset: number): any {
		var unadjustedLatitude = latitude;
		var increment = 0.01;
		if (latitude > 0) {
			increment = -0.01;
		}
			//		var originaltimes = SunCalc.getTimes(date, latitude, longitude);
		var times = SunCalc.getTimes(date, latitude, longitude);
		var originalTimes = SunCalc.getTimes(date, latitude, longitude);
		var fajrIsAdjusted = false;

		if(!moment(originalTimes.fajr).isValid()
			|| moment(originalTimes.solarNoon).diff(moment(originalTimes.fajr), "hours") >= 10) {
			times.fajr = moment(times.solarNoon).subtract(10, "hours").toDate();
			times.isha = moment(times.solarNoon).add(10, "hours").toDate();
			fajrIsAdjusted = true;
		}
		var maghribIsAdjusted = false;
		if ((!moment(originalTimes.sunset).isValid() && !moment(originalTimes.fajr).isValid())
			|| moment(originalTimes.sunset).diff(moment(originalTimes.solarNoon), "hours") >= 9) {
			times.sunrise = moment(times.solarNoon).subtract(9, "hours").toDate();
			times.sunset = moment(times.solarNoon).add(9, "hours").toDate();
			maghribIsAdjusted = true;
		}
		var maghribIsAdjustedLater = false;
		if ((!moment(originalTimes.sunset).isValid() && moment(originalTimes.fajr).isValid())
			|| moment(originalTimes.sunset).diff(moment(originalTimes.solarNoon), "hours") <= 2) {
			times.sunrise = moment(times.solarNoon).subtract(2, "hours").toDate();
			times.sunset = moment(times.solarNoon).add(2, "hours").toDate();
			maghribIsAdjustedLater = true;
		}
		var fajrIsAdjustedEarlier = false;
		// if (!moment(times.fajr).isValid() 
		// 	|| moment(times.solarNoon).diff(moment(times.fajr), "hours") <= 3) {
		// 	times.fajr = moment(times.solarNoon).subtract(3, "hours").toDate();
		// 	times.isha = moment(times.solarNoon).add(3, "hours").toDate();
		// 	fajrIsAdjustedEarlier = true;
		// }


		// while (Math.abs(latitude) > 10 && (
		// 	!moment(times.sunrise).isValid()
		// 	|| !this.hasNormalFullDaylightLength(times)
		// )) {
		// 	latitude = latitude + increment;
		// 	times = SunCalc.getTimes(date, latitude, longitude);
		// }
		// var validSunriseTimes = times;
		// var sunriseAdjustedLatitude = latitude;
		// while (Math.abs(latitude) > 10 && (
		// 	!moment(times.fajr).isValid()
		// 	|| !this.hasNormalDayLength(times)
		// )) {
		// 	latitude = latitude + increment;
		// 	times = SunCalc.getTimes(date, latitude, longitude);
		// }
		//		var validFajrTimes = times;
		//		var fajrAdjustedLatitude = latitude;

		var noon = moment(times.solarNoon);
		var sunset = moment(times.sunset);
		var minutesDifference = sunset.diff(noon, "minutes");
		var midAfternoon = moment(noon).add(minutesDifference * 2.0 / 3.0, "minutes");
		var timeFormat = "HH:mm";
		var moonAtIsha = SunCalc.getMoonIllumination(times.isha);
		var isha1DaysAgo = moment(times.isha).subtract(1, 'd').toDate();
		var isha2DaysAgo = moment(times.isha).subtract(2, 'd').toDate();
		var isha3DaysAgo = moment(times.isha).subtract(3, 'd').toDate();
		var moonAtPreviousIsha1DaysAgo = SunCalc.getMoonIllumination(isha1DaysAgo);
		var moonAtPreviousIsha2DaysAgo = SunCalc.getMoonIllumination(isha2DaysAgo);
		var moonAtPreviousIsha3DaysAgo = SunCalc.getMoonIllumination(isha3DaysAgo);
		var moonAtMaghrib = SunCalc.getMoonIllumination(times.sunset);
		//var moonPositionAtMaghrib = SunCalc.getMoonPosition(times.sunset);
		var moonPositionAtIsha= SunCalc.getMoonPosition(times.isha);
		
		//1 day old moon at isha is deemed 100% visible
		var moonVisibilityAtIsha = Math.min(1,moonAtIsha.phase*29.5306);
		var moonVisibilityAtIshaYesterday = Math.min(1,moonAtPreviousIsha1DaysAgo.phase*29.5306);
		var isStartOfLunarMonthYesterday=(moonAtPreviousIsha3DaysAgo.phase>moonAtPreviousIsha1DaysAgo.phase 
			&& moonVisibilityAtIshaYesterday==1);
		var startOfLunarMonthToday=(!isStartOfLunarMonthYesterday &&
			moonAtPreviousIsha2DaysAgo.phase>moonAtIsha.phase && moonVisibilityAtIsha==1);
				
	//	var maghribYesterday = moment(times.sunset).subtract(1, 'd').toDate();;
	//	var moonAtPreviousMaghrib = SunCalc.getMoonIllumination(maghribYesterday);
        //adding 1 minute to each prayer time to correct for seconds truncation in time format
		var response =
			{
				moonVisibility:(moonVisibilityAtIsha*100.0).toFixed(1),
				startOfLunarMonth:startOfLunarMonthToday,
				maghribIsAdjusted: maghribIsAdjusted,
				fajrIsAdjusted: fajrIsAdjusted,
				maghribIsAdjustedLater: maghribIsAdjustedLater,
				fajrIsAdjustedEarlier: fajrIsAdjustedEarlier,
				fajr: moment(times.fajr).utcOffset(utcOffset).add(1,'m').format(timeFormat),
				sunrise: moment(times.sunrise).utcOffset(utcOffset).add(1, 'm').format(timeFormat),
				zuhr: noon.utcOffset(utcOffset).add(1, 'm').format(timeFormat),
				asr: midAfternoon.utcOffset(utcOffset).add(1, 'm').format(timeFormat),
				maghrib: sunset.utcOffset(utcOffset).add(1, 'm').format(timeFormat),
				isha: moment(times.isha).add(1, 'm').utcOffset(utcOffset).format(timeFormat)
			};

		// var response =
		// 	{
		// 		startOfLunarMonth:(moonAtPreviousMaghrib.phase>moonAtMaghrib.phase),
		// 		// (moonAtPreviousIsha2DaysAgo.phase > moonAtIsha.phase),
		// 		maghribIsAdjusted: validSunriseTimes.sunset.valueOf() != originaltimes.sunset.valueOf(),
		// 		fajrIsAdjusted: validFajrTimes.fajr.valueOf() != originaltimes.fajr.valueOf(),
		// 		unadjustedLatitude: unadjustedLatitude,
		// 		fajrAdjustedLatitude: fajrAdjustedLatitude,
		// 		sunriseAdjustedLatitude: sunriseAdjustedLatitude,
		// 		fajr: moment(validFajrTimes.fajr).utcOffset(utcOffset).add(1,'m').format(timeFormat),
		// 		sunrise: moment(validSunriseTimes.sunrise).utcOffset(utcOffset).add(1, 'm').format(timeFormat),
		// 		zuhr: noon.utcOffset(utcOffset).add(1, 'm').format(timeFormat),
		// 		asr: midAfternoon.utcOffset(utcOffset).add(1, 'm').format(timeFormat),
		// 		maghrib: sunset.utcOffset(utcOffset).add(1, 'm').format(timeFormat),
		// 		isha: moment(validFajrTimes.isha).add(1, 'm').utcOffset(utcOffset).format(timeFormat)
		// 	};

		return response;
	}
	
	getPrayerTimesForTimeZone(date:Date,timeZone:timeZoneInfo,longitude:number,latitude:number):prayerTimesForDay{
		var self = this;
	
		var dateMoment = moment().startOf('d').add(12, 'h');
		if (date != null && moment(date).isValid()) {
			dateMoment = moment(date).startOf('d').add(12, 'h');
		}
		var timestamp=dateMoment.format("x");
		var utcOffset=0-moment.tz.zone(timeZone.timeZoneId).utcOffset(Number(timestamp))/ 60.0;
		var timeZoneAbbreviation=moment.tz.zone(timeZone.timeZoneId).abbr(Number(timestamp));
		//utcOffset = (timeZone.dstOffset + timeZone.rawOffset) / 3600.0;

		SunCalc.addTime(-18, 'fajr', 'isha');
		var times = self.getAdjustedTimes(latitude, longitude, dateMoment.toDate(), utcOffset);
		//var hijriDate:hijriDate =null;
		// if(yesterdayHijri==null){
		// 	hijriDate=this.getHijriDate(latitude, longitude, dateMoment.toDate(), utcOffset);
		// }
		// else{
		// 	if(!yesterDayWasNewMoon)
		// 	{
		// 		hijriDate={
		// 			day:yesterdayHijri.day+1,
		// 			month:yesterdayHijri.month,
		// 			year:yesterdayHijri.year
		// 		}
		// 	}else{
		// 		hijriDate={
		// 				day:1,
		// 				month: this.hijriMonths[yesterdayHijri.month.number%12],
		// 				year:yesterdayHijri.year+Math.floor((yesterdayHijri.month.number/12))
		// 			}
		// 	}
		// }
		var result= new prayerTimesForDay();
			//result.moonVisibility=times.moonVisibility;
			//result.startOfLunarMonth= times.startOfLunarMonth;
			//result.date=date;
			//result.isFriday=moment(dateMoment).day()==5;
			result.simpleDate=moment(dateMoment).format("DD/MM/YYYY");
			//result.formatedDate=moment(dateMoment).format("Do MMM YYYY");
			result.weekDay=moment(dateMoment).format("ddd");
			result.timeZoneName= timeZone.timeZoneName;
			result.timeZoneId= timeZone.timeZoneId;
			result.timeZoneAbbreviation=timeZoneAbbreviation;
			//result.timeZoneChange=false;
			//result.maghribIsAdjusted=times.maghribIsAdjusted;
			//result.maghribIsAdjustedLater=times.maghribIsAdjustedLater;
			//result.fajrIsAdjusted= times.fajrIsAdjusted;
			//result.fajrIsAdjustedEarlier= times.fajrIsAdjustedEarlier;
			//result.times=times;
			result.fajr=times.fajr;
			result.sunrise=times.sunrise;
			result.zuhr=times.zuhr;
			result.asr=times.asr;
			result.maghrib=times.maghrib;
			result.isha=times.isha;
		return result;

	}
}
