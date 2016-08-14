import { Component, OnInit ,OnDestroy} from '@angular/core';
import { QuranService } from '../quran.service';
import { Router, ActivatedRoute } from '@angular/router';


export interface verseResult{
	text:string,
	surahNumber:number,
	verseNumber:number
}
  export interface quranSearchResults{
  	hasSearched:boolean,
  	hasResults:boolean,
  	singleVerse:verseResult
  }

@Component({
  selector: 'app-quran-search',
  templateUrl: 'quran-search.component.html',
  styleUrls: ['quran-search.component.css'],
  providers:[QuranService]
})

export class QuranSearchComponent implements OnInit {

	constructor(private quranService:QuranService,private route:ActivatedRoute) { }
	private getRouteParamsSubscribe:any;

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
		singleVerse:undefined
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
	search(){
	  	this.searchResults={
	  			hasSearched:false,
	  			hasResults:false,
	  			singleVerse:undefined
	  		};
	  	if(this.searchText!=null){
	  		this.searchResults.hasSearched=true;
	  		var surahs=this.quranService.getSurahs();
	  		var colonIndex=this.searchText.indexOf(':')
		  	if(colonIndex>0){
		  		var surahNumber=Number(this.searchText.substring(0,colonIndex));
		  		var verseNumber=Number(this.searchText.substring(colonIndex+1));
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
						this.searchResults.hasResults=true;
						this.searchResults.singleVerse={text:surahs[surahNumber-1][verseNumber-1].join(" "),
			  				surahNumber:surahNumber,
			  				verseNumber:verseNumber
			  			};
		  		}
		  	}
		}
  	}
}
