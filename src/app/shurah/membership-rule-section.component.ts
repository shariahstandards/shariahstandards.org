import { Component, OnInit,ChangeDetectorRef ,Input,Output,EventEmitter} from '@angular/core';
import {membershipRuleSectionModel} from './membership-rule-section.model'
import {membershipRuleModel} from './membership-rule.model'

@Component({
  selector:'membership-rule-section',
  templateUrl:'./membership-rule-section.component.html',
  styleUrls: ['./membership-rule-section.component.css'],
})
export class MembershipRuleSectionComponent{
  
  constructor(private changeDetectorRef:ChangeDetectorRef){
      

 
  }
  @Input('section') 
  section:membershipRuleSectionModel
  @Input('sub-sections') 
  subSections:membershipRuleSectionModel[]
  @Input('routed-section') 
  routedSection:string
  @Input('expanded-sections') 
  expandedSections:string[]
  isSectionExpanded(){
    return this.expandedSections.some(sectionName=>{
      return this.section.uniqueName==sectionName;
    })
  }

  @Output() routingExpanded:EventEmitter<string>=new EventEmitter<string>();
  expandParents(val:string){
    console.log(this.section.uniqueName +" expanding parents ");
    if(!this.isSectionExpanded()){
      this.expand(true);
    }
    console.log(this.section.uniqueName +" expanded");
    this.routingExpanded.emit(val);
    console.log(this.section.uniqueName +" emitted event");
  }

  ngOnInit() {
    if(this.routedSection==this.section.uniqueName){
      this.expandParents("x");
    }else{
    }
   }
  expand(doNotSetActiveRoute?:boolean){
    console.log("expanding "+this.section.uniqueName);
    this.expandedSections.push(this.section.uniqueName)
    if(!doNotSetActiveRoute){
      this.setActiveSection(this.section);
    }
 //   this.expandParents("x");
  }
  collapse(){
    var sectionToRemove=this.expandedSections.filter(sectionName=>{
      return sectionName==this.section.uniqueName;
    })[0];
    this.expandedSections.splice(this.expandedSections.indexOf(sectionToRemove),1);
    this.setActiveSection(this.section);

  }
  @Input('allow-edit') allowEdit:boolean
  @Input('enable-paste') enablePaste:boolean
  @Input('enable-paste-rule') enablePasteRule:boolean
  @Output() addSection:EventEmitter<membershipRuleSectionModel>=new EventEmitter<membershipRuleSectionModel>();
  onClickAdd(section:membershipRuleSectionModel){
    this.addSection.emit(section);
  }
  @Output() cutSection:EventEmitter<membershipRuleSectionModel>=new EventEmitter<membershipRuleSectionModel>();
  cut(section:membershipRuleSectionModel){
    this.cutSection.emit(section);
  }
  
  @Output() pasteInto:EventEmitter<membershipRuleSectionModel>=new EventEmitter<membershipRuleSectionModel>();
  paste(section:membershipRuleSectionModel){
    this.pasteInto.emit(section);
  }
  deleteEnabled(){
    return this.subSections==null || this.subSections.length==0;
  }
 @Output() deleteSection:EventEmitter<membershipRuleSectionModel>=new EventEmitter<membershipRuleSectionModel>();
  delete(section:membershipRuleSectionModel){
    this.deleteSection.emit(section);
  }
  @Output() updateSection:EventEmitter<membershipRuleSectionModel>=new EventEmitter<membershipRuleSectionModel>();
  update(section:membershipRuleSectionModel){
    this.updateSection.emit(section);
  }
  @Output() onDeleteRule:EventEmitter<membershipRuleModel>=new EventEmitter<membershipRuleModel>();
  deleteRule(rule:membershipRuleModel){
    this.onDeleteRule.emit(rule);
  }
  @Output() onSectionExpanded:EventEmitter<membershipRuleSectionModel>=new EventEmitter<membershipRuleSectionModel>();
  setActiveSection(section:membershipRuleSectionModel){
    this.onSectionExpanded.emit(section);
  }
  @Output() createRuleInSection:EventEmitter<membershipRuleSectionModel>=new EventEmitter<membershipRuleSectionModel>();
  createRule(section:membershipRuleSectionModel){
    this.createRuleInSection.emit(section);
  }
  @Output() cutRuleOut:EventEmitter<membershipRuleModel>=new EventEmitter<membershipRuleModel>();
  cutRule(rule:membershipRuleModel){
    this.cutRuleOut.emit(rule);
  }
  @Output() onUpdateRule:EventEmitter<membershipRuleModel>=new EventEmitter<membershipRuleModel>();
  updateRule(rule:membershipRuleModel){
    this.onUpdateRule.emit(rule);
  }
  @Output() pasteRuleIn:EventEmitter<membershipRuleModel>=new EventEmitter<membershipRuleModel>();
  pasteRule(rule:membershipRuleModel){
    this.pasteRuleIn.emit(rule);
  }
}