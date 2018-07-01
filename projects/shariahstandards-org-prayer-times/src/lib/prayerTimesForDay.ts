import {prayerTime} from './prayerTimes.interface'
import {hijriDate} from './hijriDate.interface'
export class prayerTimesForDay {
	//startOfLunarMonth:boolean;
	//moonVisibility:number;
	//moonPhaseAtIsha:number,
	//lunarCalandarDayAtIsha: number,
	//date:Date;
	//isFriday:boolean;
	simpleDate:string;
	//formatedDate:string;
	weekDay:string;
	timeZoneName: string;
	timeZoneId: string;
	timeZoneAbbreviation:string;
	//timeZoneChange:boolean;
	//maghribIsAdjusted: boolean;
	//fajrIsAdjusted: boolean;
	//fajrIsAdjustedEarlier: boolean;
	//maghribIsAdjustedLater: boolean;
	//times: [prayerTime];
	//hijriDate:hijriDate;
	fajr:string;
	sunrise:string;
	zuhr:string;
	asr:string;
	maghrib:string;
	isha:string;
}
