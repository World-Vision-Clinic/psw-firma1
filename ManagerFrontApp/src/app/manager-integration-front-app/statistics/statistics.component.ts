import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { Injectable } from '@angular/core';
import { NotificationService } from 'src/app/notification.service';

@Component({
  selector: 'app-statistics',
  templateUrl: './statistics.component.html',
  styleUrls: ['./statistics.component.css']
})

@Injectable({ providedIn: 'root' })
export class StatisticsComponent implements OnInit {

  constructor(private http: HttpClient, private notifyService: NotificationService) { }

  buttonClicked: boolean = false;
  startDate: any = undefined;
  endDate: any = undefined;

  barChartOptions: any = {
    scaleShowVerticalLines: false,
    responsive: true,
    plugins: {
      title: {
        display: true,
        text: 'Number of offers'
      }
    }
  };

  barChartOptionsMax: any = {
    scaleShowVerticalLines: false,
    responsive: true,
    plugins: {
      title: {
        display: true,
        text: 'Max prices in dollars'
      }
    }
  };

  barChartOptionsMin: any = {
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
  }

  getChartDataForNumberOfOffers() {
    this.barChartData = []
    return this.http.get<any>("http://localhost:43818/tender/offers/number?start=" + this.startDate + "&end=" + this.endDate).subscribe(data => {
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
    this.barChartDataMax = []
    return this.http.get<any>("http://localhost:43818/tender/prices/max?start=" + this.startDate + "&end=" + this.endDate).subscribe(data => {
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
    this.barChartDataMin = []
    return this.http.get<any>("http://localhost:43818/tender/prices/min?start=" + this.startDate + "&end=" + this.endDate).subscribe(data => {
      for (var c in data.data) {
        this.barChartDataMin.push({
          label: c as never,
          data: data.data[c] as never
        } as never);
      }
      this.barChartLabelsMin = data.tenders;
    });
  }

  validateDate(): Boolean {
    if (this.startDate == undefined || this.endDate == undefined) {
      alert("Dates must be selected");
      return false;
    }
    if (this.endDate <= this.startDate) {
      alert("End date must be after starting date");
      return false;
    }

    return true;
  }


  generateStatistics() {
    if (!this.validateDate())
      return;

    this.buttonClicked = true;
    this.getChartDataForNumberOfOffers();
    this.getChartDataForMaxPrices();
    this.getChartDataForMinPrices();
    return this.http.get(`http://localhost:43818/tender/report?start=` + this.startDate + "&end=" + this.endDate, {responseType: 'blob'})
    .subscribe((result: Blob) => {
      const blob = new Blob([result], { type:'application/pdf' }); // you can change the type
      const url= window.URL.createObjectURL(blob);
      window.open(url, 'title');
  });
  }

}
