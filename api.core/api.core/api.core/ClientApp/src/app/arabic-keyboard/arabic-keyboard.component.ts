import { Component, OnInit, Output,EventEmitter } from '@angular/core';

@Component({
  selector: 'arabic-keyboard',
  templateUrl: './arabic-keyboard.component.html',
  styleUrls: ['./arabic-keyboard.component.css']
})
export class ArabicKeyboardComponent implements OnInit {
	shifted:boolean=false;
	show:boolean=false;
	value:string;
    @Output() onValueChanged = new EventEmitter<string>();
    constructor() { }
	keys = {
		rows:["ذ1234567890-=".split(''),"ضصثقفغعهخحجد".split(''),"شسيبلاتنمكط\\".split(''),"\\ئءؤرلاىةوزظ".split('')],
		shiftedRows:["ّ!@#$%^&*)(_+".split(''),"ًٌَُلإإ‘×؛<>".split(''),"ٍِ][لأأـ،/:\"".split(''),"|~ْ}{لآآ’,.؟".split('')]
	};
	add(key){
		console.log(key+" pressed");
		this.value+=key;
		this.onValueChanged.emit(this.value);
		this.shifted=false;
	}
	shift(){
		this.shifted=!this.shifted;
		console.log("shift pressed");
	}
	backspace(){
		this.value=this.value.substring(0,this.value.length-1);
		this.onValueChanged.emit(this.value);
		
		console.log("backspace pressed");
	}
	ngOnInit() {
		this.value='';
	}
	toggleShow(value:string){
		this.value=value;
		this.show=!this.show;
	}


}
