import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { RegisterService } from '../register.service';
import { SurveyComponent } from '../survey/survey/survey.component';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  LoginDTO = {
    username:"",
    password:""
  }
  token = ""


  constructor(private router: Router, private _registerService: RegisterService) { }

  ngOnInit(): void {
  }

  handleError(){
    this.LoginDTO.username = ""
    this.LoginDTO.password = ""
    alert("Failed to login please try again")
  }

  login() {
    this._registerService.login(this.LoginDTO).subscribe(
      data => localStorage.setItem('PSWtoken', data.token),
      error => this.handleError(),
      () => this.router.navigate(["/medical-record"]));
  }
  verifyPassword() {
    if(this.LoginDTO.password == "")
      return false;
    return true;
  }

  verifyUsername() {
    if(this.LoginDTO.username == "")
      return false;
    return true;
  }

  contentIsValid() {
    if(!this.verifyPassword())
      return false;
    if(!this.verifyUsername())
      return false;
    return true;
  }
}
