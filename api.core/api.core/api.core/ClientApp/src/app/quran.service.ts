import { Injectable, Inject } from '@angular/core';
import { surahSelection ,quranVerse} from './quran-data.service'
import {HttpClient } from '@angular/common/http';
import {Observable} from 'rxjs/Observable';
import {environment} from './environments/environment';

@Injectable()
export class QuranService {

constructor(private http: HttpClient) {

}
	surahInformationSubscription:Observable<surahSelection[]>
  	getSurahInformation():Observable<surahSelection[]>{
  		if(this.surahInformationSubscription!=null){
  			return this.surahInformationSubscription;
  		}
  		return this.surahInformationSubscription=<Observable<surahSelection[]>>this.http.get(environment.shariahStandardsApiUrlBase+"Surahs")
  			.publishReplay(1).refCount();
  	}
	getVerse(surah:number,verse:number):Observable<quranVerse>{
		return <Observable<quranVerse>>this.http.get(environment.shariahStandardsApiUrlBase+"QuranVerse/"+surah+"/"+verse);
	}
	containsEnglish(text:string){
		var lowerCaseText=text.toLowerCase();
	  	var englishChars="abcdefghijklmnopqrstuvwxyz";
	  	var englishSearch=false;
	  	for(var i=0;i<lowerCaseText.length;i++){
	  		if(englishChars.indexOf(lowerCaseText[i])>=0){
	  			englishSearch=true;
	  			break;
	  		}
	  	}
	  	return englishSearch;
	}
	
	searchResults:Observable<any>[]=[]
	search(searchText:string,searchInEnglish:boolean):any{
		var self=this;
		if(self.searchResults[searchText]!=null){
			return self.searchResults[searchText];
		}
		return self.searchResults[searchText]=
			self.http.post(environment.shariahStandardsApiUrlBase+"SearchQuran",{searchText:searchText,searchInEnglish:searchInEnglish})
			.publishReplay(1).refCount();
	}

	// englishSearchresults:Observable<any>[]=[]
	// englishSearch(searchText:string):any{
	// 	var self=this;
	// 	if(self.englishSearchresults[searchText]!=null){
	// 		return self.englishSearchresults[searchText];
	// 	}
	// 	return self.englishSearchresults[searchText]=
	// 		self.http.post(shariahStandardsApiUrlBase+"SearchEnglishQuran",{searchText:searchText})
	// 		.publishReplay(1).refCount();
	// }


	// removePunctuation(text:string){
	// 	return text.replace(/[\W ]+/g," ");
	// }
 //  	englishSearch(text:string){
	//   	var results=[];
	//   	var lowerCaseText=text.toLowerCase();
	//   	var verses=this.quranDataService.quranRaw.quran['en.yusufali'];
	// 	var englishQuranVerseNumbers = Object.keys(verses);
	// 	var matchingVerseNumbers =englishQuranVerseNumbers.filter(verseNum=>{
	//   		var verse=verses[verseNum];
	//   		var cleanerLowercaseVerseText=this.removePunctuation(verse.verse).toLowerCase();
	//   		var cleanerLowerCaseText = this.removePunctuation(lowerCaseText);
	//   		return cleanerLowercaseVerseText.indexOf(cleanerLowerCaseText)>=0; 
	//   	});
	//   	matchingVerseNumbers.forEach(verseNumber=>{
	//   		var verse=verses[verseNumber];

	// 		results.push({surah:verse.surah,verse:verse.ayah,searchText:text});

	// 		// results.push(verse.surah+":"+verse.ayah);
	//   	});

	//   	return {
	//   		hasEnglishResults:results.length>0,
	//   		hasArabicResults:false,
	//   		results:results
	//   	};
	// }
}
