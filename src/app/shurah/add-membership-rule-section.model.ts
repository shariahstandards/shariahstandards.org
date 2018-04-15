import {membershipRuleSectionModel} from './membership-rule-section.model'

export class addMembershipRuleSectionModel{
  constructor(
  public name:string
    ){
    this.errors=[]
  }
  parentSection:membershipRuleSectionModel
  urlSlug(){
  var cleanedText = this.name.toLowerCase().replace(/[^a-zA-Z0-9]/g, '-');
      cleanedText = cleanedText.replace(/--/g, '-');
      return cleanedText;
  }
  public errors:string[]
}