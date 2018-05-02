import { Injectable } from '@angular/core';
export interface surahSelection{
	number:number,
	arabicName:string,
	englishName:string,
	verseCount:number
}
export interface quranVerse{
	arabicWords:string[],
	englishText:string
}


