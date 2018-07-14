import {membershipRuleModel} from './membership-rule.model'
import {membershipRuleSectionModel} from './membership-rule-section.model'

export class addMembershipRuleModel{
  constructor(public section:membershipRuleSectionModel){
    this.errors=[]
  }
  ruleStatement:string
  public errors:string[]
}