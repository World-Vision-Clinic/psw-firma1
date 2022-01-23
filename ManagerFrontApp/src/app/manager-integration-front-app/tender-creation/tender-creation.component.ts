import { Component, OnInit } from '@angular/core';
import {HttpClient} from '@angular/common/http'
import { Injectable } from '@angular/core';
import { NotificationService } from 'src/app/notification.service';

@Component({
  selector: 'app-tender-creation',
  templateUrl: './tender-creation.component.html',
  styleUrls: ['./tender-creation.component.css']
})


@Injectable({providedIn:'root'})
export class TenderCreationComponent implements OnInit {

  constructor(private http:HttpClient, private notifyService: NotificationService) { }

  tenderTitle:string = '';
  tenderDescription:string = '';
  selectedDate:any;
  medicineName:string = '';
  dosage:string = '';
  quantity:string = '';
  medicineList:any[] = [];

  ngOnInit(): void {
    // this.tenderTitle="";
    // this.tenderDescription="";
    // this.medicineName="";
    // this.dosage="";
    // this.quantity="";
  }

  validateTender(): Boolean {
    if (this.tenderTitle==="" || this.tenderDescription==="" || this.medicineList.length === 0)
      return false;
    
    return true;
  }
  addMedicine(){
      // let obj = new Medicine();
      // obj.medicineName = "test1";
      // obj.quantity = "test2";
      // obj.dosage = "test3";
      // this.medicineList.push(obj);

    if(this.medicineName.length==0 || this.quantity.length==0 || this.dosage.length==0){
      return;
    }
    for (let i = 0; i < this.medicineList.length; i++) {
      if(this.medicineList[i].MedicineName===this.medicineName && this.medicineList[i].Dosage===this.dosage)
        return;
    }
    let obj = new TenderItem();
    obj.MedicineName = this.medicineName;
    obj.Quantity = this.quantity;
    obj.Dosage = this.dosage;
    this.medicineList.push(obj);

  }
  removeMedicine(med){
    for (let i = 0; i < this.medicineList.length; i++) {
      if(this.medicineList[i].MedicineName===med.MedicineName && this.medicineList[i].Dosage===med.Dosage &&
          this.medicineList[i].Quantity===med.Quantity)
      { 
        delete this.medicineList[i]
        return;
      }
        
    }
  }

  sendTender()
  {

    if(!this.validateTender()){
      this.notifyService.showError("Please fill all fields", "Error");
      return;
    }
      var val = {
          Title:this.tenderTitle,
          Description:this.tenderDescription,
          TenderItems:this.medicineList,
          EndTime:this.selectedDate,

        //  Content:this.objectionContent,
        //  PharmacyLocalhost:this.SelectedPharmacy.Localhost
       }
      const headers = { 'content-type': 'application/json'}  
      const body=JSON.stringify(val);
      alert("Sending tender information...");
      return this.http.post('http://localhost:43818/Tender', body,{'headers':headers}).subscribe(res => alert("Successfull."));
  }

}
//  TenderItemId: string='0';
class TenderItem{
  MedicineName: string='';
  Dosage: string='';
  Quantity: string='';
}