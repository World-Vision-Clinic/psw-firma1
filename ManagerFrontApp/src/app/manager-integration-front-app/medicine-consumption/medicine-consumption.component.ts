import { Component, OnInit } from '@angular/core';
import {FormGroup, FormControl} from '@angular/forms';
import {HttpClient} from '@angular/common/http'
import { Injectable } from '@angular/core';



@Component({
  selector: 'app-medicine-consumption',
  templateUrl: './medicine-consumption.component.html',
  styleUrls: ['./medicine-consumption.component.css']
})
@Injectable({providedIn:'root'})
export class MedicineConsumptionComponent implements OnInit {

  StartDate:any;
  EndDate:Date;
  
  constructor(private http:HttpClient) {
    //this.datepipe = new DatePipe('mm/dd/yyyy');
      this.StartDate = new Date();
      //this.StartDate = this.datepipe.transform(new Date(), 'mm/dd/yyyy');
      this.EndDate = new Date();

   }

  ngOnInit(): void {
  }

  validateDate(): Boolean {
    if(this.StartDate==null || this.EndDate==null)
    {
      alert("Dates must be selected");
      return false;
    }
    if(this.EndDate<=this.StartDate)
    {
      alert("End date must be after starting date");
      return false;
    }
    
    return true;
  }

  sendObjection()
  {
    if(!this.validateDate())
    {
      return;
      
    }

      var val = {
        Beginning:this.StartDate,
        End:this.EndDate
      }
      const headers = { 'content-type': 'application/json'}  
      const body=JSON.stringify(val);
      //alert("Request sent... Please wait...");
      return this.http.post('http://localhost:8083/medicines/sendConsumptionNotification', body,{'headers':headers}).subscribe(res => alert("Successfull send notification to pharmacies."));
  }

}
