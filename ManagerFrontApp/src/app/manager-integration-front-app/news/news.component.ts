import { Component, OnInit } from '@angular/core';
import {HttpClient} from '@angular/common/http'
import { Injectable } from '@angular/core';

@Component({
  selector: 'app-news',
  templateUrl: './news.component.html',
  styleUrls: ['./news.component.css']
})
@Injectable({providedIn:'root'})
export class NewsComponent implements OnInit {

  constructor(private http:HttpClient) { }

  NewsList:any =[];

  ngOnInit(): void {
    this.getNews();
  }

  getNews(){
    return this.http.get<any>("http://localhost:8083/News").subscribe(data=>{
      this.NewsList=data;
});
  }

  changeNewsView(i: any){
    this.http.put<any>('http://localhost:8083/News', this.NewsList[i])
        .subscribe(data => this.NewsList[i] = data);
  }
}
