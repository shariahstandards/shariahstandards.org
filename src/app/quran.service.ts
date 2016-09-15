import { Injectable, Inject } from '@angular/core';
import { QuranDataService,surahSelection } from './quran-data.service'
@Injectable()
export class QuranService {

constructor(@Inject(QuranDataService) private quranDataService:QuranDataService) {

	this.buildArabicIndex();
}
  	search(text:string){
	  	var lowerCaseText=text.toLowerCase();
	  	var englishChars="abcdefghijklmnopqrstuvwxyz";
	  	var englishSearch=false;
	  	for(var i=0;i<lowerCaseText.length;i++){
	  		if(englishChars.indexOf(lowerCaseText[i])>=0){
	  			englishSearch=true;
	  			break;
	  		}
	  	}
	  	if(englishSearch){
	  		return this.englishSearch(text)
	  	}
	  	return this.arabicSearch(text);
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
	arabicSearch(searchText:string){
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

	arabicIndex:any[]
	arabicWords:any[]
	buildArabicIndex(){
		this.arabicIndex=[];
		this.arabicWords=[]; 
		var surahs=this.getSurahs();

		var wordCount=0;
		for(var s=1;s<=114;s++){
			for(var v=1;v<=surahs[s-1].length;v++){
				for(var w=1;w<=surahs[s-1][v-1].length;w++){
					var indexKey = surahs[s-1][v-1][w-1];
					if(this.arabicIndex[indexKey]==null){
						this.arabicIndex[indexKey]=[];							
						this.arabicWords[wordCount]=indexKey;
						wordCount++;
					}
					var ref={
						surah:s,
						verse:v,
						word:w
						};	
					this.arabicIndex[indexKey].push(ref);
				}
			}
		}
	}
  	englishSearch(text:string){
	  	var results=[];
	  	var lowerCaseText=text.toLowerCase();
	  	var verses=this.quranDataService.quranRaw.quran['en.yusufali'];
		var englishQuranVerseNumbers = Object.keys(verses);
		var matchingVerseNumbers =englishQuranVerseNumbers.filter(verseNum=>{
	  		var verse=verses[verseNum];
	  		return verse.verse.toLowerCase().indexOf(lowerCaseText)>=0; 
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
	getSurahs(){
		return this.quranDataService.surahs;
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
