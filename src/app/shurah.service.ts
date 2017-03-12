import { Injectable } from '@angular/core';
import {Http, Response} from '@angular/http';
import {Observable} from 'rxjs/Observable';
declare var shariahStandardsApiUrlBase:string;
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

@Injectable()
export class ShurahService {

	constructor(private http: Http) { 
		
	}
	searchForMembers(organisationId:number,page:number){
		return this.http.post(shariahStandardsApiUrlBase+"SearchForMembers",{
			organisationId:organisationId,
			page:page
		});
	}

	rejectMembershipApplication(model:RejectMembershipApplicationModel){
		return this.http.post(shariahStandardsApiUrlBase+"RejectMembershipApplication",{
			id:model.applicationId,
			reason:model.reason
		});
	}
	acceptMembershipApplication(applicationId:number){
		return this.http.post(shariahStandardsApiUrlBase+"AcceptMembershipApplication",{
			id:applicationId
		});
	}
	getOrganisationSummary(organisationId:number){
		return this.http.post(shariahStandardsApiUrlBase+"GetOrganisationSummary",{
			organisationId:organisationId
		});
	}
	viewApplications(organisationId:number,page:number){
		return this.http.post(shariahStandardsApiUrlBase+"ViewApplications",{
			organisationId:organisationId,
			page:page
		});
	}

	applyToJoinOrganisation(model:applyToJoinOrganisationModel){
		return this.http.post(shariahStandardsApiUrlBase+"ApplyToJoin",{
			organisationId:model.organisationId,
			emailAddress:model.emailAddress,
			phoneNumber:model.phoneNumber,
			publicName:model.publicName,
			publicProfileStatement:model.publicProfileStatement
		});
	}
	searchSuggestions(organisationId:number,page?:number){
		return this.http.post(shariahStandardsApiUrlBase+"SearchSuggestions",{
			organisationId:organisationId,
			page:page
		});
	}
	suggestionDetails(suggestionId:number){
		return this.http.post(shariahStandardsApiUrlBase+"ViewSuggestion",{
			suggestionId:suggestionId
		});
	}
	deleteSuggestion(suggestionId:number){
		return this.http.post(shariahStandardsApiUrlBase+"DeleteSuggestion",{
			suggestionId:suggestionId
		});
	}
	addSuggestion(organisationId:number,addSuggestionModel:addSuggestionModel){
		return this.http.post(shariahStandardsApiUrlBase+"CreateSuggestion",{
			organisationId:organisationId,
			subject:addSuggestionModel.subject,
			suggestion:addSuggestionModel.suggestion
		});

	}
	voteOnSuggestion(suggestionId:number, inFavour?:boolean){
		return this.http.post(shariahStandardsApiUrlBase+"Vote",{
			suggestionId:suggestionId,
			votingInSupport:inFavour
		});
	}
	removeVoteOnSuggestion(voteId:number){
		return this.http.post(shariahStandardsApiUrlBase+"RemoveVote",{
			voteId:voteId
		});
	}
	getPermissionsForOrganisation(organisationId:number){
		return this.http.get(shariahStandardsApiUrlBase+"GetPermissionsForOrganisation/"+organisationId);
	}
	getTermList(organisationId:number){
		return this.http.get(shariahStandardsApiUrlBase+"GetTermList/"+organisationId);
	}
	getTermDefinition(termId:number,organisationId:number){
		return this.http.get(shariahStandardsApiUrlBase+"getTermDefinition/"+termId+'/'+organisationId);
	}
	createTermDefinition(model:addTermDefinitionModel,organisationId:number){
		return this.http.post(shariahStandardsApiUrlBase+"CreateTermDefinition",{
			organisationId:organisationId,
			term:model.term,
			definition:model.definition
		})
	}
	updateTermDefinition(model:updateTermDefinitionModel){
		return this.http.post(shariahStandardsApiUrlBase+"UpdateTermDefinition",{
			termId:model.termId,
			term:model.term,
			definition:model.definition
		})
	}
  	getOrganisation(organisationId:number){
  		return this.http.get(shariahStandardsApiUrlBase+"GetOrganisation/"+organisationId);
  	}
	
	leave(organisationId:number){
		return this.http.post(shariahStandardsApiUrlBase+"Leave",{
			organisationId:organisationId
		});
	}
	createRuleSection(model:addMembershipRuleSectionModel,organisationId:number,parentSectionId?:number){
		return this.http.post(shariahStandardsApiUrlBase+"CreateRuleSection",{
			organisationId:organisationId,
			parentSectionId:parentSectionId,
			title:model.name,
			uniqueUrlSlug:model.urlSlug()
		})
	}
	dragDropRuleSection(draggedSection:membershipRuleSectionModel,droppedSection:membershipRuleSectionModel){
		return this.http.post(shariahStandardsApiUrlBase+"DragDropRuleSection",{
			draggedMembershipRuleSectionId:draggedSection.id,
			droppedOnMembershipRuleSectionId:droppedSection.id
		})
	}
	dragDropRule(draggedRule:membershipRuleModel,droppedRule:membershipRuleModel){
		return this.http.post(shariahStandardsApiUrlBase+"DragAndDropRule",{
			draggedMembershipRuleId:draggedRule.id,
			droppedMembershipRuleId:droppedRule.id
		})
	}
	deleteRule(rule:membershipRuleModel){
		return this.http.post(shariahStandardsApiUrlBase+"DeleteRule",{
			membershipRuleId:rule.id,
		})
	}

	deleteRuleSection(sectionToDelete:membershipRuleSectionModel){
		return this.http.post(shariahStandardsApiUrlBase+"DeleteRuleSection",{
			membershipRuleSectionId:sectionToDelete.id
		})
	}
	updateMembershipRuleSection(model:updateMembershipRuleSectionModel){
		return this.http.post(shariahStandardsApiUrlBase+"UpdateRuleSection",{
			membershipRuleSectionId:model.membershipRuleSectionModel.id,
			title:model.membershipRuleSectionModel.title,
			uniqueUrlSlug:model.urlSlug()
		})
	}
	updateMembershipRule(model:updateMembershipRuleModel){
		return this.http.post(shariahStandardsApiUrlBase+"UpdateRule",{
			membershipRuleId:model.membershipRuleModel.id,
			rule:model.membershipRuleModel.ruleStatement
		})
	}
	createRule(model:addMembershipRuleModel){
		return this.http.post(shariahStandardsApiUrlBase+"CreateRule",{
			membershipRuleSectionId:model.section.id,
			rule:model.ruleStatement,
		})
	}
}
