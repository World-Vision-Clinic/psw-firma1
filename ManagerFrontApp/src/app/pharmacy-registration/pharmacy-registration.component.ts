import { Component, OnInit } from '@angular/core';
import {HttpClient} from '@angular/common/http'
import { Injectable } from '@angular/core';
import {Observable} from 'rxjs';
 
@Component({
  selector: 'app-pharmacy-registration',
  templateUrl: './pharmacy-registration.component.html',
  styleUrls: ['./pharmacy-registration.component.css']
})

@Injectable({providedIn:'root'})
export class PharmacyRegistrationComponent implements OnInit {

  name: string = '';
  localhost: string = '';

  constructor(private http:HttpClient) { }
    ngOnInit(): void {
  }

  addPharmacy() {
    var val = {Name:this.name,
               Localhost:this.localhost}
               const headers = { 'content-type': 'application/json'}  
    const body=JSON.stringify(val);
    console.log(body)
    return this.http.post('http://localhost:43818/pharmacies/registerPharmacy', body,{'headers':headers}).subscribe();
    //return this.http.post('http://localhost:43818/pharmacies/registerPharmacy',val);
  }

}