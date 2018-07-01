import { Injectable } from '@angular/core';
import {Response} from '@angular/http';
import {HttpClient } from '@angular/common/http';
import {Observable} from 'rxjs/Observable';
import {environment} from './environments/environment';
import {membershipRuleSectionModel} from './shurah/membership-rule-section.model'
import {membershipRuleModel} from './shurah/membership-rule.model'
import {addMembershipRuleSectionModel} from './shurah/add-membership-rule-section.model'
import {addTermDefinitionModel} from './term-definitions/add-term-definition.model'
import {updateMembershipRuleSectionModel} from './shurah/update-membership-rule-section.model'
import {updateMembershipRuleModel} from './shurah/update-membership-rule.model'
import {addMembershipRuleModel} from './shurah/add-membership-rule.model'
import {addSuggestionModel} from './suggestion/add-suggestion-model'
import {applyToJoinOrganisationModel} from './shurah/apply-to-join-organisation.model'
import {RejectMembershipApplicationModel} from './membership-application/reject-membership-application.model'
import {updateTermDefinitionModel} from './term-definitions/update-term-definition.model'
import {StandardApiResponse} from './Models/standard-api-response'
import {organisationModel} from './shurah/organisation.model'

@Injectable()
export class ShurahService {

	constructor(
		// @Inject(forwardRef(() => HttpClient)) 
		private http: HttpClient) { 
		
	}
	unfollow(organisationId:number):Observable<StandardApiResponse>{
		return <Observable<StandardApiResponse>>this.http.get(environment.shariahStandardsApiUrlBase+"StopFollowingAMember/"+organisationId);
	}
	follow(memberId:number):Observable<StandardApiResponse>{
		return <Observable<StandardApiResponse>>this.http.get(environment.shariahStandardsApiUrlBase+"FollowMember/"+memberId);
	}
	memberDetails(memberId:number):Observable<StandardApiResponse>{
		return <Observable<StandardApiResponse>>this.http.get(environment.shariahStandardsApiUrlBase+"MemberDetails/"+memberId);
	}
	searchForMembers(organisationId:number,page:number):Observable<StandardApiResponse>{
		return <Observable<StandardApiResponse>>this.http.post(environment.shariahStandardsApiUrlBase+"SearchForMembers",{
			organisationId:organisationId,
			page:page
		});
	}

	rejectMembershipApplication(model:RejectMembershipApplicationModel):Observable<StandardApiResponse>{
		return <Observable<StandardApiResponse>>this.http.post(environment.shariahStandardsApiUrlBase+"RejectMembershipApplication",{
			id:model.applicationId,
			reason:model.reason
		});
	}
	acceptMembershipApplication(applicationId:number):Observable<StandardApiResponse>{
		return <Observable<StandardApiResponse>>this.http.post(environment.shariahStandardsApiUrlBase+"AcceptMembershipApplication",{
			id:applicationId
		});
	}
	getOrganisationSummary(organisationId:number):Observable<StandardApiResponse>{
		return <Observable<StandardApiResponse>>this.http.post(environment.shariahStandardsApiUrlBase+"GetOrganisationSummary",{
			organisationId:organisationId
		});
	}
	viewApplications(organisationId:number,page:number):Observable<any>{
		return <Observable<any>>this.http.post(environment.shariahStandardsApiUrlBase+"ViewApplications",{
			organisationId:organisationId,
			page:page
		});
	}

