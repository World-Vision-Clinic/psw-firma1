import { Component, OnInit } from '@angular/core';
import {HttpClient} from '@angular/common/http'

@Component({
  selector: 'app-pharmacies',
  templateUrl: './pharmacies.component.html',
  styleUrls: ['./pharmacies.component.css']
})
export class PharmaciesComponent implements OnInit {

  PharmaciesList:any =[];

  constructor(private http:HttpClient) { }

  ngOnInit(): void {
    this.getPharmacies();
  }

  getPharmacies(){
    return this.http.get<any>("http://localhost:43818/Pharmacies").subscribe(data=>{
      this.PharmaciesList=data;
   });
  }
}
