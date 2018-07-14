import {membershipRuleModel} from './membership-rule.model'
export interface membershipRuleSectionModel{
  title:string,
  id:number,
  organisationId:number,
  uniqueName:string,
  sectionNumber:string,
  subSections:membershipRuleSectionModel[],
  rules:membershipRuleModel[]
}