	applyToJoinOrganisation(model:applyToJoinOrganisationModel) :Observable<StandardApiResponse>{
		return <Observable<StandardApiResponse>>this.http.post(environment.shariahStandardsApiUrlBase+"ApplyToJoin",{
			organisationId:model.organisationId,
			emailAddress:model.emailAddress,
			phoneNumber:model.phoneNumber,
			publicName:model.publicName,
			publicProfileStatement:model.publicProfileStatement,
			agreesToTermsAndConditions:model.agreesToTermsAndConditions
		});
	}
	searchSuggestions(organisationId:number,page:number,mostRecentFirst:boolean,memberId?:number):Observable<StandardApiResponse>{
		return <Observable<StandardApiResponse>>this.http.post(environment.shariahStandardsApiUrlBase+"SearchSuggestions",{
			organisationId:organisationId,
			memberId:memberId,
			page:page,
			mostRecentFirst:mostRecentFirst
		});
	}
	suggestionDetails(suggestionId:number):Observable<any>{
		return <Observable<any>>this.http.post(environment.shariahStandardsApiUrlBase+"ViewSuggestion",{
			suggestionId:suggestionId
		});
	}
	deleteSuggestion(suggestionId:number):Observable<StandardApiResponse>{
		return <Observable<StandardApiResponse>>this.http.post(environment.shariahStandardsApiUrlBase+"DeleteSuggestion",{
			suggestionId:suggestionId
		});
	}
	addSuggestion(organisationId:number,addSuggestionModel:addSuggestionModel):Observable<StandardApiResponse>{
		return <Observable<StandardApiResponse>>this.http.post(environment.shariahStandardsApiUrlBase+"CreateSuggestion",{
			organisationId:organisationId,
			subject:addSuggestionModel.subject,
			suggestion:addSuggestionModel.suggestion
		});

	}
	voteOnSuggestion(suggestionId:number, inFavour?:boolean):Observable<StandardApiResponse>{
		return <Observable<StandardApiResponse>>this.http.post(environment.shariahStandardsApiUrlBase+"Vote",{
			suggestionId:suggestionId,
			votingInSupport:inFavour
		});
	}
	commentOnSuggestion( model:any,suggestionId:number):Observable<StandardApiResponse>{
		return <Observable<StandardApiResponse>>this.http.post(environment.shariahStandardsApiUrlBase+"CommentOnSuggestion",{
			suggestionId:suggestionId,
			comment:model.commentText
		});
	}
	removeVoteOnSuggestion(voteId:number):Observable<StandardApiResponse>{
		return <Observable<StandardApiResponse>>this.http.post(environment.shariahStandardsApiUrlBase+"RemoveVote",{
			voteId:voteId
		});
	}
	getPermissionsForOrganisation(organisationId:number):Observable<string[]>{
		return <Observable<string[]>>this.http.get(environment.shariahStandardsApiUrlBase+"GetPermissionsForOrganisation/"+organisationId);
	}
	getTermList(organisationId:number):Observable<any>{
		return <Observable<any>>this.http.get(environment.shariahStandardsApiUrlBase+"GetTermList/"+organisationId);
	}
	getTermDefinition(termId:number,organisationId:number):Observable<any>{
		return <Observable<any>>this.http.get(environment.shariahStandardsApiUrlBase+"getTermDefinition/"+termId+'/'+organisationId);
	}
	createTermDefinition(model:addTermDefinitionModel,organisationId:number):Observable<any>{
		return <Observable<any>>this.http.post(environment.shariahStandardsApiUrlBase+"CreateTermDefinition",{
			organisationId:organisationId,
			term:model.term,
			definition:model.definition
		})
	}
	updateTermDefinition(model:updateTermDefinitionModel):Observable<any>{
		return <Observable<any>>this.http.post(environment.shariahStandardsApiUrlBase+"UpdateTermDefinition",{
			termId:model.termId,
			term:model.term,
			definition:model.definition
		})
	}
	deleteTermDefinition(termId:number):Observable<StandardApiResponse>{
		return <Observable<StandardApiResponse>>this.http.post(environment.shariahStandardsApiUrlBase+"DeleteTermDefinition",{
			termId:termId,
		})
	}
  	getOrganisation(organisationId:number):Observable<organisationModel>{
  		return <Observable<organisationModel>>this.http.get(environment.shariahStandardsApiUrlBase+"GetOrganisation/"+organisationId);
  	}
	
	leave(organisationId:number):Observable<StandardApiResponse>{
		return <Observable<StandardApiResponse>>this.http.post(environment.shariahStandardsApiUrlBase+"Leave",{
			organisationId:organisationId
		});
	}
	createRuleSection(model:addMembershipRuleSectionModel,organisationId:number,parentSectionId?:number) :Observable<StandardApiResponse>{
		return <Observable<StandardApiResponse>>this.http.post(environment.shariahStandardsApiUrlBase+"CreateRuleSection",{
			organisationId:organisationId,
			parentSectionId:parentSectionId,
			title:model.name,
			uniqueUrlSlug:model.urlSlug()
		})
	}
	dragDropRuleSection(draggedSection:membershipRuleSectionModel,droppedSection:membershipRuleSectionModel):Observable<StandardApiResponse>{
		return <Observable<StandardApiResponse>>this.http.post(environment.shariahStandardsApiUrlBase+"DragDropRuleSection",{
			draggedMembershipRuleSectionId:draggedSection.id,
			droppedOnMembershipRuleSectionId:droppedSection.id
		})
	}
	dragDropRule(draggedRule:membershipRuleModel,droppedRule:membershipRuleModel):Observable<StandardApiResponse>{
		return <Observable<StandardApiResponse>>this.http.post(environment.shariahStandardsApiUrlBase+"DragAndDropRule",{
			draggedMembershipRuleId:draggedRule.id,
			droppedMembershipRuleId:droppedRule.id
		})
	}
	deleteRule(rule:membershipRuleModel):Observable<StandardApiResponse>{
		return <Observable<StandardApiResponse>>this.http.post(environment.shariahStandardsApiUrlBase+"DeleteRule",{
			membershipRuleId:rule.id,
		})
	}

	deleteRuleSection(sectionToDelete:membershipRuleSectionModel):Observable<StandardApiResponse>{
		return <Observable<StandardApiResponse>>this.http.post(environment.shariahStandardsApiUrlBase+"DeleteRuleSection",{
			membershipRuleSectionId:sectionToDelete.id
		})
	}
	updateMembershipRuleSection(model:updateMembershipRuleSectionModel):Observable<StandardApiResponse>{
		return <Observable<StandardApiResponse>>this.http.post(environment.shariahStandardsApiUrlBase+"UpdateRuleSection",{
			membershipRuleSectionId:model.membershipRuleSectionModel.id,
			title:model.membershipRuleSectionModel.title,
			uniqueUrlSlug:model.urlSlug()
		})
	}
	updateMembershipRule(model:updateMembershipRuleModel):Observable<StandardApiResponse>{
		return <Observable<StandardApiResponse>>this.http.post(environment.shariahStandardsApiUrlBase+"UpdateRule",{
			membershipRuleId:model.membershipRuleModel.id,
			rule:model.membershipRuleModel.ruleStatement
		})
	}
	createRule(model:addMembershipRuleModel):Observable<StandardApiResponse>{
		return <Observable<StandardApiResponse>>this.http.post(environment.shariahStandardsApiUrlBase+"CreateRule",{
			membershipRuleSectionId:model.section.id,
			rule:model.ruleStatement,
		})
	}
}
