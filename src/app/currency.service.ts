import { Injectable } from '@angular/core';
export interface recognisedCurrency{
	name:string,
	threeLetterCode:string,
	symbol:string,
	usdRate:number
}

@Injectable()
export class CurrencyService {
  symbols:{}={}
  constructor() {
  	// from http://www.xe.com/symbols.php
  	var symbolsRawData="Albania Lek	ALL		Lek	Lek	76, 101, 107	4c, 65, 6b	 |Afghanistan Afghani	AFN		؋	؋	1547	60b	 |Argentina Peso	ARS		$	$	36	24	info|Aruba Guilder	AWG		ƒ	ƒ	402	192	 |Australia Dollar	AUD		$	$	36	24	 |Azerbaijan New Manat	AZN		ман	ман	1084, 1072, 1085	43c, 430, 43d	 |Bahamas Dollar	BSD		$	$	36	24	 |Barbados Dollar	BBD		$	$	36	24	 |Belarus Ruble	BYR		p.	p.	112, 46	70, 2e	 |Belize Dollar	BZD		BZ$	BZ$	66, 90, 36	42, 5a, 24	 |Bermuda Dollar	BMD		$	$	36	24	 |Bolivia Bolíviano	BOB		$b	$b	36, 98	24, 62	 |Bosnia and Herzegovina Convertible Marka	BAM		KM	KM	75, 77	4b, 4d	 |Botswana Pula	BWP		P	P	80	50	 |Bulgaria Lev	BGN		лв	лв	1083, 1074	43b, 432	 |Brazil Real	BRL		R$	R$	82, 36	52, 24	info|Brunei Darussalam Dollar	BND		$	$	36	24	 |Cambodia Riel	KHR		៛	៛	6107	17db	 |Canada Dollar	CAD		$	$	36	24	 |Cayman Islands Dollar	KYD		$	$	36	24	 |Chile Peso	CLP		$	$	36	24	info|China Yuan Renminbi	CNY		¥	¥	165	a5	info|Colombia Peso	COP		$	$	36	24	 |Costa Rica Colon	CRC		₡	₡	8353	20a1	 |Croatia Kuna	HRK		kn	kn	107, 110	6b, 6e	 |Cuba Peso	CUP		₱	₱	8369	20b1	 |Czech Republic Koruna	CZK		Kč	Kč	75, 269	4b, 10d	 |Denmark Krone	DKK		kr	kr	107, 114	6b, 72	info|Dominican Republic Peso	DOP		RD$	RD$	82, 68, 36	52, 44, 24	 |East Caribbean Dollar	XCD		$	$	36	24	 |Egypt Pound	EGP		£	£	163	a3	 |El Salvador Colon	SVC		$	$	36	24	 |Euro Member Countries	EUR		€	€	8364	20ac	 |Falkland Islands (Malvinas) Pound	FKP		£	£	163	a3	 |Fiji Dollar	FJD		$	$	36	24	 |Ghana Cedi	GHS		¢	¢	162	a2	 |Gibraltar Pound	GIP		£	£	163	a3	 |Guatemala Quetzal	GTQ		Q	Q	81	51	 |Guernsey Pound	GGP		£	£	163	a3	 |Guyana Dollar	GYD		$	$	36	24	 |Honduras Lempira	HNL		L	L	76	4c	 |Hong Kong Dollar	HKD		$	$	36	24	info|Hungary Forint	HUF		Ft	Ft	70, 116	46, 74	 |Iceland Krona	ISK		kr	kr	107, 114	6b, 72	 |India Rupee	INR						info|Indonesia Rupiah	IDR		Rp	Rp	82, 112	52, 70	 |Iran Rial	IRR		﷼	﷼	65020	fdfc	 |Isle of Man Pound	IMP		£	£	163	a3	 |Israel Shekel	ILS		₪	₪	8362	20aa	 |Jamaica Dollar	JMD		J$	J$	74, 36	4a, 24	 |Japan Yen	JPY		¥	¥	165	a5	info|Jersey Pound	JEP		£	£	163	a3	 |Kazakhstan Tenge	KZT		лв	лв	1083, 1074	43b, 432	 |Korea (North) Won	KPW		₩	₩	8361	20a9	 |Korea (South) Won	KRW		₩	₩	8361	20a9	 |Kyrgyzstan Som	KGS		лв	лв	1083, 1074	43b, 432	 |Laos Kip	LAK		₭	₭	8365	20ad	 |Lebanon Pound	LBP		£	£	163	a3	 |Liberia Dollar	LRD		$	$	36	24	 |Macedonia Denar	MKD		ден	ден	1076, 1077, 1085	434, 435, 43d	 |Malaysia Ringgit	MYR		RM	RM	82, 77	52, 4d	 |Mauritius Rupee	MUR		₨	₨	8360	20a8	 |Mexico Peso	MXN		$	$	36	24	info|Mongolia Tughrik	MNT		₮	₮	8366	20ae	 |Mozambique Metical	MZN		MT	MT	77, 84	4d, 54	 |Namibia Dollar	NAD		$	$	36	24	 |Nepal Rupee	NPR		₨	₨	8360	20a8	 |Netherlands Antilles Guilder	ANG		ƒ	ƒ	402	192	 |New Zealand Dollar	NZD		$	$	36	24	 |Nicaragua Cordoba	NIO		C$	C$	67, 36	43, 24	 |Nigeria Naira	NGN		₦	₦	8358	20a6	 |Korea (North) Won	KPW		₩	₩	8361	20a9	 |Norway Krone	NOK		kr	kr	107, 114	6b, 72	 |Oman Rial	OMR		﷼	﷼	65020	fdfc	 |Pakistan Rupee	PKR		₨	₨	8360	20a8	 |Panama Balboa	PAB		B/.	B/.	66, 47, 46	42, 2f, 2e	 |Paraguay Guarani	PYG		Gs	Gs	71, 115	47, 73	 |Peru Sol	PEN		S/.	S/.	83, 47, 46	53, 2f, 2e	info|Philippines Peso	PHP		₱	₱	8369	20b1	 |Poland Zloty	PLN		zł	zł	122, 322	7a, 142	 |Qatar Riyal	QAR		﷼	﷼	65020	fdfc	 |Romania New Leu	RON		lei	lei	108, 101, 105	6c, 65, 69	 |Russia Ruble	RUB		руб	руб	1088, 1091, 1073	440, 443, 431	info|Saint Helena Pound	SHP		£	£	163	a3	 |Saudi Arabia Riyal	SAR		﷼	﷼	65020	fdfc	 |Serbia Dinar	RSD		Дин.	Дин.	1044, 1080, 1085, 46	414, 438, 43d, 2e	 |Seychelles Rupee	SCR		₨	₨	8360	20a8	 |Singapore Dollar	SGD		$	$	36	24	 |Solomon Islands Dollar	SBD		$	$	36	24	 |Somalia Shilling	SOS		S	S	83	53	 |South Africa Rand	ZAR		R	R	82	52	 |Korea (South) Won	KRW		₩	₩	8361	20a9	 |Sri Lanka Rupee	LKR		₨	₨	8360	20a8	 |Sweden Krona	SEK		kr	kr	107, 114	6b, 72	info|Switzerland Franc	CHF		CHF	CHF	67, 72, 70	43, 48, 46	 |Suriname Dollar	SRD		$	$	36	24	 |Syria Pound	SYP		£	£	163	a3	 |Taiwan New Dollar	TWD		NT$	NT$	78, 84, 36	4e, 54, 24	info|Thailand Baht	THB		฿	฿	3647	e3f	 |Trinidad and Tobago Dollar	TTD		TT$	TT$	84, 84, 36	54, 54, 24	 |Turkey Lira	TRY						info|Tuvalu Dollar	TVD		$	$	36	24	 |Ukraine Hryvnia	UAH		₴	₴	8372	20b4	 |United Kingdom Pound	GBP		£	£	163	a3	 |United States Dollar	USD		$	$	36	24	 |Uruguay Peso	UYU		$U	$U	36, 85	24, 55	 |Uzbekistan Som	UZS		лв	лв	1083, 1074	43b, 432	 |Venezuela Bolivar	VEF		Bs	Bs	66, 115	42, 73	 |Viet Nam Dong	VND		₫	₫	8363	20ab	 |Yemen Rial	YER		﷼	﷼	65020	fdfc	 |Zimbabwe Dollar	ZWD		Z$	Z$	90, 36	5a, 24	 "

  	var lines=symbolsRawData.split('|');
  	for(var i=0;i<lines.length;i++){
  		var parts = lines[i].split('\t');
  		this.symbols[parts[1]]=parts[3];
  	}
  }
  currencies() :recognisedCurrency[]{
  	var self=this;

  	var result= this.isoCurrencyCodesText.split('|').map(iso=>{
  		var parts=iso.split('\t');
  		var exchangeRate = self.exchangeRatesData.rates[parts[0]];
  		if(exchangeRate==null){
  			return null;
  		}
  		var currencySymbol=this.symbols[parts[0]];
  		if(currencySymbol==null){
			currencySymbol="";
  		}
  		return {
  			name:parts[1] +" ("+parts[0]+")",
  			threeLetterCode:parts[0],
  			symbol:currencySymbol,
  			usdRate:exchangeRate
  		}
  	}).filter(currency=>{
  		return currency!=null;
  	}).sort((a,b)=>{
  		if (a.name < b.name)
    		return -1;
  		if (a.name > b.name)
    		return 1;
  		return 0;
  	});
  	//console.log(JSON.stringify(result));
  	return result;
  }
  //from http://www.xe.com/iso4217.php
  isoCurrencyCodesText:string="AED	United Arab Emirates Dirham|AFN	Afghanistan Afghani|ALL	Albania Lek|AMD	Armenia Dram|ANG	Netherlands Antilles Guilder|AOA	Angola Kwanza|ARS	Argentina Peso|AUD	Australia Dollar|AWG	Aruba Guilder|AZN	Azerbaijan New Manat|BAM	Bosnia and Herzegovina Convertible Marka|BBD	Barbados Dollar|BDT	Bangladesh Taka|BGN	Bulgaria Lev|BHD	Bahrain Dinar|BIF	Burundi Franc|BMD	Bermuda Dollar|BND	Brunei Darussalam Dollar|BOB	Bolivia Bolíviano|BRL	Brazil Real|BSD	Bahamas Dollar|BTN	Bhutan Ngultrum|BWP	Botswana Pula|BYR	Belarus Ruble|BZD	Belize Dollar|CAD	Canada Dollar|CDF	Congo/Kinshasa Franc|CHF	Switzerland Franc|CLP	Chile Peso|CNY	China Yuan Renminbi|COP	Colombia Peso|CRC	Costa Rica Colon|CUC	Cuba Convertible Peso|CUP	Cuba Peso|CVE	Cape Verde Escudo|CZK	Czech Republic Koruna|DJF	Djibouti Franc|DKK	Denmark Krone|DOP	Dominican Republic Peso|DZD	Algeria Dinar|EGP	Egypt Pound|ERN	Eritrea Nakfa|ETB	Ethiopia Birr|EUR	Euro Member Countries|FJD	Fiji Dollar|FKP	Falkland Islands (Malvinas) Pound|GBP	United Kingdom Pound|GEL	Georgia Lari|GGP	Guernsey Pound|GHS	Ghana Cedi|GIP	Gibraltar Pound|GMD	Gambia Dalasi|GNF	Guinea Franc|GTQ	Guatemala Quetzal|GYD	Guyana Dollar|HKD	Hong Kong Dollar|HNL	Honduras Lempira|HRK	Croatia Kuna|HTG	Haiti Gourde|HUF	Hungary Forint|IDR	Indonesia Rupiah|ILS	Israel Shekel|IMP	Isle of Man Pound|INR	India Rupee|IQD	Iraq Dinar|IRR	Iran Rial|ISK	Iceland Krona|JEP	Jersey Pound|JMD	Jamaica Dollar|JOD	Jordan Dinar|JPY	Japan Yen|KES	Kenya Shilling|KGS	Kyrgyzstan Som|KHR	Cambodia Riel|KMF	Comoros Franc|KPW	Korea (North) Won|KRW	Korea (South) Won|KWD	Kuwait Dinar|KYD	Cayman Islands Dollar|KZT	Kazakhstan Tenge|LAK	Laos Kip|LBP	Lebanon Pound|LKR	Sri Lanka Rupee|LRD	Liberia Dollar|LSL	Lesotho Loti|LYD	Libya Dinar|MAD	Morocco Dirham|MDL	Moldova Leu|MGA	Madagascar Ariary|MKD	Macedonia Denar|MMK	Myanmar (Burma) Kyat|MNT	Mongolia Tughrik|MOP	Macau Pataca|MRO	Mauritania Ouguiya|MUR	Mauritius Rupee|MVR	Maldives (Maldive Islands) Rufiyaa|MWK	Malawi Kwacha|MXN	Mexico Peso|MYR	Malaysia Ringgit|MZN	Mozambique Metical|NAD	Namibia Dollar|NGN	Nigeria Naira|NIO	Nicaragua Cordoba|NOK	Norway Krone|NPR	Nepal Rupee|NZD	New Zealand Dollar|OMR	Oman Rial|PAB	Panama Balboa|PEN	Peru Sol|PGK	Papua New Guinea Kina|PHP	Philippines Peso|PKR	Pakistan Rupee|PLN	Poland Zloty|PYG	Paraguay Guarani|QAR	Qatar Riyal|RON	Romania New Leu|RSD	Serbia Dinar|RUB	Russia Ruble|RWF	Rwanda Franc|SAR	Saudi Arabia Riyal|SBD	Solomon Islands Dollar|SCR	Seychelles Rupee|SDG	Sudan Pound|SEK	Sweden Krona|SGD	Singapore Dollar|SHP	Saint Helena Pound|SLL	Sierra Leone Leone|SOS	Somalia Shilling|SPL*	Seborga Luigino|SRD	Suriname Dollar|STD	São Tomé and Príncipe Dobra|SVC	El Salvador Colon|SYP	Syria Pound|SZL	Swaziland Lilangeni|THB	Thailand Baht|TJS	Tajikistan Somoni|TMT	Turkmenistan Manat|TND	Tunisia Dinar|TOP	Tonga Pa'anga|TRY	Turkey Lira|TTD	Trinidad and Tobago Dollar|TVD	Tuvalu Dollar|TWD	Taiwan New Dollar|TZS	Tanzania Shilling|UAH	Ukraine Hryvnia|UGX	Uganda Shilling|USD	United States Dollar|UYU	Uruguay Peso|UZS	Uzbekistan Som|VEF	Venezuela Bolivar|VND	Viet Nam Dong|VUV	Vanuatu Vatu|WST	Samoa Tala|XAF	Communauté Financière Africaine (BEAC) CFA Franc BEAC|XCD	East Caribbean Dollar|XDR	International Monetary Fund (IMF) Special Drawing Rights|XOF	Communauté Financière Africaine (BCEAO) Franc|XPF	Comptoirs Français du Pacifique (CFP) Franc|YER	Yemen Rial|ZAR	South Africa Rand|ZMW	Zambia Kwacha|ZWD	Zimbabwe Dollar"

