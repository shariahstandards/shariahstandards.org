import { Component, OnInit } from '@angular/core';

export interface asset{
	description:string,
	amount:number
} 
export interface debt{
	description:string,
	amount:number
} 
export interface country{
	name:string,
	personalWealthAllowance:number,
  currencySymbol:string
	currencyName:string
}
@Component({
  selector: 'app-zakat-calculator',
  templateUrl: 'zakat-calculator.component.html',
  styleUrls: ['zakat-calculator.component.css']
})
export class ZakatCalculatorComponent implements OnInit {

  constructor() { }
  addAssetModel:any={description:""};
  assets:asset[]=[];
  addDebtModel:any={description:""};
  debts:debt[]=[];
  countries:country[]=[{
  	name:"UK",
  	currencyName:"GBP",
    currencySymbol:"£",
  	personalWealthAllowance:10000
  }
  ,{
    name:"USA",
    currencyName:"USD",
    currencySymbol:"$",
    personalWealthAllowance:15000
  }];
  dependents:number=0;
  country:country;
  ngOnInit() {
  	this.country=this.countries[0];

  }
  addAsset(){
  	if(this.addAssetModel.description 
      && this.addAssetModel.description!=""
      && this.addAssetModel.amount){
  		this.assets.push({
  			description:this.addAssetModel.description,
  			amount:this.addAssetModel.amount
  		});
  		this.addAssetModel={description:""};
  		this.setZakatableWealth();
  	}
  }
  addDebt(){
  	if(this.addDebtModel.description
      && this.addDebtModel.description!=""
     && this.addDebtModel.amount){
  		this.debts.push({
  			description:this.addDebtModel.description,
  			amount:this.addDebtModel.amount
  		});
  		this.addDebtModel={description:""};
  		this.setZakatableWealth();
  	}
  }
  removeAsset(asset:asset){
  	var assetIndex = this.assets.indexOf(asset);
    this.assets.splice(assetIndex,1)
  	this.setZakatableWealth();
  }
  removeDebt(debt:debt){
  	var debtIndex = this.debts.indexOf(debt);
    this.debts.splice(debtIndex,1);
  	this.setZakatableWealth();
  }
  zakatWealthAllowance(){
  	return (1+this.dependents)*this.country.personalWealthAllowance;
  }
  zakatableWealthTotal:number=0;
  setZakatableWealth(){
  	var assets = this.assets.map(function(asset){
  		return asset.amount;
  	});
  	console.log(JSON.stringify(assets));
  	var debts = this.debts.map(function(debt){
  		return -debt.amount;
  	});
  	console.log(JSON.stringify(debts));

  	var total = [-this.zakatWealthAllowance()].concat(assets).concat(debts).reduce(function(a,b){
  		return a+b;
  	});
  	console.log(total);

  	this.zakatableWealthTotal = Math.max(0,total);
  }
  
}
