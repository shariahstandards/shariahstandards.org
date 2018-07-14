import {DefinitionText} from './definition-text.interface';

export interface TermDefinition{
	term:string,
	definition:DefinitionText,
  relatedTerms:string[]
}