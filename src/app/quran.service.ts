import { Injectable, Inject } from '@angular/core';
import { QuranDataService,surahSelection ,quranVerse} from './quran-data.service'
import {HttpClient } from '@angular/common/http';
import {Observable} from 'rxjs/Observable';
declare var shariahStandardsApiUrlBase:string;

@Injectable()
export class QuranService {

constructor(@Inject(QuranDataService) public quranDataService:QuranDataService,private http: HttpClient) {

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
	surahInformationSubscription:Observable<surahSelection[]>
  	getSurahInformation():Observable<surahSelection[]>{
  		if(this.surahInformationSubscription!=null){
  			return this.surahInformationSubscription;
  		}
  		return this.surahInformationSubscription=<Observable<surahSelection[]>>this.http.get(shariahStandardsApiUrlBase+"Surahs")
  			.publishReplay(1).refCount();
  	}
	getVerse(surah:number,verse:number):Observable<quranVerse>{
		return <Observable<quranVerse>>this.http.get(shariahStandardsApiUrlBase+"QuranVerse/"+surah+"/"+verse);
	}
	getSurahsForSelection():surahSelection[]{
		return this.quranDataService.getSurahsForSelection();
	}
	getArabicWordSearchResult(word,searchText:string){
		var result = {
			word:word,
			searchText:searchText,
			occurenceCount: this.arabicIndex[word].length,
			occurences:this.arabicIndex[word]
		};
		return result;
	}
	arabicSearchresults:Observable<any>[]=[]
	arabicSearch(searchText:string):any{
		var self=this;
		if(self.arabicSearchresults[searchText]!=null){
			return self.arabicSearchresults[searchText];
		}
		return self.arabicSearchresults[searchText]=
			self.http.post(shariahStandardsApiUrlBase+"SearchQuran",{searchText:searchText})
			.publishReplay(1).refCount();
	}
	getVerseCount(surahNumber:number):number{
	 	var verses=this.quranDataService.quranRaw.quran['en.yusufali'];
		var verseNumbers = Object.keys(verses);
		var versesInSurah =verseNumbers.filter(verseNum=>{
	  		var verse=verses[verseNum];
			return verse.surah==surahNumber;
	  	});
		return versesInSurah.length;
	}
	oldarabicSearch(searchText:string){
		var results=[];
		if(searchText=="") {
		return {
			hasArabicResults:false,
	  		hasEnglishResults:false,
	  		results:results
		}};
		var exactWordMatches = this.arabicIndex[searchText];
		if(exactWordMatches!=null){
			results.push({category:"exact word match",matches:[this.getArabicWordSearchResult(searchText,searchText)]});
		}
		
		var partialExactMatches = [];
		var partialExactMatchesCount = 0;
		for(var w=0;w<this.arabicWords.length;w++){
			if(this.arabicWords[w].toString().indexOf(searchText)>=0){
				partialExactMatches[partialExactMatchesCount]=this.arabicWords[w];
				partialExactMatchesCount++;
			}
		}				
		if(partialExactMatchesCount>0){
			partialExactMatches.sort();
			var matches=[]
			for(var i=0;i<partialExactMatchesCount;i++){
				matches.push(this.getArabicWordSearchResult(partialExactMatches[i],searchText));
			}
			results.push({category:"partial exact word match",matches:matches});
		}
		var consonantMatches = [];
		var consonantMatchesCount = 0;
		var consonantOnlySearchWord=this.stripArabicVowels(searchText);
		for(var w=0;w<this.arabicWords.length;w++){
			var testWord=this.stripArabicVowels(this.arabicWords[w]);
			if(testWord.indexOf(consonantOnlySearchWord)>=0){
				consonantMatches[consonantMatchesCount]=this.arabicWords[w];
				consonantMatchesCount++;
			}
		}				
		if(consonantMatchesCount>0){
			consonantMatches.sort();
			var matches=[]
			for(var i=0;i<consonantMatchesCount;i++){
				matches.push(this.getArabicWordSearchResult(consonantMatches[i],searchText));
			}
			results.push({category:"consonant matches", matches:matches});
		}
		return {
	  		hasArabicResults:results.length>0,
	  		hasEnglishResults:false,
	  		results:results
	  	};
	}
	stripArabicVowels(word)
	{
		var output=word;
		var chars=['\u064e','\u064f','\u0650','\u0652','و','ا','ى','ي'];
		for(var c=0;c<chars.length;c++){
			output=(output.split(chars[c])).join('');				
		}
		return output;
	}

	arabicIndex:{}
	arabicWords:any[]
	
	removePunctuation(text:string){
		return text.replace(/[\W ]+/g," ");
	}
  	englishSearch(text:string){
	  	var results=[];
	  	var lowerCaseText=text.toLowerCase();
	  	var verses=this.quranDataService.quranRaw.quran['en.yusufali'];
		var englishQuranVerseNumbers = Object.keys(verses);
		var matchingVerseNumbers =englishQuranVerseNumbers.filter(verseNum=>{
	  		var verse=verses[verseNum];
	  		var cleanerLowercaseVerseText=this.removePunctuation(verse.verse).toLowerCase();
	  		var cleanerLowerCaseText = this.removePunctuation(lowerCaseText);
	  		return cleanerLowercaseVerseText.indexOf(cleanerLowerCaseText)>=0; 
	  	});
	  	matchingVerseNumbers.forEach(verseNumber=>{
	  		var verse=verses[verseNumber];

			results.push({surah:verse.surah,verse:verse.ayah,searchText:text});

			// results.push(verse.surah+":"+verse.ayah);
	  	});

	  	return {
	  		hasEnglishResults:results.length>0,
	  		hasArabicResults:false,
	  		results:results
	  	};
	}
	
	  //englishQuran:string[][]
	translation(surah:string,ayah:string){
	  	var verses=this.quranDataService.quranRaw.quran['en.yusufali'];
		var englishQuranVerseNumbers = Object.keys(verses);

	  	var matchingVerse =englishQuranVerseNumbers.filter(verseNum=>{
	  		var verse=verses[verseNum];
	  		return verse.surah==surah && verse.ayah==ayah 
	  	})
	  	return verses[matchingVerse[0]].verse;
	  	// var translation=this.quranInEnglish.quran["en.yusufali"]
	  	// if(this.translatedSurahs[surah] && this.translatedSurahs[verse]){
	  	// 	return this.translatedSurahs[verse];
	  	// }
  	}
}
