import { Component, OnInit } from '@angular/core';
import {HttpClient} from '@angular/common/http'
import { Injectable } from '@angular/core';
import { NotificationService } from 'src/app/notification.service';


@Component({
  selector: 'app-register-pharmacy',
  templateUrl: './register-pharmacy.component.html',
  styleUrls: ['./register-pharmacy.component.css']
})
@Injectable({providedIn:'root'})
export class RegisterPharmacyComponent implements OnInit {

  name: string = '';
  localhost: string = '';
  address: string = '';
  city: string = '';
  protocol: string = '';
  email: string = '';

  constructor(private http:HttpClient, private notifyService: NotificationService) { }
    ngOnInit(): void {
  }

  addPharmacy() {
    var val = {Name:this.name,
               Localhost:this.localhost,
              Address:this.address,
              City:this.city,
              Protocol:this.protocol,
              Email: this.email}
               const headers = { 'content-type': 'application/json'}  
    const body=JSON.stringify(val);
    if(this.protocol == '') {
      this.notifyService.showError("Please select protocol", "Error")
      return;
    }
    alert("Request sent... Please wait...");
    return this.http.post('http://localhost:43818/pharmacies/registerPharmacy', body,{'headers':headers}).subscribe(res => this.notifyService.showSuccess("Successful registration", "Success"),
    error => this.notifyService.showError(error.error, "Error"))
    
  }

}