  // from https://openexchangerates.org/api/historical/2016-06-20.json?app_id=b1cd718e0999498b896ceaf58ee9f5ae
  exchangeRatesData:any={
  "disclaimer": "Exchange rates provided for informational purposes only and do not constitute financial advice of any kind. Although every attempt is made to ensure quality, no guarantees are made of accuracy, validity, availability, or fitness for any purpose. All usage subject to acceptance of Terms: https://openexchangerates.org/terms/",
  "license": "Data sourced from various providers; resale prohibited; no warranties given of any kind. All usage subject to License Agreement: https://openexchangerates.org/license/",
  "timestamp": 1466463624,
  "base": "USD",
  "rates": {
    "AED": 3.67293,
    "AFN": 69.32,
    "ALL": 121.5785,
    "AMD": 477.0125,
    "ANG": 1.788725,
    "AOA": 165.782831,
    "ARS": 13.89425,
    "AUD": 1.341078,
    "AWG": 1.793333,
    "AZN": 1.518775,
    "BAM": 1.730233,
    "BBD": 2,
    "BDT": 78.459831,
    "BGN": 1.729644,
    "BHD": 0.377044,
    "BIF": 1671.645012,
    "BMD": 1,
    "BND": 1.345475,
    "BOB": 6.89784,
    "BRL": 3.39903,
    "BSD": 1,
    "BTC": 0.001368538905,
    "BTN": 67.257232,
    "BWP": 10.866888,
    "BYR": 19838.05,
    "BZD": 2.009963,
    "CAD": 1.28187,
    "CDF": 955.5165,
    "CHF": 0.961472,
    "CLF": 0.024598,
    "CLP": 682.974006,
    "CNY": 6.58148,
    "COP": 2987.696657,
    "CRC": 543.055995,
    "CUC": 1,
    "CUP": 24.728383,
    "CVE": 97.56,
    "CZK": 23.92596,
    "DJF": 177.841249,
    "DKK": 6.575828,
    "DOP": 45.9315,
    "DZD": 109.7583,
    "EEK": 13.85125,
    "EGP": 8.877999,
    "ERN": 15.0015,
    "ETB": 21.77704,
    "EUR": 0.883703,
    "FJD": 2.068233,
    "FKP": 0.681114,
    "GBP": 0.681114,
    "GEL": 2.19178,
    "GGP": 0.681114,
    "GHS": 3.918724,
    "GIP": 0.681114,
    "GMD": 42.71214,
    "GNF": 7345.062598,
    "GTQ": 7.637633,
    "GYD": 205.873336,
    "HKD": 7.76085,
    "HNL": 22.75266,
    "HRK": 6.648567,
    "HTG": 62.8272,
    "HUF": 277.091198,
    "IDR": 13275.266667,
    "ILS": 3.85967,
    "IMP": 0.681114,
    "INR": 67.39949,
    "IQD": 1181.1181,
    "IRR": 30331.5,
    "ISK": 122.759999,
    "JEP": 0.681114,
    "JMD": 125.629,
    "JOD": 0.708206,
    "JPY": 103.969199,
    "KES": 101.303899,
    "KGS": 67.75,
    "KHR": 4089.699951,
    "KMF": 432.244279,
    "KPW": 900.09,
    "KRW": 1163.523348,
    "KWD": 0.301006,
    "KYD": 0.824082,
    "KZT": 334.877592,
    "LAK": 8101.050098,
    "LBP": 1508.566667,
    "LKR": 145.400001,
    "LRD": 90.50905,
    "LSL": 14.91196,
    "LTL": 3.028637,
    "LVL": 0.621625,
    "LYD": 1.369338,
    "MAD": 9.679548,
    "MDL": 19.73752,
    "MGA": 3276.181667,
    "MKD": 54.45744,
    "MMK": 1184.9,
    "MNT": 1950.5,
    "MOP": 7.99315,
    "MRO": 356.550669,
    "MTL": 0.683738,
    "MUR": 35.439901,
    "MVR": 15.276667,
    "MWK": 703.053123,
    "MXN": 18.69629,
    "MYR": 4.06651,
    "MZN": 61.56,
    "NAD": 14.91146,
    "NGN": 199.07875,
    "NIO": 28.56725,
    "NOK": 8.294009,
    "NPR": 107.7062,
    "NZD": 1.4076,
    "OMR": 0.385007,
    "PAB": 1,
    "PEN": 3.29579,
    "PGK": 3.132925,
    "PHP": 46.31709,
    "PKR": 104.687721,
    "PLN": 3.885489,
    "PYG": 5634.683268,
    "QAR": 3.640346,
    "RON": 4.013683,
    "RSD": 109.171381,
    "RUB": 64.319719,
    "RWF": 782.764672,
    "SAR": 3.749986,
    "SBD": 7.80013,
    "SCR": 13.11081,
    "SDG": 6.084756,
    "SEK": 8.257694,
    "SGD": 1.344353,
    "SHP": 0.681114,
    "SLL": 3942.5,
    "SOS": 577.905003,
    "SRD": 7,
    "STD": 21682,
    "SVC": 8.7438,
    "SYP": 218.952332,
    "SZL": 14.92446,
    "THB": 35.21294,
    "TJS": 7.8685,
    "TMT": 3.5014,
    "TND": 2.150939,
    "TOP": 2.233566,
    "TRY": 2.908555,
    "TTD": 6.64535,
    "TWD": 32.26977,
    "TZS": 2193.700033,
    "UAH": 24.90286,
    "UGX": 3354.691683,
    "USD": 1,
    "UYU": 30.65232,
    "UZS": 2946.854981,
    "VEF": 9.9725,
    "VND": 22291,
    "VUV": 111.190001,
    "WST": 2.581854,
    "XAF": 581.455147,
    "XAG": 0.0571615,
    "XAU": 0.000778,
    "XCD": 2.70302,
    "XDR": 0.705437,
    "XOF": 582.087827,
    "XPD": 0.001816,
    "XPF": 105.537812,
    "XPT": 0.001013,
    "YER": 249.428001,
    "ZAR": 14.91023,
    "ZMK": 5252.024745,
    "ZMW": 10.9858,
    "ZWL": 322.387247
  }
}

 
  

}
