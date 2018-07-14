export interface membershipRuleModel{
  number:string,
  id:number,
  uniqueName:string,
  sectionNumber:string,
  ruleFragments:ruleFragment[],
  ruleStatement:string
}

export interface ruleFragment{
	text:string,
	isPlainText:boolean,
	isTerm:boolean,
	quranReference:quranReference,
	termId:number
}

export interface quranReference{
	surah:number,
	verse:number
}