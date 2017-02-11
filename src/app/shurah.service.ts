import { Injectable } from '@angular/core';
import {Http, Response} from '@angular/http';
import {Observable} from 'rxjs/Observable';
declare var shariahStandardsApiUrlBase:string;
import {membershipRuleSectionModel} from './shurah/membership-rule-section.model'
import {membershipRuleModel} from './shurah/membership-rule.model'
import {addMembershipRuleSectionModel} from './shurah/add-membership-rule-section.model'
import {updateMembershipRuleSectionModel} from './shurah/update-membership-rule-section.model'
import {updateMembershipRuleModel} from './shurah/update-membership-rule.model'
import {addMembershipRuleModel} from './shurah/add-membership-rule.model'
@Injectable()
export class ShurahService {

	constructor(private http: Http) { 
		
	}
  	getRootOrganisation(){
  		return this.http.get(shariahStandardsApiUrlBase+"RootOrganisation");
  	}
	join(){
		return this.http.get(shariahStandardsApiUrlBase+"Join");
	}
	leave(){
		return this.http.get(shariahStandardsApiUrlBase+"Leave");
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
