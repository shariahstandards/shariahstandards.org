import { Component, OnInit } from '@angular/core';
import {RegistrationModel} from './registration-model.class'
@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  constructor() {
  	this.registrationModel=new RegistrationModel();
  }

  registrationModel:RegistrationModel
  ngOnInit() {
  }
  register(){
  	
  }

}
