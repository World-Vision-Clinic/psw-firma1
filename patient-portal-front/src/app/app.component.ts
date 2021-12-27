import { Component} from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from "@angular/router";
import { Observable } from "rxjs";
import { JwtHelperService } from '@auth0/angular-jwt';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'patient-portal-front';

  constructor(
    private router: Router,
    private jwtHelper: JwtHelperService
  ) {}

  getToken():string{
    const token = localStorage.getItem("PSWtoken")
    if(token){return token}
    else{return ""}
  }

  isLoggedIn(): boolean{
    const token = this.getToken()
    const decodedToken = this.jwtHelper.decodeToken(token);
    if(token && !this.jwtHelper.isTokenExpired(token) && decodedToken.role == "Patient"){
        return true;
    }
    return false;
  }

  logout(): void{
    localStorage.clear()
  }
}


