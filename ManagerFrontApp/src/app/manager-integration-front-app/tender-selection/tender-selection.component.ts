import { Component, OnInit } from '@angular/core';
import {HttpClient} from '@angular/common/http'
import { Injectable } from '@angular/core';

@Component({
  selector: 'app-tender-selection',
  templateUrl: './tender-selection.component.html',
  styleUrls: ['./tender-selection.component.css']
})

@Injectable({providedIn:'root'})
export class TenderSelectionComponent implements OnInit {

  constructor(private http:HttpClient) { }

  TenderList:any=[];

  ngOnInit(): void {
    this.getTenders();
  }

  getTenders(){
    return this.http.get<any>("http://localhost:43818/Tender").subscribe(data=>{
          this.TenderList=data;
          console.log(this.TenderList);
    });
  }

}
