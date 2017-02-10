import { Injectable } from '@angular/core';
import {Http, Response} from '@angular/http';
import {Observable} from 'rxjs/Observable';
declare var shariahStandardsApiUrlBase:string;
import {addSectionModel} from './shurah/shurah.component'


export interface membershipRuleSection{
  id:number,
  uniqueName:string,
  sectionNumber:string,
  subSections:membershipRuleSection[]
}

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
	createRuleSection(model:addSectionModel,organisationId:number,parentSectionId?:number){
		return this.http.post(shariahStandardsApiUrlBase+"CreateRuleSection",{
			organisationId:organisationId,
			parentSectionId:parentSectionId,
			title:model.name,
			uniqueUrlSlug:model.urlSlug()
		})
	}
	dragDropRuleSection(draggedSection:membershipRuleSection,droppedSection:membershipRuleSection){
		return this.http.post(shariahStandardsApiUrlBase+"DragDropRuleSection",{
			draggedMembershipRuleSectionId:draggedSection.id,
			droppedOnMembershipRuleSectionId:droppedSection.id
		})
	}
	deleteRuleSection(sectionToDelete:membershipRuleSection){
		return this.http.post(shariahStandardsApiUrlBase+"DeleteRuleSection",{
			membershipRuleSectionId:sectionToDelete.id
		})
	}

}
