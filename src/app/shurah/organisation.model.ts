import {memberModel} from './member.model'
export interface organisationModel{
  permissions:string[],
  id:number,
  name:string,
  pendingMembershipApplicationsCount:number,
  member:memberModel,
  leaderMember:memberModel
}