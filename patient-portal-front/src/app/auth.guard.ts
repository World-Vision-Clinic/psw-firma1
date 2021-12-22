import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from "@angular/router";
import { Observable } from "rxjs";
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable()
export class AuthGuard implements CanActivate{

    constructor(
        private router: Router,
        private jwtHelper: JwtHelperService
    ) {}
    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {
        const token = localStorage.getItem("PSWtoken")
        if(token!=null && !this.jwtHelper.isTokenExpired(token)){
            return true;
        }
        else{
            this.router.navigate(['login']);
            return false
        }
    }

    
}