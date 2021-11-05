import { Component, OnInit } from '@angular/core';
import {HttpClient} from '@angular/common/http'
import { Injectable } from '@angular/core';

@Component({
  selector: 'app-overview-objections-replies',
  templateUrl: './overview-objections-replies.component.html',
  styleUrls: ['./overview-objections-replies.component.css']
})

@Injectable({providedIn:'root'})
export class OverviewObjectionsRepliesComponent implements OnInit {

  constructor(private http:HttpClient) { }

  ObjectionList:any=[];

  ngOnInit(): void {
    this.getObjections();
  }

  getObjections(){
    return this.http.get<any>("http://localhost:43818/Objections").subscribe(data=>{
          this.ObjectionList=data;
    });
  }

}
