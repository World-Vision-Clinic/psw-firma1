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

  ObjectionList:any =[];
  RepliesForObjectionList:any=[];
  replies:boolean[] = [];
  responseDiv:boolean[] = [];
  buttons:any = [];
  index:any = 0;

  ngOnInit(): void {
    this.getObjections();
    this.getRepliesForObjections();
  }

  getObjections(){
    return this.http.get<any>("http://localhost:8083/Objections").subscribe(data=>{
      this.ObjectionList=data;
});
  }
  showResponse(i: any){
    if(this.responseDiv[i]){
      this.responseDiv[i] = false;
    }else{
      this.responseDiv[i] = true;
    }
  }
  getRepliesForObjections(){
    this.http.get<any>("http://localhost:8083/Objections").subscribe(data=>{
      for(let i=0; i<data.length; i++){
        this.http.get<any>("http://localhost:8083/Replies/GetObjectionReplies?objectionId=" + data[i].Id).subscribe(data1=>{
        if(data1.length != 0){
          this.replies[i] = true;
          this.RepliesForObjectionList[i] = data1;
        }else{
          this.replies[i] = false;
        }
      });
    }
    });
    
  }
}
