import { ChartsModule } from 'ng2-charts/ng2-charts';
import { NgClass} from '@angular/common';
import { NgModule, Component, OnInit, OnChanges,Input } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
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
	allocatedShare:number,
	allocatedShareFraction:Fraction,
	unallocatedShare:number,
	unallocatedShareFraction:Fraction,
	hasDependentChildren:boolean
}
export class Fraction{
	constructor(public top:number,public bottom:number){

	}
	primes:number[]=[2,3,5,7,11,13,17,19]
	static add(a:Fraction,b:Fraction){
		var sum= new Fraction(a.top*b.bottom + b.top*a.bottom,a.bottom*b.bottom);
		sum.reduce();
		return sum;
	}
	static minus(a:Fraction,b:Fraction){
	var subtraction= new Fraction(a.top*b.bottom - b.top*a.bottom,a.bottom*b.bottom);
		subtraction.reduce();
		return subtraction;	
	}
	static minimum(a:Fraction,b:Fraction){
		var c=Fraction.minus(a,b);
		if(c.top>0){return b;}
		return a;
	}
	static divideByInt(a:Fraction,n:number){
		var f = new Fraction(a.top,a.bottom*Math.round(n))
		f.reduce();
		return f;
	}
	static multiplyByInt(a:Fraction,n:number){
		var f = new Fraction(a.top*Math.round(n),a.bottom)
		f.reduce();
		return f;
	}
	reduce(){
		if(this.top==0){
			this.bottom=1;
			return;
		}
		var reduced=true;
		while(reduced){
			reduced=false;
			for(var p=0;p<this.primes.length;p++){
				var prime=this.primes[p];
				if((this.top%prime)==0 && (this.bottom%prime)==0){
					this.top = this.top/prime;
					this.bottom=this.bottom/prime;
					reduced=true;
					break;
				}
			}
		}

	}
}
export interface inheritanceShare{
	relationshipToDeceased:string,
	counter:number,
	share:number,
	fraction:Fraction
}
@Component({
  selector: 'app-inheritance-calculator',
  templateUrl: 'inheritance-calculator.component.html',
  styleUrls: ['inheritance-calculator.component.css']
  // directives:[NgClass,CORE_DIRECTIVES,FORM_DIRECTIVES,ROUTER_DIRECTIVES]
 // directives:[NgClass]
})
export class InheritanceCalculatorComponent implements OnInit,OnChanges {

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
		allocatedShare:0,
		allocatedShareFraction:new Fraction(0,1),
		unallocatedShare:1,
		unallocatedShareFraction:new Fraction(1,1),
		hasDependentChildren:true
	};
	pieChartData:number[]
	pieChartLabels:string[]
	pieChartColours:{}[]
	pieChartType:string='pie'
	pieChartOptions={
		tooltips:{
			callbacks:{
				label:function(item,data){

					return data.labels[item.index]+" : "+data.datasets[0].data[item.index].toFixed(3)+"%";
				}
			}
		}
	}
	pieChartColors:{}
	ngOnChanges(changes) {
      console.log(changes);
  	}
  	getRelationshipType(name:string){
  		var spaceIndex=name.indexOf(" ");
  		if(spaceIndex>0){
  			return name.substring(0,spaceIndex);
  		}
  		return name;
  	}
  	sortDescending(shares:inheritanceShare[]){
  		return shares.sort((a,b)=>{
			if(Number(a.share)>Number(b.share)){return -1;}
			if(Number(a.share)<Number(b.share)){return 1;}
			if(a.relationshipToDeceased>b.relationshipToDeceased){return 1;}
			if(a.relationshipToDeceased<b.relationshipToDeceased){return -1;}
			if(a.counter>b.counter){return 1;}
			if(a.counter<b.counter){return -1;}
			return 0;
		});
  	}
  	yesNoStates=[
  		{
	  		value:true,display:"Yes"
	  	},
	  	{
	  		value:false,display:"No"
	  	}
  	]
  	genders=[
  		{
	  		value:true,display:"Male"
	  	},
	  	{
	  		value:false,display:"Female"
	  	}
  	]
  	marritalStatuses=[
	  	{
	  		value:true,display:"Married"
	  	},
	  	{
	  		value:false,display:"Single"
	  	}
  	];
  	pieChartDataStructure:{}
  	setShares(){
		this.calculateShares(this.model);
		this.pieChartData=this.model.shares.map((a)=>{
			return Number((a.share*100.0).toFixed(3));
		});
		this.pieChartColors= this.buildColourProperties(this.model.shares.map(s=>
		{
		 	return this.getColour(s);
		}));
		this.pieChartData=this.pieChartData;
		this.pieChartLabels=this.model.shares.map(s=>{
			if(s.counter)
			{
				return s.relationshipToDeceased+ " "+s.counter;
			}
			return s.relationshipToDeceased;
		});
		


		// this.pieChartColours = [this.model.shares.map(s=>
		//  {
		//  	return this.getColour(s.relationshipToDeceased);
		//  })];
	}
	buildColourProperties(colours:string[]){
		return [{
			backgroundColor:colours,
			hoverBackgroundColor:colours.map(c=>this.getHoverColour(c))
		}];
	}
	getHoverColour(hexColour:string){
		console.log(hexColour);
		var r=hexColour.substring(1,3);
		var g=hexColour.substring(3,5);
		var b=hexColour.substring(5);
		console.log(r+" "+g+" "+b);
		
		var reducedColour= "#"
		+this.reduceIntensity(r)
		+this.reduceIntensity(g)
		+this.reduceIntensity(b);
		console.log(reducedColour);
		return reducedColour;
	}
	reduceIntensity(hexPair:string){
		var val = parseInt(hexPair, 16);
		val=val-20;
		if(val<16){
			return("00");
		}
		return val.toString(16).toUpperCase()
	}
	getColour(share:inheritanceShare){

		if(share.relationshipToDeceased== "zakat"){
			return "#DDFFDD";
		}
		if(share.relationshipToDeceased== "mother"){
			return "#FF9A56";
		}
		if(share.relationshipToDeceased=="father"){
			return "#824E2C"
		}
		if(share.relationshipToDeceased=="wives"){
			return "#A7E2E1"
		}
		if(share.relationshipToDeceased=="husband"){
			return "#35E0DA"
		}
		if(share.relationshipToDeceased=="son"){
			var hex=this.toHexPair(share.counter);
			return "#0000"+hex;
		}
		if(share.relationshipToDeceased=="daughter"){
			var hex=this.toHexPair(share.counter);
			return "#"+hex+"00"+hex;
		}
		if(share.relationshipToDeceased=="brother"){
			var hex=this.toHexPair(share.counter);
			return "#00"+hex+"00";
		}
		if(share.relationshipToDeceased=="sister"){
			var hex=this.toHexPair(share.counter)
			return "#"+hex+"0000";
		}
	}
	toHexPair(num:number){
		var val=128;
	//	console.log("num="+num);
		if(num!=null){
			val= 128+((num*32)%127)
		}
	//	console.log("value="+val);
		var hex=val.toString(16)
	//	console.log("hex="+hex);
		return hex;
	}
	exampleSituations:inheritanceSituation[]=[]
    constructor() { }
    calculateShares(situation:inheritanceSituation){
    	situation.shares=[];
    	//fixed shares start
		if(situation.numberOfSons==0 && situation.numberOfDaughters==1){
			situation.shares.push({
				relationshipToDeceased:"daughter",
				counter:null,
				share:0.5,
				fraction:new Fraction(1,2)
			});
		}
		if(situation.numberOfSons==0 && situation.numberOfDaughters>=2){
			for(var d=0;d<situation.numberOfDaughters;d++)
			situation.shares.push({
				relationshipToDeceased:"daughter",
				counter:d+1,
				share:(2.0/3.0)/situation.numberOfDaughters,
				fraction:Fraction.divideByInt(new Fraction(2,3),situation.numberOfDaughters)
			});
		}
		if(situation.numberOfSons +situation.numberOfDaughters>0){
			if(situation.hasFather){
				situation.shares.push(
				{
					relationshipToDeceased:"father",
					counter:null,
					share:(1.0/6.0),
					fraction:new Fraction(1,6)
				});
			}
			if(situation.hasMother){
				situation.shares.push(
				{
					relationshipToDeceased:"mother",
					counter:null,
					share:(1.0/6.0),
					fraction:new Fraction(1,6)
				});
			}
		}
		if(situation.numberOfSons +situation.numberOfDaughters	==0){
			if(situation.hasFather){
				situation.shares.push(
				{
					relationshipToDeceased:"father",
					counter:null,
					share:(1.0/6.0),
					fraction:new Fraction(1,6)
				});
			}
			if(situation.hasMother){
				if(situation.numberOfBrothers==0){
					situation.shares.push(
					{
						relationshipToDeceased:"mother",
						counter:null,
						share:(1.0/3.0),
						fraction:new Fraction(1,3)
					});
				}else{
					situation.shares.push(
					{
						relationshipToDeceased:"mother",
						counter:null,
						share:(1.0/6.0),
						fraction:new Fraction(1,6)
					});
				}
			}
		}
		//fard shares end
		this.calculateAllocatedShare(situation);
		if(situation.unallocatedShare<0.00001){
			return;
		}
		if(situation.unallocatedShareFraction.top==0){
			return;
		}
		if(situation.isMarried){
			if(situation.numberOfSons+situation.numberOfDaughters==0){
				if(!situation.isMale){
					situation.shares.push({
						relationshipToDeceased:"husband",
						counter:null,
						share: Math.min(0.5,situation.unallocatedShare),
						fraction:Fraction.minimum(new Fraction(1,2),situation.unallocatedShareFraction)

					});
				}
				if(situation.isMale){
					situation.shares.push({
						relationshipToDeceased:"wives",
						counter:null,
						share:Math.min(0.25,situation.unallocatedShare),
						fraction:Fraction.minimum(new Fraction(1,4),situation.unallocatedShareFraction)

					});
				}
			}else{
				if(!situation.isMale){
					situation.shares.push({
						relationshipToDeceased:"husband",
						counter:null,
						share:Math.min(0.25,situation.unallocatedShare),
						fraction:Fraction.minimum(new Fraction(1,4),situation.unallocatedShareFraction)
					});
				}
				if(situation.isMale){
					situation.shares.push({
						relationshipToDeceased:"wives",
						counter:null,
						share:Math.min(0.125,situation.unallocatedShare),
						fraction:Fraction.minimum(new Fraction(1,8),situation.unallocatedShareFraction)
					});
				}
			}
		}
		this.calculateAllocatedShare(situation);
		
		if(situation.unallocatedShare<0.00001){
			return;
		}
		if(situation.unallocatedShareFraction.top==0){
			return;
		}
		//kalalah
		if(
			(!situation.hasFather && !situation.hasMother)
			&& ((situation.numberOfSons==0 && situation.numberOfDaughters==0) 
				|| !situation.hasDependentChildren)
			){
			var siblingCount=situation.numberOfSisters+situation.numberOfBrothers;
			if((situation.numberOfSons+situation.numberOfDaughters>0)||!situation.hasDependentChildren){

				var adjustedShare=Math.min(1.0/3.0,situation.unallocatedShare);
				var adjustedShareFraction=Fraction.minimum(new Fraction(1,3),situation.unallocatedShareFraction);

				if(siblingCount==1){
					adjustedShare=Math.min(1.0/6.0,situation.unallocatedShare);
					adjustedShareFraction=Fraction.minimum(new Fraction(1,6),situation.unallocatedShareFraction);
				}
				
				for(var b=0;b<situation.numberOfBrothers;b++){
					situation.shares.push({
						relationshipToDeceased:"brother",
						counter:b+1,
						share:adjustedShare/siblingCount,
						fraction:Fraction.divideByInt(adjustedShareFraction,siblingCount)
					})
					//console.log("adding share b");

				}
				for(var s=0;s<situation.numberOfSisters;s++){
					situation.shares.push({
						relationshipToDeceased:"sister",
						counter:s+1,
						share:adjustedShare/siblingCount,
						fraction:Fraction.divideByInt(adjustedShareFraction,siblingCount)
		
					})
					//console.log("adding share s");

				}
			}else{
				if(situation.numberOfBrothers==0){
					if(situation.numberOfSisters==1){
			 			var adjustedShare=Math.min(0.5,situation.unallocatedShare);
			 			var adjustedShareFraction = Fraction.minimum(new Fraction(1,2),situation.unallocatedShareFraction)
						situation.shares.push({
							relationshipToDeceased:"sister",
							counter:null,
							share:adjustedShare,
							fraction:adjustedShareFraction
						})
					}
					if(situation.numberOfSisters>1){
						var adjustedShare=Math.min(2.0/3.0,situation.unallocatedShare);
						var adjustedShareFraction = Fraction.minimum(new Fraction(2,3),situation.unallocatedShareFraction)
						for(var s2=0;s2<situation.numberOfSisters;s2++){
							situation.shares.push({
								relationshipToDeceased:"sister",
								counter:s2+1,
								share:adjustedShare/situation.numberOfSisters,
								fraction:Fraction.divideByInt(adjustedShareFraction,situation.numberOfSisters)
							});
							//console.log("adding share s2");
						}
					}
				}else{
					var numberOfSiblingsShares=(situation.numberOfBrothers*2)+situation.numberOfSisters;
					var sistersShare=situation.unallocatedShare/numberOfSiblingsShares;
					var sistersShareFraction =
						Fraction.divideByInt(situation.unallocatedShareFraction,numberOfSiblingsShares);

					for(var b3=0;b3<situation.numberOfBrothers;b3++){
						situation.shares.push({
							relationshipToDeceased:"brother",
							counter:b3+1,
							share:sistersShare*2,
							fraction:Fraction.multiplyByInt(sistersShareFraction,2)
						});
					}
					for(var s5=0;s5<situation.numberOfSisters;s5++){
						situation.shares.push({
							relationshipToDeceased:"sister",
							counter:s5+1,
							share:sistersShare,
							fraction:sistersShareFraction
						});
					}
				}
			}
		}
		//limited shares end
		this.calculateAllocatedShare(situation);
		if(situation.unallocatedShare<0.00001){
			return;
		}
		if(situation.unallocatedShareFraction.top==0){
			return
		}
		if(situation.numberOfSons>0){

			var numberOfChildrenShares=(situation.numberOfSons*2)+situation.numberOfDaughters;
			var daughterShare=situation.unallocatedShare/numberOfChildrenShares;
			var daughtersShareFraction = Fraction.divideByInt(situation.unallocatedShareFraction,numberOfChildrenShares)
			for(var s4=0;s4<situation.numberOfSons;s4++){
				situation.shares.push({
					relationshipToDeceased:"son",
					counter:s4+1,
					share:daughterShare*2,
					fraction:Fraction.multiplyByInt(daughtersShareFraction,2)
				});
			}
			for(var d2=0;d2<situation.numberOfDaughters;d2++){
				situation.shares.push({
					relationshipToDeceased:"daughter",
					counter:d2+1,
					share:daughterShare,
					fraction:daughtersShareFraction
				});
			}
		
		}
		this.calculateAllocatedShare(situation);
		if(situation.unallocatedShare<0.00001){
			return;
		}
		situation.shares.push({
			relationshipToDeceased:"zakat",
			counter:null,
			share:situation.unallocatedShare,
			fraction:situation.unallocatedShareFraction
		});
		this.calculateAllocatedShare(situation);

		
    }
    calculateAllocatedShare(situation:inheritanceSituation){
		situation.shares=this.sortDescending(situation.shares);
    	var shares = situation.shares.map(function(share){
			return share.share;
		});
		var sum = [0].concat(shares).reduce(function(a,b){
			return a+b;
		});
		situation.allocatedShare = sum;
		situation.unallocatedShare = 1.0-sum;
		this.calculateAllocatedShareFraction(situation);
    }
    calculateAllocatedShareFraction(situation:inheritanceSituation){
		situation.shares=this.sortDescending(situation.shares);
    	var fractions = situation.shares.map(function(share){
			return share.fraction;
		});
		var sum = [new Fraction(0,1)].concat(fractions).reduce(function(a,b){
			return Fraction.add(a,b);
		});
		situation.allocatedShareFraction = sum;
		situation.unallocatedShareFraction = Fraction.minus(new Fraction(1,1),sum)
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
  									var hasDependentChildrenStatuses=[true,false];
  									for(var ca=0;ca<hasDependentChildrenStatuses.length;ca++)
  									{
  										var hasDependentChildren=hasDependentChildrenStatuses[ca];
	  									var situation:inheritanceSituation={
	  										hasFather:hasFather,
	  										hasMother:hasMother,
	  										isMale:isMale,
	  										isMarried:isMarried,
	  										numberOfBrothers:numberOfBrothers,
	  										numberOfDaughters:numberOfDaughters,
	  										numberOfSisters:numberOfSisters,
	  										numberOfSons:numberOfSons,
	  										shares:[],
	  										allocatedShare:0.0,
	  										unallocatedShare:1.0,
	  										allocatedShareFraction:new Fraction(0,1),
	  										unallocatedShareFraction:new Fraction(1,1),
	  										hasDependentChildren:hasDependentChildren
	  									};
								  		this.calculateShares(situation);
								  		this.exampleSituations.push(situation);
  									}

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
  	this.setShares()
  }
  overAllocationSituations:inheritanceSituation[]=[]
  underAllocationSituations:inheritanceSituation[]=[]
}

