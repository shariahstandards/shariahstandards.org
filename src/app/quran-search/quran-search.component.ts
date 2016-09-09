import { Component, OnInit ,OnDestroy} from '@angular/core';
import { QuranService } from '../quran.service';
import { Router, ActivatedRoute } from '@angular/router';
import {NgClass} from '@angular/common';
export interface verseResult{
	text:string,
	surahNumber:number,
	verseNumber:number,
	englishTranslation:string
}
  export interface quranSearchResults{
  	hasSearched:boolean,
  	hasResults:boolean,
  	singleVerse:verseResult,
  	results:string[]
  }

@Component({
  selector: 'app-quran-search',
  templateUrl: 'quran-search.component.html',
  styleUrls: ['quran-search.component.css'],
  providers:[QuranService],
  directives:[NgClass]
})

export class QuranSearchComponent implements OnInit {

	constructor(private quranService:QuranService,private route:ActivatedRoute) { }
	private getRouteParamsSubscribe:any;
	selectedSearchResult:verseResult
	selectedReference:string
	searchText:string="";
	ngOnInit() {
		this.getRouteParamsSubscribe=this.route.params.subscribe(params=>{
  	  	this.searchText=params['searchText'];
  	  	this.search()
  	 });
		
	}
	ngOnDestroy() {
  		this.getRouteParamsSubscribe.unsubscribe();
  	}
	searchResults:quranSearchResults={
		hasResults:false,
		hasSearched:false,
		singleVerse:undefined,
		results:[]
	};
	nextVerse(){
		if(this.searchResults.singleVerse!=null){
			var surah=this.searchResults.singleVerse.surahNumber;
			var verse=this.searchResults.singleVerse.verseNumber;
			this.searchText=""+surah+":"+(verse+1);
			this.search();
			if(!this.searchResults.hasResults){
				this.searchText=""+(surah+1)+":1";
				this.search();
				if(!this.searchResults.hasResults){
					this.searchText="1:1";
					this.search();
				}
			}
		}
	}
	previousVerse(){
		if(this.searchResults.singleVerse!=null){
			var surah=this.searchResults.singleVerse.surahNumber;
			var verse=this.searchResults.singleVerse.verseNumber;
			console.log("back from "+ surah+":"+verse);

			if(verse>1){
				this.searchText=""+surah+":"+(verse-1);
				this.search();
			}
			else{
				var surahs=this.quranService.getSurahs();
				if(surah>1){
					this.searchText=""+(surah-1)+":"+surahs[surah-2].length;
					this.search();
				}else{
					this.searchText="114:"+surahs[113].length;
					this.search();
				}
			}
		}
	}
	showSelectedVerse(){
		this.selectedSearchResult = this.getSearchResult(this.selectedReference).singleVerse;
	}
	getSearchResult(searchText:string){
		var result = 
	  	{
	  			hasSearched:false,
	  			hasResults:false,
	  			singleVerse:undefined,
	  			results:[]
	  	};
		if(searchText!=null){
	  		result.hasSearched=true;
	  		var surahs=this.quranService.getSurahs();
	  		var colonIndex=searchText.indexOf(':')
		  	if(colonIndex>0){
		  		var surahNumber=Number(searchText.substring(0,colonIndex));
		  		var verseNumber=Number(searchText.substring(colonIndex+1));
		  		console.log(surahNumber+":"+verseNumber);
		  		if(	surahNumber!=null 
		  			&& verseNumber!=null
		  			&& (!isNaN(surahNumber))
		  			&& (!isNaN(verseNumber))
		  			&& surahNumber>0
		  			&& verseNumber>0
		  			&& surahs.length>=surahNumber
		  			&& surahs[surahNumber-1].length>=verseNumber
		  			){
						result.hasResults=true;
						result.singleVerse={
							text:surahs[surahNumber-1][verseNumber-1].join(" "),
			  				surahNumber:surahNumber,
			  				verseNumber:verseNumber,
			  				englishTranslation:this.quranService.translation(surahNumber.toString(),verseNumber.toString())
			  			};
		  		}
		  	}
			else if(searchText.length>2){
				var results = this.quranService.search(searchText);
				if(results.length==0){
					result.hasResults=false;
				}
				else{
					result.hasResults=true;
					result.results = results
				}
			}
		}
		return result;

	}
	search(){
		this.selectedSearchResult=null;
		this.selectedReference=null;
		this.searchResults=this.getSearchResult(this.searchText)
  	}
}
