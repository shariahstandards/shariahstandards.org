import {NgModule,Component, AfterViewInit, ViewChild, ViewContainerRef,ChangeDetectorRef,ElementRef} from '@angular/core'
import {NgZone} from '@angular/core'
import {PrayerTimesCalculatorService, prayerTime, prayerTimesForDay, timeZoneInfo, hijriDate} from '../prayer-times-calculator.service';
import { Observable} from "rxjs/Observable"; 
import { Subscription} from "rxjs/Subscription"; 
import {BrowserModule} from '@angular/platform-browser';
import {RouterModule} from '@angular/router'
import {NgbModule,NgbDateStruct} from '@ng-bootstrap/ng-bootstrap'
import {Subscription} from 'rxjs/Subscription'
import 'moment';
import 'moment-timezone'
declare var $: any;
declare var google: any;
declare var moment: any;
declare var jsPDF:any;

interface FileReaderEventTarget extends EventTarget {
    result:string
}

interface FileReaderEvent extends Event {
    target: FileReaderEventTarget;
    getMessage():string;
}

@Component({
//  moduleId: module.id,
	templateUrl: './prayer-times.component.html',
  styleUrls: ['./prayer-times.component.css'],
  selector: 'prayer-times',
 // directives: [DATEPICKER_DIRECTIVES, MODAL_DIRECTVES, ROUTER_DIRECTIVES],
  providers: [PrayerTimesCalculatorService]
 // viewProviders:[BS_VIEW_PROVIDERS],
}) export class PrayerTimesComponent implements AfterViewInit {
	date: NgbDateStruct;
	month: {year: number, month: number};
	latitude: number;
	longitude: number;
	initialiseMap: any;
	qiblaLine: any;
	fajrAngle: number;
	ishaAngle: number;
	timeZone: string;
	startOfLunarMonth: boolean;
	fajrIsAdjusted: boolean;
	fajrIsAdjustedEarlier: boolean;
	maghribIsAdjustedLater: boolean;
	maghribIsAdjusted: boolean;
	locationFound: string;
	searchLocation: string;
	locationNotFound: boolean=false;
	showNewMonthLegend: boolean;
	map:any;
	maxZoomService:any;
	pdfTitle:string;
	constructor(private prayerTimesCalculatorService: PrayerTimesCalculatorService,
		private ngZone: NgZone,private changeDetectorRef:ChangeDetectorRef,
		private myElement: ElementRef) {
		if(!moment){return;}
		var now = moment();
		this.date={year: now.year(), month: now.month()+1, day: now.date()}
		this.latitude = 53.482863;
		this.longitude = -2.3459968;
		//this.utcOffset=moment().utcOffset()/60.0;
		this.fajrAngle = 6;
		this.ishaAngle = 6;
	}
	resetMap(){
		var self=this;

		var mapProp = {
            center: new google.maps.LatLng(this.latitude, this.longitude),
            zoom: 10,
            mapTypeId: google.maps.MapTypeId.ROADMAP
        };
        // console.log("initialising map...");
        this.map = new google.maps.Map(document.getElementById("googleMap"), mapProp);
		this.map.addListener('click', function(e){
			// console.log("clicked "+ JSON.stringify(e) );
			self.mapClicked(e);
		});
	}
	ngAfterViewInit() {
		var self=this;
		if(google){
			this.resetMap();
			this.resetLocation();
			this.maxZoomService=new google.maps.MaxZoomService();

		}
		this.changeDetectorRef.detectChanges();
		console.log("the date is"+this.getFullDate());
	}

	getWeekDay() {
		if(!moment){return "";}
		return moment(this.getDate()).format("dddd");
	}
	getFullDate() {
		if(!moment){return "";}
		return moment(this.getDate()).format("D MMMM YYYY");
	}
	removeCalendar(){
		this.numberOfDaysInCalendar = null;
		this.buildCalendar();
		this.changeDetectorRef.detectChanges();
		this.resetMap();
		this.placeQiblaOnMap();		
	}
	zoomToBuilding(){
		var self=this;
		var latlng = { lat: self.latitude, lng: self.longitude };
		this.maxZoomService.getMaxZoomAtLatLng(latlng,(response)=>{
			if(response.status !== 'OK'){
				alert("Sorry, google maps returned an error");
			}else{
				self.map.setZoom(response.zoom);
				self.map.setMapTypeId('hybrid');
			}
		})
	}
	resetLocation(){
		//console.log("resetting location on map"+this.map);
		var self = this;
		self.map.setMapTypeId('roadmap');

		if (navigator.geolocation) {
			navigator.geolocation.getCurrentPosition(function(p) {
				self.latitude = p.coords.latitude;
				self.longitude = p.coords.longitude;
				self.placeQiblaOnMap();
				self.getPrayerTimes();
				self.buildCalendar();
			},function(e){
				self.placeQiblaOnMap();
				self.getPrayerTimes();
				self.buildCalendar();

				console.log("could not get your current location:"+e.message)
			});
		}
		self.map.setZoom(10);
	}
	searchForLocation(){
		var self = this;
		//console.log("searching location on map"+this.map);

		self.locationNotFound = false;
		if (self.searchLocation != null && self.searchLocation != '') {
			var geocoder = new google.maps.Geocoder;
			var infowindow = new google.maps.InfoWindow;
			var latlng = { lat: self.latitude, lng: self.longitude };
			geocoder.geocode({ 'address': self.searchLocation }, function(results, status) {
				if (status === google.maps.GeocoderStatus.OK) {
					if (results[0]) {
						self.locationFound = results[0].formatted_address;
						self.latitude = results[0].geometry.location.lat();
						self.longitude = results[0].geometry.location.lng();
						self.placeQiblaOnMap();
						self.getPrayerTimes();
						self.buildCalendar();
						self.map.setZoom(10);
					}
				}
				if (status === google.maps.GeocoderStatus.ZERO_RESULTS){
					self.locationNotFound = true;
				}
				self.ngZone.run(function() { });
			});
		};
	}
	getLocationFound(){
		var self = this;
		var geocoder = new google.maps.Geocoder;
		var infowindow = new google.maps.InfoWindow;
		var latlng = { lat: self.latitude, lng: self.longitude };
		geocoder.geocode({ 'location': latlng }, function(results, status) {
			if (status === google.maps.GeocoderStatus.OK) {
				if(results[0]){
					self.locationFound = results[0].formatted_address;
					self.ngZone.run(function(){});
				} 
			}
		});
	}
	placeQiblaOnMap() {
		var self = this;
		// console.log("placing qibla on map"+this.map);
		if(!this.map){return;}
		var qiblaLineCoords = [
			{ lat: this.latitude, lng: this.longitude },
			{ lat: 21.422441833015604, lng: 39.82616901397705 }
		];
		if (this.qiblaLine != null) {
			this.qiblaLine.setMap(null);
		}
		self.qiblaLine = new google.maps.Polyline({
			path: qiblaLineCoords,
			geodesic: true,
			strokeColor: '#FF0000',
			strokeOpacity: 1.0,
			strokeWeight: 2
		});
		self.qiblaLine.setMap(this.map);
		this.map.setCenter({ lat: this.latitude, lng: this.longitude });

		self.getLocationFound();
	}
	prayerTimes: prayerTime[]=[]
	calendar: prayerTimesForDay[] = [];
	numberOfDaysInCalendar:number
	buildingCalendar: boolean = false;
	buildCalendar(){
		if(this.locationFound!=null){
			this.pdfTitle=this.locationFound.split(',').join("\n");
		}
		this.getPrayerTimeTableForNextNDays(this.numberOfDaysInCalendar);
	}
	getDate(){//NgbDateStruct uses sane month number need tp convert to javascript insane standard
		return new Date(this.date.year,this.date.month-1,this.date.day);
	}
	getPrayerTimeTableForTimeZone(days:number,initialTimeZone:timeZoneInfo){
		var self = this;

		self.calendar = [];
		var dateMoment = moment(self.getDate()).startOf('d');
		var yesterdayHijriDate=null;
		var yesterdayWasNewMoon=false;
		var previousTimeZoneAbbreviation=null;
		for (var i = 0; i < days; i++) {
			var date = moment(dateMoment).add(i, 'd');
			var prayerTimesForDate= self.getPrayerTimesForDate(date, initialTimeZone, yesterdayHijriDate,yesterdayWasNewMoon);
			if(previousTimeZoneAbbreviation && prayerTimesForDate.timeZoneAbbreviation!=previousTimeZoneAbbreviation )
			{
				prayerTimesForDate.timeZoneChange=true;
			}
			self.calendar.push(prayerTimesForDate);
			yesterdayHijriDate = prayerTimesForDate.hijriDate;
			yesterdayWasNewMoon = prayerTimesForDate.startOfLunarMonth;
			previousTimeZoneAbbreviation=prayerTimesForDate.timeZoneAbbreviation;
		}
		var firstDay = self.calendar[0];
		self.showNewMonthLegend = self.calendar.some(function(day){
			return day.startOfLunarMonth;
		})

	}
// 	(function(API){
//     API.textAlign = function(txt, options, x, y) {
//         options = options ||{};
//         // Use the options align property to specify desired text alignment
//         // Param x will be ignored if desired text alignment is 'center'.
//         // Usage of options can easily extend the function to apply different text
//         // styles and sizes

// 		// Get current font size
//         var fontSize = this.internal.getFontSize();

//         // Get page width
//         var pageWidth = this.internal.pageSize.width;

//         // Get the actual text's width
//         // You multiply the unit width of your string by your font size and divide
//         // by the internal scale factor. The division is necessary
//         // for the case where you use units other than 'pt' in the constructor
//         // of jsPDF.

//         var txtWidth = this.getStringUnitWidth(txt)*fontSize/this.internal.scaleFactor;

//         if( options.align === "center" ){

//             // Calculate text's x coordinate
//             x = ( pageWidth - txtWidth ) / 2;

//         } else if( options.align === "centerAtX" ){ // center on X value

// 	        x = x - (txtWidth/2);

//         } else if(options.align === "right") {

// 	        x = x - txtWidth;
//         }

//         // Draw text at x,y
//         this.text(txt,x,y);
//     };
// /*
//     API.textWidth = function(txt) {
// 	    var fontSize = this.internal.getFontSize();
//         return this.getStringUnitWidth(txt)*fontSize / this.internal.scaleFactor;
//     };
// */

//     API.getLineHeight = function(txt) {
//         return this.internal.getLineHeight();
//     };

// })(jsPDF.API);
	getTextWidth(pdf:any,text:string):number{
		var txtWidth = pdf.getStringUnitWidth(text)*pdf.internal.getFontSize()/pdf.internal.scaleFactor;
		return txtWidth
	}
	placeTextCenteredOnPdf(pdf:any,text:string,xPosition:number,yPosition:number){
		pdf.text(text, xPosition-(this.getTextWidth(pdf,text)/2),yPosition );
	}
	topPosMm:number=30;
	pageNumber:number=1;
	headerImageData;
	imageErrorText:string;
	prepareHeaderImage(){
		var self=this;
		self.imageErrorText="";
		return new Promise((resolve,reject)=>{
			if(self.pdfHeaderImage==null){
				resolve();
			}
			self.headerImageData={
				type: undefined,
				src:undefined,
				h:undefined,
				w:undefined
			}
			
			if(self.pdfHeaderImage.type === 'image/jpeg'){
				self.headerImageData.type="JPEG"
			}
			if(self.pdfHeaderImage.type === 'image/png'){
				self.headerImageData.type="PNG"
			}
			if(!self.headerImageData.type){
				self.headerImageData=null;
				self.pdfHeaderImage=null;
				self.imageErrorText="only jpeg and png images are supported";
				resolve();
			}
			var reader = new FileReader();
			reader.onloadend = () =>{
				self.headerImageData.src = reader.result;

				// we need this to get img dimensions in points
				var user_img = new Image();
				user_img.onload = function () {
					self.headerImageData.w = user_img.width;
					self.headerImageData.h = user_img.height;
					resolve();
					self.changeDetectorRef.detectChanges();
				};
				user_img.src = self.headerImageData.src;
			};
			reader.readAsDataURL(this.pdfHeaderImage)
		});
	}
	addHeaderImage(pdf:any){
		var self=this;
		if(self.headerImageData.src){
			var img_size = self.imgSizes(self.headerImageData.w, self.headerImageData.h, 190);
			pdf.addImage(self.headerImageData.src, self.headerImageData.type, img_size.centered_x, 
					self.topPosMm, img_size.w, img_size.h);
			self.topPosMm+= img_size.h;
			self.topPosMm+= 10;
		}
	}
	imgSizes(img_w:number, img_h:number, img_mm_w:number) {
		/*
			img_w and img_h represent the original image size, in pixel
			img_mm_w is the desidered rendered image size, in millimeters

		*/
		var content_width=190;
		var page_width=210;
		if( img_mm_w > content_width ) { // this should be never used...
			img_mm_w = content_width;
		}

		if(this.getPoints(img_mm_w) > img_w ) {
			throw 'The `img_mm_w` parameter is too big';
		}

		var img_mm_h = Math.round( (this.getMm(img_h) * img_mm_w) / this.getMm(img_w) );

		return {
			w            : img_mm_w,
			h            : img_mm_h,
			centered_x   : (page_width - img_mm_w) / 2
		};
	}
	getPoints = function (millimeters) {
		// mm to inches
		var inches = millimeters / 25.4;
		return inches * 72;
	}
	getMm = function (points) {
		// px to inches
		var inches = points / 72;
		return inches * 25.4;
	}
	setPdfHeaderImage(event){
		 var files = event.srcElement.files;
		 this.pdfHeaderImage=files[0];
		 this.prepareHeaderImage().then(()=>
		 {
			 this.changeDetectorRef.detectChanges();
		 });
	}
	clearHeaderImage(){
		this.pdfHeaderImage=null;
		this.headerImageData=null;
	}
	showFileInput(){
		if(this.pdfHeaderImage==null){
			return true;
		}
		if(this.headerImageData==null){
			return true;
		}
		if(this.headerImageData.src==null){
			return true;
		}
		return false;
	}
	addPdfHeader(pdf:any){
		var self=this;
			self.topPosMm=15;
			if(self.pdfHeaderImage!=null){
				self.addHeaderImage(pdf);
				pdf.addImage(self.logoSrc, 'JPEG', 50, 275, 10, 10);
				pdf.setFontSize(10);
				self.placeTextCenteredOnPdf(pdf,'ShariahStandards.org generated prayer timetable', 105, 282);
			}else{
				self.topPosMm+=13;
				pdf.addImage(self.logoSrc, 'JPEG', 20, 15, 20, 20);
				pdf.setFontSize(20);
				self.placeTextCenteredOnPdf(pdf,'ShariahStandards.org Prayer Timetable', 105, self.topPosMm);
				self.topPosMm+=10;
				pdf.setFontSize(15);
				self.pdfTitle.split('\n').forEach(line=>{	
					self.placeTextCenteredOnPdf(pdf,line, 105, self.topPosMm);	
					self.topPosMm+=7;
				})
				self.topPosMm+=10;
			}
			pdf.setFontSize(12);
			
			pdf.text("Date", 20,self.topPosMm);
			pdf.text("Hijri", 50,self.topPosMm);
			pdf.text("Fajr", 80,self.topPosMm);
			pdf.text("Sunrise", 100,self.topPosMm);
			pdf.text("Zuhr", 120,self.topPosMm);
			pdf.text("Asr", 140,self.topPosMm);
			pdf.text("Maghrib", 160,self.topPosMm);
			pdf.text("Isha", 180,self.topPosMm);
			self.topPosMm+=8;
			pdf.setFontSize(10);	
	}
	pdfHeaderImage:File

	logoSrc:string
	="data:image/jpeg;base64,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"

	buildPdfTimeTable(pdf:any){
		var self=this;

		if(!self.locationFound){return;}
		if(!self.calendar){return;}
		if(self.calendar.length==0){return;}
		self.pageNumber=1;
		self.addPdfHeader(pdf);
		var oddRow=true;
		self.calendar.forEach(day=>{

			if(self.topPosMm>275){
				pdf.addPage();
				 self.addPdfHeader(pdf);
			}
			if(day.timeZoneChange){
				self.placeTextCenteredOnPdf(pdf,"*** Start of "+day.timeZoneAbbreviation+" ***", 105, self.topPosMm);
				self.topPosMm+=6;
				pdf.setFontType("bold");
			}
			if(!day.timeZoneChange){
				pdf.setFontType("normal");
			}
			if(day.isFriday){
				pdf.setFillColor(220,255,220);
				pdf.roundedRect(19.5,self.topPosMm-4, 170, 6, 0, 0, "F");
			}else if(oddRow){
				pdf.setFillColor(240,240,240);
				pdf.roundedRect(19.5,self.topPosMm-4, 170, 6, 0, 0, "F");
			}
			pdf.text(day.weekDay+" "+day.simpleDate, 20,self.topPosMm);
			pdf.text(day.hijriDate.day+" "+day.hijriDate.month.englishName, 50,self.topPosMm);
			pdf.text(day.times["fajr"], 80,self.topPosMm);
			pdf.text(day.times["sunrise"], 100,self.topPosMm);
			pdf.text(day.times["zuhr"], 120,self.topPosMm);
			pdf.text(day.times["asr"], 140,self.topPosMm);
			pdf.text(day.times["maghrib"], 160,self.topPosMm);
			pdf.text(day.times["isha"], 180,self.topPosMm);
			self.topPosMm+=6;
			oddRow=!oddRow;
			
		})

	
        pdf.save('ShariahStandardsTimeTable'+self.latitude+"_"
        	+self.longitude+'_'+moment(self.getDate()).format("YYYYMMDD")+self.numberOfDaysInCalendar+'.pdf');	
	}	
	getPdfTimetable(){
		var self=this;
		var pdf = new jsPDF('p', 'mm', 'a4');
		this.prepareHeaderImage().then(()=>{
			this.buildPdfTimeTable(pdf)
		});
		
	}
	getPrayerTimeTableForNextNDays(days:number){
		var self = this;
		self.showNewMonthLegend = false;
		self.calendar = [];
		if(days==null){
			return;
		}
		
		return self.prayerTimesCalculatorService.getDefaultTimeZone(self.getDate(), self.latitude, self.longitude)
			.subscribe(initialTimeZone => {

				// var lastDate=moment(self.getDate()).add(days,"days").toDate();
				// self.prayerTimesCalculatorService.getDefaultTimeZone(lastDate,self.latitude,self.longitude)
				// .subscribe(finalTimeZone=>{
				// 	// if(initialTimeZone.dstOffset==finalTimeZone.dstOffset
					// && finalTimeZone.rawOffset== initialTimeZone.rawOffset)
					// {
						self.getPrayerTimeTableForTimeZone(days,initialTimeZone)
					// }else{

					// }
				//})
			});
	}
	hijriDate:hijriDate;
	getPrayerTimes() {
		var self = this;
		return self.prayerTimesCalculatorService.getDefaultTimeZone(self.getDate(), self.latitude, self.longitude)
			.subscribe(timeZone => {
				console.log("getting prayer times");
				var prayerTimesDay = self.getPrayerTimesForDate(self.getDate(), timeZone, null,false);
				self.prayerTimes = prayerTimesDay.times;
				self.startOfLunarMonth = prayerTimesDay.startOfLunarMonth;
				self.timeZone = prayerTimesDay.timeZoneAbbreviation;
				self.fajrIsAdjusted = prayerTimesDay.fajrIsAdjusted;
				self.maghribIsAdjusted = prayerTimesDay.maghribIsAdjusted;
				self.fajrIsAdjustedEarlier = prayerTimesDay.fajrIsAdjustedEarlier;
				self.maghribIsAdjustedLater = prayerTimesDay.maghribIsAdjustedLater;
				self.hijriDate=prayerTimesDay.hijriDate;
				console.log(self.hijriDate);
			});
	}
	getPrayerTimesForDate(date:Date,timeZone:timeZoneInfo,yesterdayHijri?:hijriDate,yesterdayWasNewMoon?:boolean) {
		var self = this;
		return self.prayerTimesCalculatorService.getPrayerTimes(date,
			self.latitude, self.longitude, timeZone, yesterdayHijri,yesterdayWasNewMoon);
	}
	dateChanged(){
		console.log("the date is"+this.getFullDate());
		this.getPrayerTimes();
		this.getPrayerTimeTableForNextNDays(this.numberOfDaysInCalendar);
	}
	mapClicked($event: any) {
		// console.log($event);
		this.locationNotFound = false;
		this.latitude = $event.latLng.lat();
		this.longitude = $event.latLng.lng();
		this.placeQiblaOnMap();
		this.getPrayerTimes();
		this.getPrayerTimeTableForNextNDays(this.numberOfDaysInCalendar);
    }
}
