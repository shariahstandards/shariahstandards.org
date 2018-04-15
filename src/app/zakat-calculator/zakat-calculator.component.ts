import { Component, OnInit } from '@angular/core';
import {CurrencyService} from '../currency.service';
export interface asset{
	description:string,
	amount:number
} 
export interface debt{
	description:string,
	amount:number
} 
export interface currency{
	name:string,
	personalWealthAllowance:number,
  prefix:string,
	suffix:string
}
@Component({
  selector: 'app-zakat-calculator',
  templateUrl: './zakat-calculator.component.html',
  styleUrls: ['./zakat-calculator.component.css'],
  providers:[CurrencyService],
  // directives: [ROUTER_DIRECTIVES]
})
export class ZakatCalculatorComponent implements OnInit {

  constructor(private currencyService:CurrencyService) { }
  addAssetModel:any={description:""};
  assets:asset[]=[];
  addDebtModel:any={description:""};
  debts:debt[]=[];
  currencies:currency[]
  zakatFreeWealthAllowanceInUsd:number=15000
  eurosPerCurrencyUnit:number
  dependents:number=0;
  currency:currency;
  ngOnInit() {
    var allRecognisedCurrencies = this.currencyService.currencies();
    this.currencies = allRecognisedCurrencies.map(rCurrency=>{
      return{
        suffix:rCurrency.threeLetterCode,
        prefix:rCurrency.symbol,
        name:rCurrency.name,
        personalWealthAllowance:Math.ceil(this.zakatFreeWealthAllowanceInUsd*rCurrency.usdRate)
      }
    })
    var currencyDictionary={};
    for (var i = 0; i < this.currencies.length; ++i) {
      currencyDictionary[this.currencies[i].suffix] = this.currencies[i];
    }
  	this.currency=currencyDictionary["USD"];
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
  	}
  }
  formatCurrency(x:number){
    return this.currency.prefix + x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",") + " ("+this.currency.suffix+")";
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
  	}
  }
  removeAsset(asset:asset){
  	var assetIndex = this.assets.indexOf(asset);
    this.assets.splice(assetIndex,1)
  }
  removeDebt(debt:debt){
  	var debtIndex = this.debts.indexOf(debt);
    this.debts.splice(debtIndex,1);
  }
 
  //allAssets:number=0;
  //allDebts:number=0;
  //zakatFreeWealthAllowance:number=0;
  //zakatableWealthTotal:number=0;
//  zakatDue:number=0;
 // formattedAllAssets:string="";
  //formattedAllDebts:string="";
 // formattedZakatFreeWealthAllowance:string="";
  //formattedZakatableWealthTotal:string="";
  //formattedZakatDue:string="";

  sum(parts:number[]):number{
    return [0].concat(parts).reduce(function(a,b){
      return a+b;
    });
  }
  allAssets(){ 
    return this.sum(this.assets.map(function(asset){
         return Math.ceil(asset.amount);
       }));
  }
  allDebts(){
    return this.sum(this.debts.map(function(debt){
       return Math.ceil(debt.amount);
     }));
  }
  zakatFreeWealthAllowance(){
   return Math.ceil((1+this.dependents)*this.currency.personalWealthAllowance);
  }
  zakatableWealthTotal(){
    return Math.max(0,this.allAssets() -this.allDebts() - this.zakatFreeWealthAllowance())
  }
  zakatDue(){
    return Math.ceil(this.zakatableWealthTotal()/40.0);
  }
  formattedAllAssets(){
    return this.formatCurrency(this.allAssets());
  }
  formattedAllDebts(){
    return this.formatCurrency(this.allDebts());
  }
  formattedZakatFreeWealthAllowance(){
    return this.formatCurrency(this.zakatFreeWealthAllowance());
  }
  formattedZakatableWealthTotal(){
    return this.formatCurrency(this.zakatableWealthTotal());
  }
  formattedZakatDue(){
    return this.formatCurrency(this.zakatDue());
  }
}