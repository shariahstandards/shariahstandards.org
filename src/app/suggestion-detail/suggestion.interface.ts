export interface Suggestion{
	id:number
	title:string
	dateTimeText:string
	authorPictureUrl:string
	authorPublicName:string
	fullText:string
	percentFor:number
	percentAgainst:number
	percentAbstaining:number
	for:number
	against:number
	abstaining:number
	votingPercent:number
	userVoteId?:number
	userVoteIsSupporting?:boolean
	voteByLeader?:boolean
	userIsSuggestionAuthor:boolean
	commentCount:number
}