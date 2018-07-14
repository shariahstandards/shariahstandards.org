export class RejectMembershipApplicationModel{
	constructor(public applicationId:number){
	}
	reason:string;
	public errors:string[]
}