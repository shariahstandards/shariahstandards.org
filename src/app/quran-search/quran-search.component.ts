import { Component, OnInit ,OnDestroy, Input,ChangeDetectorRef} from '@angular/core';
import { QuranService } from '../quran.service';
import { quranVerse,surahSelection } from '../quran-data.service';
import { Router, ActivatedRoute } from '@angular/router';
import { ArabicKeyboardComponent } from '../arabic-keyboard/arabic-keyboard.component'
import {NgClass} from '@angular/common';
export interface verseResult{
	text:string[],
	surahNumber:number,
	verseNumber:number,
	englishTranslation:any[]
}
export interface quranSearchResults{
	hasSearched:boolean,
	hasEnglishResults:boolean,
	hasArabicResults:boolean,
	singleVerse:verseResult,
	results:any[]
}
interface searchType{
	code:string,
	text:string
}

@Component({
  selector: 'app-quran-search',
  templateUrl: './quran-search.component.html',
  styleUrls: ['./quran-search.component.css'],
  providers:[QuranService]
//  directives:[NgClass]
})
export class QuranSearchComponent implements OnInit {

	constructor(public quranService:QuranService,
		private route:ActivatedRoute,
		private router:Router,
		private changeDetectorRef:ChangeDetectorRef) {

	}
	private getRouteParamsSubscribe:any;
	// @Input()
	selectedSearchResult:verseResult
	// @Input()
	selectedReference:string
	// @Input()
	searchText:string="";
	// @Input()
	wordToHighlight:string;
	loading:boolean=false;
	ngOnInit() {
		var self=this;
		self.loading=true;
		
		self.getRouteParamsSubscribe=self.route.params.subscribe(params=>{
			self.quranService.getSurahInformation().subscribe(surahs=>{
	  	  		var surahNumber=params['surahNumber'];
	  	  		var verseNumber=params['verseNumber'];
				self.wordToHighlight=params["wordToHighlight"];
				var searchText=params['searchText'];
				var routeBitForWord="";
				if(self.wordToHighlight!=null){
					routeBitForWord="/"+self.wordToHighlight;
				}
			
				self.surahsForSelection = surahs;
				if(surahNumber!=null && verseNumber!=null){
					self.selectedSurah=self.surahsForSelection[Number(surahNumber)-1];
					self.selectedVerseNumber=Number(verseNumber);
				}else{
					self.selectedSurah=self.surahsForSelection[0];
					self.selectedVerseNumber=1;
				}
				var items=[];
				for(var v=0;v<self.selectedSurah.verseCount;v++){
					items.push(v+1)
				}
				self.verseSelectionItems=items;
				self.quranService.getVerse(self.selectedSurah.number,self.selectedVerseNumber).subscribe(verse=>{
					self.currentVerse=verse;
					if(searchText){
		  	  			self.searchText=searchText;
		  	  			self.search();
		  	  		}
					self.loading=false;
	  	  			self.changeDetectorRef.detectChanges(); 
				})				
			});
	  	});
	}
	
	generatedScript:string="";
	toggleKeyboard(arabicKeyboard){
		arabicKeyboard.toggleShow(this.searchText);
	}
	gotoEnglishSearchResult(result:any){
		this.router.navigate(['/quran/surah/' + result.surah + '/verse/' + result.verse + '/' + result.searchText]);
	}
	// @Input()
	selectedEnglishSearchResult:any
	arabicKeyboardValueChanged(value:string){
//		console.log("arabic keyboard value update :"+ value)
		this.searchText=value;
	}
	// @Input() 
	selectedVerseNumber:number;
	verseSelectionItems:number[]=[];
	// @Input()
	selectedSurah:surahSelection;
	surahsForSelection:surahSelection[]=[];
	gotoSurah(selectedSurah){
		this.router.navigate(["/quran/surah/"+ selectedSurah.number+'/verse/1/'+this.searchText]);
	}
	gotoVerse(selectedVerse){
		this.router.navigate(["/quran/surah/"+ this.selectedSurah.number+'/verse/'
			+selectedVerse+'/'+this.searchText]);		
	}
	
	currentVerse:quranVerse;
	
	// @Input() 
	selectedSearchRouteUrl:string=null
	searchRouteSelected(){
		this.onSearchResultSelected(this.selectedSearchRouteUrl);
	}
	//cSharpCode:string="";

	selectedVerseResult:verseResult;

	highlight(verseText:string){
		if(this.searchText==null || this.searchText==""){
			return [{text:verseText}];
		}
		//var re = new RegExp("(?text"+this.searchText+")","g");
		//var result =  verseText.replace(re, '');
		var parts=[];
		var lowerCaseVerseText=verseText.toLowerCase();
		var lowerCaseSearchText=this.searchText.toLowerCase();
		var nextMatchIndex=lowerCaseVerseText.indexOf(lowerCaseSearchText);
		var previousMatchIndex=0;

		while(nextMatchIndex>=0){
			parts.push({text:verseText.substring(previousMatchIndex,nextMatchIndex),highlight:false})
			parts.push({text:verseText.substring(nextMatchIndex,nextMatchIndex+this.searchText.length),highlight:true})			
			var previousMatchIndex = nextMatchIndex+this.searchText.length;
			nextMatchIndex=lowerCaseVerseText.indexOf(lowerCaseSearchText,previousMatchIndex);
		}
		parts.push({text:verseText.substring(previousMatchIndex),highlight:false});
		return parts;
	}
	ngOnDestroy() {
  		this.getRouteParamsSubscribe.unsubscribe();
  	}
	searchResults:quranSearchResults={
		hasEnglishResults:false,
		hasArabicResults:false,
		hasSearched:false,
		singleVerse:undefined,
		results:[]
	};
	onSearchResultSelected(referenceUrl:any){
		this.router.navigateByUrl(referenceUrl);
	}
	getSearchResult(searchText:string){
		var self=this;
		self.searchResults = 
	  	{
	  			hasSearched:false,
	  			hasEnglishResults:false,
	  			hasArabicResults:false,
	  			singleVerse:undefined,
	  			results:[]
	  	};
		if(searchText!=null){
	  		if(searchText.length>2){
	  			var searchInEnglish=self.quranService.containsEnglish(searchText);
  				var searchresponse = self.quranService.search(searchText,searchInEnglish).subscribe(res=>{
  					var hasResults=res.resultCategories.filter(cat=>{
  						return cat.results.length>0;
  					}).length>0;
	  				self.searchResults.hasEnglishResults=searchInEnglish && hasResults;		
	  				self.searchResults.hasArabicResults=!searchInEnglish && hasResults;
	  				self.searchResults.results=res.resultCategories;
	  				self.searchResults.hasSearched=true;
	  				self.wordToHighlight=null;
	  	  			self.changeDetectorRef.detectChanges(); 
	  				self.selectCurrentResult();
  				})

			}
		}
	}
	selectCurrentResult(){
		this.selectedSearchRouteUrl = '/quran/surah/'+ this.selectedSurah.number + '/verse/' + this.selectedVerseNumber + '/' 
		 + this.searchText
		 + (this.wordToHighlight!=null?("/"+this.wordToHighlight):"/0")


	}
	gotoSearch(){
		this.router.navigate(["/quran/search/"+ this.searchText]);
	}
	searchTextChanged(){
		this.searchResults.hasEnglishResults=false;
		this.searchResults.hasArabicResults=false;
		this.searchResults.hasSearched=false;
	}
	search(){
		this.selectedSearchResult=null;
		this.selectedReference=null;
		this.getSearchResult(this.searchText);
			
  	}
}
