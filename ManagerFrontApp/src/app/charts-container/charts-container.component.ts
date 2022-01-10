import { Component, OnInit } from '@angular/core';
import { DoctorStat } from '../data/doctorStat';
import { DoctorStatsRequest } from '../data/doctorStatsRequest';
import { ChartsService } from './charts-container.service';
import {} from '@angular/core';
import { ChartComponent } from 'ng-apexcharts';

import {
  ApexNonAxisChartSeries,
  ApexResponsive,
  ApexChart,
} from 'ng-apexcharts';

export type ChartOptions = {
  series: ApexNonAxisChartSeries;
  chart: ApexChart;
  responsive: ApexResponsive[];
  labels: any;
};
@Component({
  selector: 'app-charts-container',
  templateUrl: './charts-container.component.html',
  styleUrls: [
    './charts-container.component.css',
    '../doctor-on-duty-container/doctor-on-duty-container.component.css',
  ],
})
export class ChartsContainerComponent implements OnInit {
  stats: DoctorStat[] = [];
  requestData: DoctorStatsRequest = {
    endDate: null,
    startDate: null,
  };
  showTable: boolean = false;
  public piedata: Object[] = [];
  public map: Object = 'fill';
  public datalabel: Object = {};
  public legendSettings: Object = {};
  selectedStats: DoctorStat = {} as DoctorStat;
  showChart = false;
  constructor(private chartsService: ChartsService) {}

  ngOnInit(): void {
    this.piedata = [
      { x: 'Patients', y: 10, text: 'Patients' },
      { x: 'Appointments', y: 20, text: 'Appointments' },
      { x: 'On-call shifts', y: 14, text: 'On-call shifts' },
      { x: 'Vacation days', y: 2, text: 'Vacation days' },
    ];
    this.datalabel = { visible: true, name: 'text', position: 'Outside' };
    this.legendSettings = {
      visible: false,
    };
  }

  selectStat = (stat: DoctorStat) => {
    this.selectedStats = stat;

    this.piedata = [
      { x: 'Patients', y: stat.numberOfPatients, text: 'Patients' },
      { x: 'Appointments', y: stat.numberOfAppointments, text: 'Appointments' },
      {
        x: 'On-call shifts',
        y: stat.numberOfOnCallShifts,
        text: 'On-call shifts',
      },
      {
        x: 'Vacation days',
        y: stat.numberOfVacationDays,
        text: 'Vacation days',
      },
    ];
    this.showChart = true;
  };

  apply = () => {
    if (this.requestData.endDate == null || this.requestData.endDate == null)
      return;
    this.chartsService.getStats(this.requestData).subscribe(
      (data) => {
        this.stats = data;
        this.showTable = true;
      },
      (error) => {
        console.log(error);
        this.showTable = true;
      }
    );
  };
}
