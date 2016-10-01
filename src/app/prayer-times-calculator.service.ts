import { Injectable }     from '@angular/core';
import { Http, Response } from '@angular/http';
//import { Observable }     from 'rxjs/Observable';
import {Observable} from 'rxjs/Rx';
import 'rxjs/add/operator/map';
import 'moment';
import 'SunCalc';
declare var SunCalc: any;
declare var moment: any;

export interface timeZoneInfo {
	rawOffset: number,
	dstOffset: number,
	timeZoneName: string,
	timeZoneId:string
}
export interface prayerTime
{ name: string, time: string }
export interface hijriMonth{
	englishName:string,
	arabicName:string,
	phoneticName:string
	number:number
}
export interface hijriDate{
	year:number,
	month:hijriMonth,
	day:number
}
export interface prayerTimesForDay {
	startOfLunarMonth:boolean,
	moonVisibility:number,
	//moonPhaseAtIsha:number,
	//lunarCalandarDayAtIsha: number,
	date:Date,
	isFriday:boolean,
	simpleDate:string,
	formatedDate:string,
	weekDay:string,
	timeZoneName: string,
	timeZoneId: string,
	timeZoneAbbreviation:string;
	timeZoneChange:boolean;
	maghribIsAdjusted: boolean,
	fajrIsAdjusted: boolean,
	fajrIsAdjustedEarlier: boolean,
	maghribIsAdjustedLater: boolean,
	times: [prayerTime],
	hijriDate:hijriDate
}


@Injectable()
export class PrayerTimesCalculatorService {

	constructor(private http: Http) {

	}
	getTimeZone(date: Date, latitude: number, longitude: number): Observable<timeZoneInfo> {
		var dateMoment = moment(date).startOf('d').add(12, 'h');
		var timeStamp = dateMoment.unix();
		var url = 'https://maps.googleapis.com/maps/api/timezone/json?location=' + latitude + "," + longitude
			+ "&timestamp=" + timeStamp;
		return this.http.get(url).map(this.extractData).catch(this.handleError);

	}
	private extractData(res: Response) {
		return res.json();
	}
	private handleError(error: any) {
		// In a real world app, we might use a remote logging infrastructure
		// We'd also dig deeper into the error to get a better message
		let errMsg = (error.message) ? error.message :
			error.status ? `${error.status} - ${error.statusText}` : 'Server error';
		console.error(errMsg); // log to console instead
		return Observable.throw(errMsg);
	}
	fullDaylightLengthInHours(times: any): number {
		if (moment(times.sunrise).isValid()) {
			var daylengthInSeconds = moment(times.sunset).diff(moment(times.sunrise), 's');
			return daylengthInSeconds / 3600.0;
		}
		return 0;
	}

