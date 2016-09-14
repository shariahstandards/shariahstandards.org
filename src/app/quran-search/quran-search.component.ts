import { Component, OnInit ,OnDestroy} from '@angular/core';
import { QuranService } from '../quran.service';
import { Router, ActivatedRoute } from '@angular/router';
import {NgClass} from '@angular/common';
export interface verseResult{
	text:string,
	surahNumber:number,
	verseNumber:number,
	englishTranslation:any[]
}
export interface quranSearchResults{
	hasSearched:boolean,
	hasResults:boolean,
	singleVerse:verseResult,
	results:string[]
}
interface searchType{
	code:string,
	text:string
}
interface surahSelection{
	number:number,
	arabicName:string,
	englishName:string
}
@Component({
  selector: 'app-quran-search',
  templateUrl: 'quran-search.component.html',
  styleUrls: ['quran-search.component.css'],
  providers:[QuranService]
//  directives:[NgClass]
})
export class QuranSearchComponent implements OnInit {

	searchTypes:searchType[]=[
		{
			code:"ref",
			text:"Surah number, Verse number"
		},
		{
			code:"eng",
			text:"English Text"
		}
	]
	searchType:searchType;
	constructor(private quranService:QuranService,
		private route:ActivatedRoute,
		private router:Router) {

	}
	private getRouteParamsSubscribe:any;
	selectedSearchResult:verseResult
	selectedReference:string
	searchText:string="";
	ngOnInit() {
		var self=this;
		self.surahsForSelection=self.getSurahsForSelection();
		
		self.getRouteParamsSubscribe=self.route.params.subscribe(params=>{
  	  		
  	  		var surahNumber=params['surahNumber'];
  	  		var verseNumber=params['verseNumber'];
			self.searchType=self.searchTypes[0];

  	  		if(surahNumber && verseNumber){
  	  			self.setSurahAndVerse(Number(surahNumber),Number(verseNumber));
  	  		}
  	  		else{
				self.setSurahAndVerse(1,1);
  	  		}
  	  		var searchText=params['searchText'];
  	  		if(searchText){
				self.searchType=self.searchTypes[1];
  	  			self.searchText=searchText;
  	  			self.search();
  	  		}

	  	});
	}
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
	selectedSurah:surahSelection;
	surahsForSelection:surahSelection[]=[];
	getSurahsForSelection():surahSelection[]{
	//from http://www.irfan-ul-quran.com/quran/english/contents/gifs/cols/sura/Read-Holy-Quran-with-Arabic-Urdu-Translation-Images.html
		var surahListRaw="1.	al-Fātihah (the Opening)	الْفَاتِحَة	1.\n\
2.	al-Baqarah (the Cow)	الْبَقَرَة	2.\n\
3.	Āl ‘Imrān (the Family of ‘Imrān)	آل عِمْرَان	3.\n\
4.	an-Nisā’ (Women)	النِّسَآء	4.\n\
5.	al-Mā’idah (the Table spread)	الْمَآئِدَة	5.\n\
6.	al-An‘ām (the Cattle)	الْأَنْعَام	6.\n\
7.	al-A‘rāf (the Heights)	الْأَعْرَاف	7.\n\
8.	al-Anfāl (Spoils of war)	الْأَنْفَال	8.\n\
9.	at-Tawbah (Repentance)	التَّوْبَة	9.\n\
10.	Yūnus (Jonah)	يُوْنـُس	10.\n\
11.	Hūd (Hud)	هُوْد	11.\n\
12.	Yūsuf (Joseph)	يُوْسُف	12.\n\
13.	ar-Ra‘d (Thunder)	الرَّعْد	13.\n\
14.	Ibrāhīm (Abraham)	إِبْرَاهِيْم	14.\n\
15.	al-Hijr (the Rocky tract)	الْحِجْر	15.\n\
16.	an-Nahl (the Bee)	النَّحْل	16.\n\
17.	al-Isrā’ (the Night journey)	الْإِسْرَاء - - بَنِيْ إِسْرَآءِيْل	17.\n\
18.	al-Kahf (the Cave)	الْكَهْف	18.\n\
19.	Maryam (Mary)	مَرْيَم	19.\n\
20.	Tāhā	طهٰ	20.\n\
21.	al-Ambiyā’ (the Prophets)	الْأَنْبِيَآء	21.\n\
22.	al-Hajj (the Pilgrimage)	الْحَجّ	22.\n\
23.	al-Mu’minūn (the Believers)	الْمُؤْمِنُوْن	23.\n\
24.	an-Nūr (the Light)	النُّوْر	24.\n\
25.	al-Furqān (the Criterion)	الْفُرْقَان	25.\n\
26.	ash-Shu‘arā’ (the Poets)	الشُّعَرَآء	26.\n\
27.	an-Naml (the Ants)	النَّمْل	27.\n\
28.	al-Qasas (the Narratives)	الْقَصَص	28.\n\
29.	al-‘Ankabūt (the Spider)	الْعَنْکَبُوْت	29.\n\
30.	ar-Rūm (the Roman Empire)	الرُّوْم	30.\n\
31.	Luqmān	لُقْمَان	31.\n\
32.	as-Sajdah (Prostration)	السَّجْدَة	32.\n\
33.	al-Ahzāb (the Confederates)	الْأَحْزَاب	33.\n\
34.	Saba’ (Sheba)	سَبـَا	34.\n\
35.	Fātir (the Originator)	فَاطِر	35.\n\
36.	Yāsīn (Ya-Sin)	يٰس	36.\n\
37.	as-Sāffāt (Those ranged in ranks)	الصَّافَّات	37.\n\
38.	Sād	ص	38.\n\
39.	az-Zumar (the Crowds)	الزُّمَر	39.\n\
40.	Ghāfir (The Forgiving)	غَافِر - الْمُؤْمِن	40.\n\
41.	Fussilat (Clearly spelled out)	فُصِّلَت - حٰم السَّجْدَة	41.\n\
42.	ash-Shūrā (the Consultation)	الشُّوْرٰی	42.\n\
43.	az-Zukhruf (Ornaments of gold)	الزُّخْرُف	43.\n\
44.	ad-Dukhān (Smoke)	الدُّخَان	44.\n\
45.	al-Jāthiyah (the Kneeling)	الْجَاثِيَة	45.\n\
46.	al-Ahqāf (the Sand-dunes)	الْأَحْقَاف	46.\n\
47.	Muhammad	مُحَمَّد	47.\n\
48.	al-Fath (Victory)	الْفَتْح	48.\n\
49.	al-Hujurāt (The Apartments)	الْحُجُرَات	49.\n\
50.	Qāf	ق	50.\n\
51.	adh-Dhāriyāt (the Scattering winds)	الذَّارِيَات	51.\n\
52.	at-Tūr (Mount Sinai)	الطُّوْر	52.\n\
53.	an-Najm (the Star)	النَّجْم	53.\n\
54.	al-Qamar (the Moon)	الْقَمَر	54.\n\
55.	ar-Rahmān (the Most Merciful)	الرَّحْمٰن	55.\n\
56.	al-Wāqi‘ah (the Inevitable event)	الْوَاقِعَة	56.\n\
57.	al-Hadīd (Iron)	الْحَدِيْد	57.\n\
58.	al-Mujādalah (the Quarrel)	الْمُجَادَلَة	58.\n\
59.	al-Hashr (the Gathering)	الْحَشْر	59.\n\
60.	al-Mumtahinah (the Woman to be examined)	الْمُمْتَحِنَة	60.\n\
61.	as-Saff (Battle array)	الصَّفّ	61.\n\
62.	al-Jumu‘ah (Friday, the Congregation)	الْجُمُعَة	62.\n\
63.	al-Munāfiqūn (the Hypocrites)	الْمُنَافِقُوْن	63.\n\
64.	at-Taghābun (Mutual loss and gain)	التَّغَابُن	64.\n\
65.	at-Talāq (Divorce)	الطَّلاَق	65.\n\
66.	at-Tahrīm (Prohibition)	التَّحْرِيْم	66.\n\
67.	al-Mulk (Dominion)	الْمُلْک	67.\n\
68.	al-Qalam (the Pen)	الْقَلَم	68.\n\
69.	al-Hāqqah (the Concrete reality)	الْحَآقَّة	69.\n\
70.	al-Ma‘ārij (the Ways of ascent)	الْمَعَارِج	70.\n\
71.	Nūh (Noah)	نُوْح	71.\n\
72.	al-Jinn (the Jinn)	الْجِنّ	72.\n\
73.	al-Muzzammil (Folded in garments)	الْمُزَّمِّل	73.\n\
74.	al-Muddaththir (the Enwrapped)	الْمُدَّثِّر	74.\n\
75.	al-Qiyāmah (the Resurrection)	الْقِيَامَة	75.\n\
76.	al-Insān (Man)	الْإِنْسَان - الدَّهْر	76.\n\
77.	al-Mursalāt (the Emissaries)	الْمُرْسَلاَت	77.\n\
78.	an-Naba’ (the News)	النَّبَا	78.\n\
79.	an-Nāzi‘āt (Those who pull)	النَّازِعَات	79.\n\
80.	‘Abasa (He frowned)	عَبَسَ	80.\n\
81.	at-Takwīr (Shrouding in darkness)	التَّکْوِيْر	81.\n\
82.	al-Infitār (the Cleaving asunder)	الْإِنْفِطَار	82.\n\
83.	al-Mutaffifīn (Dealing in fraud)	الْمُطَفِّفِيْن	83.\n\
84.	al-Inshiqāq (the Splitting asunder)	الْإِنْشِقَاق	84.\n\
85.	al-Burūj (the Zodiac)	الْبُرُوْج	85.\n\
86.	at-Tāriq (That which seems at night)	الطَّارِق	86.\n\
87.	al-A‘lā (the Most High)	الْأَعْلیٰ	87.\n\
88.	al-Ghāshiyah (the Overwhelming event)	الْغَاشِيَة	88.\n\
89.	al-Fajr (the Dawn)	الْفَجْر	89.\n\
90.	al-Balad (the City)	الْبَلَد	90.\n\
91.	ash-Shams (the Sun)	الشَّمْس	91.\n\
92.	al-Layl (the Night)	اللَّيْل	92.\n\
93.	ad-Duhā (the Forenoon)	الضُّحٰی	93.\n\
94.	ash-Sharh (the Opening up)	الشَّرْح - - الْإِنْشِرَاح	94.\n\
95.	at-Tīn (the Fig)	التِّيْن	95.\n\
96.	al-‘Alaq (the Hanging mass)	الْعَلَق	96.\n\
97.	al-Qadr (Determination)	الْقَدْر	97.\n\
98.	al-Bayyinah (the Clear evidence)	الْبَـيِّـنَة	98.\n\
99.	az-Zalzalah (the Earthquake)	الزَّلْزَلَة - - الزِّلْزَال	99.\n\
100.	al-‘Ādiyāt (Those that run)	الْعَادِيَات	100.\n\
101.	al-Qāri‘ah (Rattling violent jerk and thunder)	الْقَارِعَة	101.\n\
102.	at-Takāthur (Piling up)	التَّکَاثُر	102.\n\
103.	al-‘Asr (the Time)	الْعَصْر	103.\n\
104.	al-Humazah (the Slanderer)	الْهُمَزَة	104.\n\
105.	al-Fīl (the Elephant)	الْفِيل	105.\n\
106.	al-Quraysh	قُرَيْش	106.\n\
107.	al-Mā‘ūn (Things of common use)	الْمَاعُوْن	107.\n\
108.	al-Kawthar (the Abundance)	الْکَوْثَر	108.\n\
109.	al-Kāfirūn (the Unbelievers)	الْکَافِرُوْن	109.\n\
110.	an-Nasr (Help)	النَّصْر	110.\n\
111.	al-masad (the Twisted strands)	الْمَسَد - - اللَّهَب	111.\n\
112.	al-Ikhlās (Purity)	الْإِخْلاَص	112.\n\
113.	al-Falaq (the Fission)	الْفَلَق	113.\n\
114.	al-Nās (Men)	النَّاس	114.";
		var results = surahListRaw.split("\n").map(line=>{
			var parts=line.split('\t');
			return {
				number:Number(parts[0].replace('.','')),
				arabicName:parts[2],
				englishName:parts[1]
			};
		});
		return results;
	}
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
	}
	selectedVerseResult:verseResult;
	verseChanged(selectedVerseNumber){
		this.selectedVerseNumber=selectedVerseNumber;
		if(this.selectedSurah==null){
			return;
		}
		var surahs=this.quranService.getSurahs();
		this.selectedVerseResult=
		{
			text:surahs[this.selectedSurah.number-1][this.selectedVerseNumber-1].join(" "),
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
		hasResults:false,
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
	gotoSearch(){
		this.router.navigate(["/quran/search/"+ this.searchText]);
	}
	searchTextChanged(){
		this.searchResults.hasResults=false;
		this.searchResults.hasSearched=false;
	}
	search(){
		this.selectedSearchResult=null;
		this.selectedReference=null;
		this.searchResults=this.getSearchResult(this.searchText);
			
  	}
}
