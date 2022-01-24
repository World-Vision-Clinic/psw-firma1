import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from "@angular/router";
import { Observable } from "rxjs";
import { JwtHelperService } from '@auth0/angular-jwt';
import { SignalService } from './signalr.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'ManagerFrontApp';
  isManagerFrontApp = false;

  constructor(
    private router: Router,
    private jwtHelper: JwtHelperService,
  ) {}

  ngOnInit() {
    if(window.location.href.includes('manager-front-app'))
      this.isManagerFrontApp = true;
  }

  getToken():string{
    const token = localStorage.getItem("PSWtoken")
    if(token){return token}
    else{return ""}
  }

  isLoggedIn(): boolean{
    const token = this.getToken()
    const decodedToken = this.jwtHelper.decodeToken(token);
    if(token && !this.jwtHelper.isTokenExpired(token) && decodedToken.role == "Manager"){
        return true;
    }
    return false;
  }

  logout(): void{
    localStorage.clear()
  }
}
