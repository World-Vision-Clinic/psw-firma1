import { Component, OnInit, Output } from '@angular/core';
import {HttpClient} from '@angular/common/http'
import { Injectable } from '@angular/core';

@Component({
  selector: 'app-objection-form-page',
  templateUrl: './objection-form-page.component.html',
  styleUrls: ['./objection-form-page.component.css']
})

@Injectable({providedIn:'root'})
export class ObjectionFormPageComponent implements OnInit {

  constructor(private http:HttpClient) { }

  
  PharmacyList:any=[];
  SelectedPharmacy:any;
  isPharmacySelectedL:boolean = false;
  objectionContent:any;

  ngOnInit(): void {
    this.getPharmacies();
  }

  selectChange($event:any) {

    //console.log($event);
    this.isPharmacySelectedL = true;
    this.SelectedPharmacy = $event
    console.log(this.SelectedPharmacy);
  }

  getPharmacies(){
    return this.http.get<any>("http://localhost:43818/Pharmacies").subscribe(data=>{
          this.PharmacyList=data;
          console.log(this.PharmacyList);
    });
  }

  sendObjection()
  {
      var val = {
        Content:this.objectionContent,
        PharmacyLocalhost:this.SelectedPharmacy.Localhost
      }
      const headers = { 'content-type': 'application/json'}  
      const body=JSON.stringify(val);
      //alert("Request sent... Please wait...");
      return this.http.post('http://localhost:43818/objections', body,{'headers':headers}).subscribe(res => {alert("Successfull ")},error =>{alert(error.error)});
  }

}
