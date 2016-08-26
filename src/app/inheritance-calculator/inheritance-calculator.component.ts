import { Component, OnInit } from '@angular/core';
export interface inheritanceSituation{
	hasMother:boolean,
	hasFather:boolean,
	numberOfSons:number,
	numberOfDaughters:number,
	numberOfBrothers:number,
	numberOfSisters:number,
	isMale:boolean,
	isMarried:boolean,
	shares:inheritanceShare[],
	allocatedShare:number
}
export interface inheritanceShare{
	relationshipToDeceased:string,
	share:number
}
@Component({
  selector: 'app-inheritance-calculator',
  templateUrl: 'inheritance-calculator.component.html',
  styleUrls: ['inheritance-calculator.component.css']
})
export class InheritanceCalculatorComponent implements OnInit {

	model:inheritanceSituation={
		hasMother:false,
		hasFather:false,
		numberOfSons:0,
		numberOfDaughters:0,
		numberOfBrothers:0,
		numberOfSisters:0,
		isMale:false,
		isMarried:false,
		shares:[],
		allocatedShare:0
	};
	getShares(){
		this.calculateShares(this.model);
		return this.model;
	}
	exampleSituations:inheritanceSituation[]=[]
    constructor() { }
    calculateShares(situation:inheritanceSituation){
    	situation.shares=[];
    	//fixed shares start
		if(situation.numberOfSons==0 && situation.numberOfDaughters==1){
			situation.shares.push({
				relationshipToDeceased:"daughter",
				share:0.5
			});
		}
		if(situation.numberOfSons==0 && situation.numberOfDaughters>=2){
			for(var d=0;d<situation.numberOfDaughters;d++)
			situation.shares.push({
				relationshipToDeceased:"daughter"+(d+1).toString(),
				share:(2.0/3.0)/situation.numberOfDaughters
			});
		}
		if(situation.numberOfSons +situation.numberOfDaughters>0){
			if(situation.hasFather){
				situation.shares.push(
				{
					relationshipToDeceased:"father",
					share:(1.0/6.0)
				});
			}
			if(situation.hasMother){
				situation.shares.push(
				{
					relationshipToDeceased:"mother",
					share:(1.0/6.0)
				});
			}
		}
		if(situation.numberOfSons +situation.numberOfDaughters	==0){
			if(situation.hasFather){
				situation.shares.push(
				{
					relationshipToDeceased:"father",
					share:(1.0/6.0)
				});
			}
			if(situation.hasMother){
				if(situation.numberOfBrothers==0){
					situation.shares.push(
					{
						relationshipToDeceased:"mother",
						share:(1.0/3.0)
					});
				}else{
					situation.shares.push(
					{
						relationshipToDeceased:"mother",
						share:(1.0/6.0)
					});
				}
			}
		}
		//fard shares end
		this.calculateAllocatedShare(situation);
		
		var unallocatedShare = 1.0-situation.allocatedShare;
		if(unallocatedShare<0.00001){
			return;
		}
		if(situation.isMarried){
			if(situation.numberOfSons+situation.numberOfDaughters==0){
				if(!situation.isMale){
					situation.shares.push({
						relationshipToDeceased:"husband",
						share: Math.min(0.5,unallocatedShare)
					});
				}
				if(situation.isMale){
					situation.shares.push({
						relationshipToDeceased:"wives",
						share:Math.min(0.25,unallocatedShare)
					});
				}
			}else{
				if(!situation.isMale){
					situation.shares.push({
						relationshipToDeceased:"husband",
						share:Math.min(0.25,unallocatedShare)
					});
				}
				if(situation.isMale){
					situation.shares.push({
						relationshipToDeceased:"wives",
						share:Math.min(0.125,unallocatedShare)
					});
				}
			}
		}
		this.calculateAllocatedShare(situation);
		
		var unallocatedShare = 1.0-situation.allocatedShare;
		if(unallocatedShare<0.00001){
			return;
		}

		//kalalah
		if(
			// !situation.isMarried 
			// && 
			(!situation.hasFather
			&& 
			!situation.hasMother)
			||(situation.numberOfSons==0 && situation.numberOfDaughters==0)
			){
			var siblingCount=situation.numberOfSisters+situation.numberOfBrothers;
			if(situation.numberOfSons
				+situation.numberOfDaughters
				>0){
				var adjustedShare=Math.min(1.0/3.0,unallocatedShare);
				for(var b=0;b<situation.numberOfBrothers;b++){
					situation.shares.push({
						relationshipToDeceased:"brother"+(b+1).toString(),
						share:adjustedShare/siblingCount
					})
					//console.log("adding share b");

				}
				for(var s=0;s<situation.numberOfSisters;s++){
					situation.shares.push({
						relationshipToDeceased:"sister"+(s+1).toString(),
						share:adjustedShare/siblingCount
					})
					//console.log("adding share s");

				}
			}else{
				if(situation.numberOfBrothers==0){
					if(situation.numberOfSisters==1){
			 			var adjustedShare=Math.min(0.5,unallocatedShare);
						situation.shares.push({
							relationshipToDeceased:"sister",
							share:adjustedShare
						})
					}
					if(situation.numberOfSisters>1){
						var adjustedShare=Math.min(2.0/3.0,unallocatedShare);

						for(var s2=0;s2<situation.numberOfSisters;s2++){
							situation.shares.push({
								relationshipToDeceased:"sister"+(s2+1).toString(),
								share:adjustedShare/situation.numberOfSisters
							});
							//console.log("adding share s2");
						}
					}
				}else{
					var numberOfSiblingsShares=(situation.numberOfBrothers*2)+situation.numberOfSisters;
					var sistersShare=unallocatedShare/numberOfSiblingsShares;
					for(var b3=0;b3<situation.numberOfBrothers;b3++){
						situation.shares.push({
							relationshipToDeceased:"brother"+(b3+1).toString(),
							share:sistersShare*2
						});
					}
					for(var s5=0;s5<situation.numberOfSisters;s5++){
						situation.shares.push({
							relationshipToDeceased:"sister"+(s5+1).toString(),
							share:sistersShare
						});
					}
				}
			}
		}
		//limited shares end
		this.calculateAllocatedShare(situation);
		
		var unallocatedShare = 1.0-situation.allocatedShare;
		if(unallocatedShare<0.00001){
			return;
		}
		if(situation.numberOfSons>0){

			var numberOfChildrenShares=(situation.numberOfSons*2)+situation.numberOfDaughters;
			var daughterShare=unallocatedShare/numberOfChildrenShares;
			for(var s4=0;s4<situation.numberOfSons;s4++){
				situation.shares.push({
					relationshipToDeceased:"son"+(s4+1).toString(),
					share:daughterShare*2
				});
			}
			for(var d2=0;d2<situation.numberOfDaughters;d2++){
				situation.shares.push({
					relationshipToDeceased:"daughter"+(d2+1).toString(),
					share:daughterShare
				});
			}
		
		}
		this.calculateAllocatedShare(situation);


    }
    calculateAllocatedShare(situation:inheritanceSituation){
    	var shares = situation.shares.map(function(share){
			return share.share;
		});
		var sum = [0].concat(shares).reduce(function(a,b){
			return a+b;
		});
		situation.allocatedShare = sum;
    }
    ngOnInit() {
  	var hasMotherStatuses=[true,false];
  	for(var m=0;m<hasMotherStatuses.length;m++){
  		var hasMother=hasMotherStatuses[m];
  		var hasFatherStatuses =[true,false];
  		for(var f=0;f<hasFatherStatuses.length;f++){
  			var hasFather = hasFatherStatuses[f];
  			var sonsNumberStatues=[0,1,2,3];
  			for(var s=0;s<sonsNumberStatues.length;s++){
  				var numberOfSons = sonsNumberStatues[s];
  				var daughtersNumberStatuses = [0,1,2,3]
  				for(var d=0;d<daughtersNumberStatuses.length;d++){
  					var numberOfDaughters = daughtersNumberStatuses[d];
  					var brotherNumberStatuses = [0,1,2,3];
  					for(var b=0;b<brotherNumberStatuses.length;b++){
  						var numberOfBrothers=brotherNumberStatuses[b];
  						var sisterNumberStatuses = [0,1,2,3];
  						for(var s2=0;s2<sisterNumberStatuses.length;s2++){
  							var numberOfSisters = sisterNumberStatuses[s2]
  							var isMaleStatuses = [true,false];
  							for(var m2=0;m2<isMaleStatuses.length;m2++){ 
  								var isMale=isMaleStatuses[m2];
  								var isMarriedStatuses=[true,false];
  								for(var m3=0;m3<isMarriedStatuses.length;m3++){
  									var isMarried=isMaleStatuses[m3];
  									var situation={
  										hasFather:hasFather,
  										hasMother:hasMother,
  										isMale:isMale,
  										isMarried:isMarried,
  										numberOfBrothers:numberOfBrothers,
  										numberOfDaughters:numberOfDaughters,
  										numberOfSisters:numberOfSisters,
  										numberOfSons:numberOfSons,
  										shares:[],
  										allocatedShare:0.0
  									};
							  		this.calculateShares(situation);

							  		this.exampleSituations.push(situation);
							  	//	console.log(situation.allocatedShare);
							  	}
						  	}
					  	}
				  	}
			  	}
		  	}
	  	}
  	}
  	this.overAllocationSituations=this.exampleSituations.filter(function(situation){
  		return (situation.allocatedShare-1.0)>0.00001
  	});
  	this.underAllocationSituations=this.exampleSituations.filter(function(situation){
  		return situation.allocatedShare==0.0
  	});
  }
  overAllocationSituations:inheritanceSituation[]=[]
  underAllocationSituations:inheritanceSituation[]=[]
}

