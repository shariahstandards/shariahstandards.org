import {termDefinitionModel} from './term-definition.model'

export class updateTermDefinitionModel{
  constructor(term:termDefinitionModel){
    this.errors=[];
    this.termId=term.id;
    this.definition=term.rawDefinition;
    this.term=term.term
  }
  termId:number
  definition:string
  term:string
  errors:string[]
}

