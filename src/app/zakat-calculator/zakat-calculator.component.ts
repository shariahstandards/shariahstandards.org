import { Component, OnInit } from '@angular/core';

export interface asset{
	description:string,
	amount:number,
  formattedAmount:string
} 
export interface debt{
	description:string,
	amount:number,
  formattedAmount:string
} 
export interface currency{
	name:string,
	personalWealthAllowance:number,
  prefix:string
	suffix:string
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
  currencies:currency[]=[{
  	name:"UK Pounds",
  	prefix:"£",
    suffix:"GBP",
  	personalWealthAllowance:10000
  }
  ,{
    name:"US Dollars",
    suffix:"USD",
    prefix:"$",
    personalWealthAllowance:14000
  },
  {
    name:"Euros",
    suffix:"EUR",
    prefix:"€",
    personalWealthAllowance:12000
  }];
  dependents:number=0;
  currency:currency;
  ngOnInit() {
  	this.currency=this.currencies[0];

  }
  addAsset(){
  	if(this.addAssetModel.description 
      && this.addAssetModel.description!=""
      && this.addAssetModel.amount){
  		this.assets.push({
  			description:this.addAssetModel.description,
  			amount:this.addAssetModel.amount,
        formattedAmount:this.formatCurrency(this.addAssetModel.amount)
  		});
  		this.addAssetModel={description:""};
  		this.setZakatableWealth();
  	}
  }
  formatCurrency(x:number){
    return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
  }
  addDebt(){
  	if(this.addDebtModel.description
      && this.addDebtModel.description!=""
     && this.addDebtModel.amount){
  		this.debts.push({
  			description:this.addDebtModel.description,
        formattedAmount:this.formatCurrency(this.addDebtModel.amount),
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
  	return (1+this.dependents)*this.currency.personalWealthAllowance;
  }
  formattedZakatWealthAllowance(){
    return this.formatCurrency( this.zakatWealthAllowance());
  }
  formattedZakatDue(){
     return this.formatCurrency(this.zakatableWealthTotal/40.0); 
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