	dayLengthInHours(times: any): number {
		if (moment(times.fajr).isValid()) {
			var daylengthInSeconds = moment(times.isha).diff(moment(times.fajr), 's');
			return daylengthInSeconds / 3600.0;
		}
		return 0;
	}
	hasNormalDayLength(times: any): boolean {
		return (this.dayLengthInHours(times) > 4
			&& this.dayLengthInHours(times) < 20);
	}
	hasNormalFullDaylightLength(times: any): boolean {
		return (this.fullDaylightLengthInHours(times) > 6
			&& this.fullDaylightLengthInHours(times) < 18);
	}
	getHijriDate(latitude: number, longitude: number, date: Date, utcOffset: number): hijriDate {
		var referenceDate = moment.utc("2016-06-20T12:00:00");
		var currentDate = moment(date);
		var testDate=moment(date).toDate();
		var dayCountBack=0;
		var adjustedTimes = this.getAdjustedTimes(latitude,longitude,testDate,utcOffset);
		while(!adjustedTimes.startOfLunarMonth || dayCountBack==0){
			dayCountBack--;
			testDate=moment(testDate).add(-1,"days").toDate();
			adjustedTimes = this.getAdjustedTimes(latitude,longitude,testDate,utcOffset);
		}
		dayCountBack++;//start of lunar month indicates next day is start of lunar month
		var hijriDateDay=1-dayCountBack;
		var daysTo15thOfHijriDateDay = 15-hijriDateDay;
		var fifteenthOfHijriMonthDate = currentDate.add(daysTo15thOfHijriDateDay,"days");
		var daysToReferenceDate = fifteenthOfHijriMonthDate.diff(referenceDate,"days");
		var lunarMonthsSinceReferenceDate = Math.round(daysToReferenceDate /29.5306);
		var referenceDateLunarMonthIndex=8;
		var lunarMonthIndex = (referenceDateLunarMonthIndex+lunarMonthsSinceReferenceDate)%12;
		var hijriMonth = this.hijriMonths[lunarMonthIndex];
		var referenceDateHijriYear=1437;
		var lunarYearsSinceReferenceDay = Math.floor(
			(referenceDateLunarMonthIndex+lunarMonthsSinceReferenceDate)/12.0);
		var hijriYear = referenceDateHijriYear + lunarYearsSinceReferenceDay;
		return {
			day:hijriDateDay,
			month:hijriMonth,
			year:hijriYear
		}

	}
	hijriMonths:hijriMonth[]=[
	{
		arabicName:'مُحَرَّم',
		phoneticName:'Muḥarram',
		englishName:'Muharram',
		number:1
	},
	{
		arabicName:'صَفَر',
		phoneticName:'Ṣafar',
		englishName:'Safar',
		number:2
	},
	{
		arabicName:'رَبيع الأوّل',
		phoneticName:'Rabī‘ al-awwal',
		englishName:'Rabi al-awwal',
		number:3
	},
	{
		arabicName:'رَبيع الثاني',
		phoneticName:'Rabī‘ ath-thānī',
		englishName:'Rabi athani',
		number:4
	},
	{
		arabicName:'جُمادى الأولى',
		phoneticName:'Jumādá al-ūlá',
		englishName:'Jumada al-ula',
		number:5
	},
	{
		arabicName:'جُمادى الآخرة',
		phoneticName:'Jumādá al-ākhirah',
		englishName:'Ju. al-akhirah',
		number:6
	},
	{	
		arabicName:'رَجَب',
		phoneticName:'Rajab',
		englishName:'Rajab',
		number:7	
	},
	{
		arabicName:'شَعْبان',
		phoneticName:'Sha‘bān',
		englishName:'Shaban',
		number:8
	},
	{
		arabicName:'رَمَضان',
		phoneticName:'Ramaḍān',
		englishName:'Ramadan',
		number:9
	},
	{
		arabicName:'شَوّال',
		phoneticName:'Shawwāl',
		englishName:'Shawwal',
		number:10
	},
	{
		arabicName:'ذو القعدة',
		phoneticName:'Dhū al-Qa‘dah',
		englishName:'Dhu al-Qa\'dah',
		number:11
	},
	{
		arabicName:'ذو الحجة',
		phoneticName:'Dhū al-Ḥijjah',
		englishName:'Dhu al-Hijjah',
		number:12
	}
	]
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
		var moonVisibilityAtIsha = Math.min(1.0,moonAtIsha.phase*29.5306);
		var moonVisibilityAtIshaYesterday = Math.min(1.0,moonAtPreviousIsha1DaysAgo.phase*29.5306);
		var isStartOfLunarMonthYesterday=(moonAtPreviousIsha3DaysAgo.phase>moonAtPreviousIsha1DaysAgo.phase 
			&& moonVisibilityAtIshaYesterday==1.0);
		var startOfLunarMonthToday=(!isStartOfLunarMonthYesterday &&
			moonAtPreviousIsha2DaysAgo.phase>moonAtIsha.phase && moonVisibilityAtIsha==1.0);
				
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
		
	getDefaultTimeZone(date: Date, latitude: number, longitude: number){
		return this.getTimeZone(date, latitude, longitude);
	}
	getPrayerTimes(date: Date, latitude: number, longitude: number,
		timeZone:timeZoneInfo, yesterdayHijri?:hijriDate, yesterDayWasNewMoon?:boolean): prayerTimesForDay {

		var self = this;
	
				var dateMoment = moment().startOf('d').add(12, 'h');
				if (date != null && moment(date).isValid()) {
					dateMoment = moment(date).add(12, 'h');
				}
				var timestamp=dateMoment.format("x");
				var utcOffset=0-moment.tz.zone(timeZone.timeZoneId).offset(timestamp)/ 60.0;
				var timeZoneAbbreviation=moment.tz.zone(timeZone.timeZoneId).abbr(timestamp);
				//utcOffset = (timeZone.dstOffset + timeZone.rawOffset) / 3600.0;

				SunCalc.addTime(-18, 'fajr', 'isha');
				var times = self.getAdjustedTimes(latitude, longitude, dateMoment.toDate(), utcOffset);
				var hijriDate:hijriDate =null;
				if(yesterdayHijri==null){
					hijriDate=this.getHijriDate(latitude, longitude, dateMoment.toDate(), utcOffset);
				}
				else{
					if(!yesterDayWasNewMoon)
					{
						hijriDate={
							day:yesterdayHijri.day+1,
							month:yesterdayHijri.month,
							year:yesterdayHijri.year
						}
					}else{
						hijriDate={
								day:1,
								month: this.hijriMonths[yesterdayHijri.month.number%12],
								year:yesterdayHijri.year+Math.floor((yesterdayHijri.month.number/12))
							}
					}
				}
				return {
					moonVisibility:times.moonVisibility,
					startOfLunarMonth: times.startOfLunarMonth,
					date: date,
					isFriday:moment(dateMoment).day()==5,
					simpleDate:moment(dateMoment).format("DD/MM/YYYY"),
					formatedDate:moment(dateMoment).format("Do MMM YYYY"),
					weekDay:moment(dateMoment).format("ddd"),
					timeZoneName: timeZone.timeZoneName,
					timeZoneId: timeZone.timeZoneId,
					timeZoneAbbreviation:timeZoneAbbreviation,
					timeZoneChange:false,
					maghribIsAdjusted: times.maghribIsAdjusted,
					maghribIsAdjustedLater: times.maghribIsAdjustedLater,
					fajrIsAdjusted: times.fajrIsAdjusted,
					fajrIsAdjustedEarlier: times.fajrIsAdjustedEarlier,
					times:times,
					hijriDate:hijriDate
				};

	}
}