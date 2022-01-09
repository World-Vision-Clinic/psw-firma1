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

  closeTender(tender:any){
    let confirmAction = confirm("Are you sure you want to close tender?");
    if (confirmAction) {
     
    //   var val = {
    //     Title:this.tenderTitle,
    //     Description:this.tenderDescription,
    //     TenderItems:this.medicineList,
    //     EndTime:this.selectedDate,
    //  }
      const headers = { 'content-type': 'application/json'}  
      const body=JSON.stringify(tender);
      //alert("Sending tender information...");
      this.http.post('http://localhost:43818/Tender/closeTender', body,{'headers':headers}).subscribe(res => alert("Successfull."));

    } 
  }
  chooseTenderWinner(tender:any,tenderOffer:any)
  {
    let confirmAction = confirm("Are you sure you want to choose tender winner?");
    if (confirmAction) {
     
      const headers = { 'content-type': 'application/json'}
      tenderOffer.TenderHash = tender.TenderHash;  
      const body=JSON.stringify(tenderOffer);
      //alert("Sending tender information...");
      this.http.post('http://localhost:43818/Tender/chooseTenderWinner', body,{'headers':headers}).subscribe(res => alert("Successfull."));
    }
  }

}
