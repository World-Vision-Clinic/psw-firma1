import { Component, OnInit } from '@angular/core';
import {HttpClient} from '@angular/common/http'
import { Injectable } from '@angular/core';
import { NotificationService } from 'src/app/notification.service';

@Component({
  selector: 'app-get-specification',
  templateUrl: './get-specification.component.html',
  styleUrls: ['./get-specification.component.css']
})

@Injectable({providedIn:'root'})
export class GetSpecificationComponent implements OnInit {

  constructor(private http:HttpClient, private notifyService : NotificationService) { }

  
  PharmacyList:any=[];
  SelectedPharmacy:any;
  isPharmacySelectedL:boolean = false;
  objectionContent:any;
  medicineName: string = '';

  barChartOptions = {
    scaleShowVerticalLines: false,
    responsive: true
  };
  barChartLabels = ['2006', '2007', '2008', '2009', '2010', '2011', '2012'];
  barChartLegend = true;
  barChartData = [
    {data: [65, 59, 80, 81, 56, 55, 40], label: 'Series A'},
    {data: [28, 48, 40, 19, 86, 27, 90], label: 'Series B'}
  ];

  barChartLabels1 = ['2006', '2007', '2008', '2009', '2010', '2011', '2012'];
  barChartLegend1 = true;
  barChartData1 = [
    {data: [65, 59, 80, 81, 56, 55, 40], label: 'Series A'},
    {data: [28, 48, 40, 19, 86, 27, 90], label: 'Series B'}
  ];

  ngOnInit(): void {
    this.getPharmacies();
  }

  selectChange($event:any) {

    //console.log($event);
    this.isPharmacySelectedL = true;
    this.SelectedPharmacy = $event
    //console.log(this.SelectedPharmacy);
  }

  getPharmacies(){
    return this.http.get<any>("http://localhost:43818/Pharmacies").subscribe(data=>{
          this.PharmacyList=data;
          console.log(this.PharmacyList);
    });
  }

  GetSpecification()
  {
    return this.http.get<any>('http://localhost:43818/medicines/spec?pharmacyLocalhost=' + this.SelectedPharmacy.Localhost
    + "&medicine=" + this.medicineName).subscribe(
      res => this.notifyService.showSuccess("Go to Downloads!", this.medicineName + " specification recieved"),
      error => this.notifyService.showError("From pharmacy \"" + this.SelectedPharmacy.Name + "\"", this.medicineName + " specification doesn't exist!")
    );
  }

}
