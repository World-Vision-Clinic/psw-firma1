import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { RegisterService } from '../register.service';

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

  login() {
    this._registerService.login(this.LoginDTO).subscribe(data => localStorage.setItem('PSWtoken', data.token),
      error =>console.log(error),
      () => console.log(localStorage.getItem('PSWtoken')));
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
