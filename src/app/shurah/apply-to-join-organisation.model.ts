export class applyToJoinOrganisationModel
{
	constructor(public organisationName:string, public organisationId:number){
    	this.errors=[]
  	}
  	public errors:string[]
    public emailAddress:string
    public phoneNumber:string
    public publicName:string
    public publicProfileStatement:string
    public rulesAgreedTo:boolean
}
