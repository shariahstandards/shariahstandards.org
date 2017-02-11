import {membershipRuleSectionModel} from './membership-rule-section.model'

export class updateMembershipRuleSectionModel{
  constructor(
  public membershipRuleSectionModel:membershipRuleSectionModel
    ){
    this.errors=[]
  }
  urlSlug(){
  var cleanedText = this.membershipRuleSectionModel.title.toLowerCase().replace(/[^a-zA-Z0-9]/g, '-');
      cleanedText = cleanedText.replace(/--/g, '-');
      return cleanedText;
  }
  public errors:string[]
}