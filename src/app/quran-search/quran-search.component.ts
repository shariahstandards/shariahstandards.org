import { Component, OnInit ,OnDestroy, Input,ChangeDetectorRef} from '@angular/core';
import { QuranService } from '../quran.service';
import { QuranDataService } from '../quran-data.service';
import { surahSelection } from '../quran-data.service';
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
  providers:[QuranService,QuranDataService]
//  directives:[NgClass]
})
export class QuranSearchComponent implements OnInit {

	constructor(private quranService:QuranService,
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
	ngOnInit() {
		var self=this;
		self.surahsForSelection=self.quranService.getSurahsForSelection();
		
		self.getRouteParamsSubscribe=self.route.params.subscribe(params=>{
  	  		
  	  		var surahNumber=params['surahNumber'];
  	  		var verseNumber=params['verseNumber'];
			self.wordToHighlight=params["wordToHighlight"];
			var searchText=params['searchText'];
  	  		if(searchText){
  	  			self.searchText=searchText;
  	  			self.search();
  	  		}
  	  		if(surahNumber && verseNumber){
  	  			self.setSurahAndVerse(Number(surahNumber),Number(verseNumber));
  	  		}
  	  		else{
				self.setSurahAndVerse(1,1);
  	  		}
  	  		
  	  		self.changeDetectorRef.detectChanges(); 
  	  		console.log("initialising-"+surahNumber+":"+verseNumber);
	  	});
	}
	toggleKeyboard(arabicKeyboard){
		arabicKeyboard.toggleShow(this.searchText);
	}
	gotoEnglishSearchResult(result:any){
		this.router.navigate(['/quran/surah/' + result.surah + '/verse/' + result.verse + '/' + result.searchText]);
	}
	// @Input()
	selectedEnglishSearchResult:any
	arabicKeyboardValueChanged(value:string){
		console.log("arabic keyboard value update :"+ value)
		this.searchText=value;
	}
	// @Input() 
	selectedVerseNumber:number;
	verseSelectionItems:number[]=[];
	getVerseSelectionItems(){
		console.log("verses:"+this.verseSelectionItems.length);
		return this.verseSelectionItems;
	}
	setVersesForSelection(){
		if(this.selectedSurah==null){
			return [];
		}
		this.selectedVerseNumber=null;
		var surahs=this.quranService.getSurahs();
		var verseCount=surahs[this.selectedSurah.number-1].length;
		console.log("verse count=:"+verseCount);

		var items=[];
		for(var v=0;v<verseCount;v++){
			items.push(v+1)
		}
		this.verseSelectionItems= items;
	}
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
	surahChanged(selectedSurah){

		this.selectedSurah=selectedSurah
		this.setVersesForSelection();
		this.verseChanged(1);
	}
	setSurahAndVerse(surahNumber:number,verseNumber:number){
		this.selectedSurah=this.surahsForSelection[surahNumber-1];
		this.setVersesForSelection();
		this.verseChanged(verseNumber);
		console.log("setting surah and verse-"+this.selectedSurah.number+":"+this.selectedVerseNumber);
	}
	// @Input() 
	selectedVerseResult:verseResult;
	verseChanged(selectedVerseNumber){
		this.selectedVerseNumber=selectedVerseNumber;
		if(this.selectedSurah==null){
			return;
		}
		var surahs=this.quranService.getSurahs();
		this.selectedVerseResult=
		{
			text:surahs[this.selectedSurah.number-1][this.selectedVerseNumber-1],
			surahNumber:this.selectedSurah.number,
			verseNumber:this.selectedVerseNumber,
			englishTranslation:this.highlight(this.quranService.translation(
				this.selectedSurah.number.toString(),this.selectedVerseNumber.toString()))
		};
	}

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
	// nextVerse(){
	// 	if(this.searchResults.singleVerse!=null){
	// 		var surah=this.searchResults.singleVerse.surahNumber;
	// 		var verse=this.searchResults.singleVerse.verseNumber;
	// 		this.searchText=""+surah+":"+(verse+1);
	// 		this.search();
	// 		if(!this.searchResults.hasResults){
	// 			this.searchText=""+(surah+1)+":1";
	// 			this.search();
	// 			if(!this.searchResults.hasResults){
	// 				this.searchText="1:1";
	// 				this.search();
	// 			}
	// 		}
	// 	}
	// }
	// previousVerse(){
	// 	if(this.searchResults.singleVerse!=null){
	// 		var surah=this.searchResults.singleVerse.surahNumber;
	// 		var verse=this.searchResults.singleVerse.verseNumber;
	// 		console.log("back from "+ surah+":"+verse);

	// 		if(verse>1){
	// 			this.searchText=""+surah+":"+(verse-1);
	// 			this.search();
	// 		}
	// 		else{
	// 			var surahs=this.quranService.getSurahs();
	// 			if(surah>1){
	// 				this.searchText=""+(surah-1)+":"+surahs[surah-2].length;
	// 				this.search();
	// 			}else{
	// 				this.searchText="114:"+surahs[113].length;
	// 				this.search();
	// 			}
	// 		}
	// 	}
	// }
	// showSelectedVerse(selectedReference){
	// 	this.selectedReference=selectedReference;
	// 	this.selectedSearchResult = this.getSearchResult(this.selectedReference).singleVerse;
	// }
	getSearchResult(searchText:string){
		var result = 
	  	{
	  			hasSearched:false,
	  			hasEnglishResults:false,
	  			hasArabicResults:false,
	  			singleVerse:undefined,
	  			results:[]
	  	};
		if(searchText!=null){
	  		result.hasSearched=true;
	  		if(searchText.length>2){
				var searchResponse = this.quranService.search(searchText);
				result.hasEnglishResults= searchResponse.hasEnglishResults;
				result.hasArabicResults= searchResponse.hasArabicResults;
				result.results=searchResponse.results;
			}
		}
		return result;

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
		this.searchResults=this.getSearchResult(this.searchText);
			
  	}
}
