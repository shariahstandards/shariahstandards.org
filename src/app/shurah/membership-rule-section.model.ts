export interface membershipRuleSectionModel{
  title:string,
  id:number,
  uniqueName:string,
  sectionNumber:string,
  subSections:membershipRuleSectionModel[]
}