import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from 'src/user';
import { Doctor } from 'src/doctor';
import { Allergen } from 'src/allergen';
import {catchError} from 'rxjs/operators'; 
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class RegisterService {
  private _url: string = "api/Patients/register";
  httpOptions = {
    headers: new HttpHeaders().set('Content-Type', 'application/json')
  };

  constructor(private http: HttpClient) { }

  register(user: User): Observable<User> {
    return this.http.post<User>(this._url, JSON.stringify(user), this.httpOptions);
  }

  getDoctors() : Observable<Doctor[]>{
    return this.http.get<Doctor[]>("/api/Doctors/available")
  }

  getAllergens() : Observable<Allergen[]>{
    return this.http.get<Allergen[]>("/api/Allergens")
  }
}