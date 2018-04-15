import {membershipRuleModel} from './membership-rule.model'

export class updateMembershipRuleModel{
  constructor(
  public membershipRuleModel:membershipRuleModel
    ){
    this.errors=[]
  }
  public errors:string[]
}