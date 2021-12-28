import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { Injectable } from '@angular/core';
import { NotificationService } from 'src/app/notification.service';

@Component({
  selector: 'app-get-specification',
  templateUrl: './get-specification.component.html',
  styleUrls: ['./get-specification.component.css']
})

@Injectable({ providedIn: 'root' })
export class GetSpecificationComponent implements OnInit {

  constructor(private http: HttpClient, private notifyService: NotificationService) { }


  PharmacyList: any = [];
  SelectedPharmacy: any;
  isPharmacySelectedL: boolean = false;
  objectionContent: any;
  medicineName: string = '';

  barChartOptions : any = {
    scaleShowVerticalLines: false,
    responsive: true,
    plugins: {
      title: {
          display: true,
          text: 'Number of offers'
      }
    }
  };

  barChartOptionsMax : any = {
    scaleShowVerticalLines: false,
    responsive: true,
    plugins: {
      title: {
          display: true,
          text: 'Max prices in dollars'
      }
    }
  };

  barChartOptionsMin : any = {
    scaleShowVerticalLines: false,
    responsive: true,
    plugins: {
      title: {
          display: true,
          text: 'Min prices in dollars'
      }
    }
  };

  barChartLegend = true;

  barChartLabels = [];
  barChartData = [];

  barChartLabelsMax = [];
  barChartDataMax = [];

  barChartLabelsMin = [];
  barChartDataMin = [];

  ngOnInit(): void {
    this.getPharmacies();
    this.getChartDataForNumberOfOffers();
    this.getChartDataForMaxPrices();
    this.getChartDataForMinPrices();
  }

  selectChange($event: any) {

    //console.log($event);
    this.isPharmacySelectedL = true;
    this.SelectedPharmacy = $event
    //console.log(this.SelectedPharmacy);
  }

  getChartDataForNumberOfOffers() {
    return this.http.get<any>("http://localhost:43818/tender/offers/number").subscribe(data => {
      for (var c in data.data) {
        this.barChartData.push({
          label: c as never,
          data: data.data[c] as never
        } as never);
      }
      this.barChartLabels = data.tenders;
    });
  }

  getChartDataForMaxPrices() {
    return this.http.get<any>("http://localhost:43818/tender/prices/max").subscribe(data => {
      for (var c in data.data) {
        this.barChartDataMax.push({
          label: c as never,
          data: data.data[c] as never
        } as never);
      }
      this.barChartLabelsMax = data.tenders;
    });
  }

  getChartDataForMinPrices() {
    return this.http.get<any>("http://localhost:43818/tender/prices/min").subscribe(data => {
      for (var c in data.data) {
        this.barChartDataMin.push({
          label: c as never,
          data: data.data[c] as never
        } as never);
      }
      this.barChartLabelsMin = data.tenders;
    });
  }

  getPharmacies() {
    return this.http.get<any>("http://localhost:43818/Pharmacies").subscribe(data => {
      this.PharmacyList = data;
      console.log(this.PharmacyList);
    });
  }

  GetSpecification() {
    return this.http.get<any>('http://localhost:43818/medicines/spec?pharmacyLocalhost=' + this.SelectedPharmacy.Localhost
      + "&medicine=" + this.medicineName).subscribe(
        res => this.notifyService.showSuccess("Go to Downloads!", this.medicineName + " specification recieved"),
        error => this.notifyService.showError("From pharmacy \"" + this.SelectedPharmacy.Name + "\"", this.medicineName + " specification doesn't exist!")
      );
  }

}
