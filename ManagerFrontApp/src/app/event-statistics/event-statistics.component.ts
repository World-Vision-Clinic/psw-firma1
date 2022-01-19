import { Component, OnInit } from '@angular/core';
import { ManagerFeedbackService } from '../manager-feedback.service';
import { StatisticDataPair } from '../statistic-data-pair';

@Component({
  selector: 'app-event-statistics',
  templateUrl: './event-statistics.component.html',
  styleUrls: ['./event-statistics.component.css']
})
export class EventStatisticsComponent implements OnInit {
  public eventStatistics = [] as any
  public errorMsg = ""

  constructor(private managerService : ManagerFeedbackService) { }

  ngOnInit(): void {
    this.managerService.getEventStatistics().subscribe(data => this.eventStatistics = data,
      error => this.errorMsg = "Couldn't load event statistics");
  }

  getStatisticDataPercentage(dataPair: StatisticDataPair, allData: StatisticDataPair[]): number {
    let totalCount: number = 0;
    allData.forEach(element => {
      totalCount+=element.value;
    });
    if(totalCount == 0)
      return 0
    return (dataPair.value/totalCount)*100
  }
}
