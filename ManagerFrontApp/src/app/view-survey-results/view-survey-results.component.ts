import { Component, OnInit } from '@angular/core';
import { ManagerFeedbackService } from '../manager-feedback.service';

@Component({
  selector: 'app-view-survey-results',
  templateUrl: './view-survey-results.component.html',
  styleUrls: ['./view-survey-results.component.css']
})
export class ViewSurveyResultsComponent implements OnInit {
  public surveyBreakdowns = [] as any
  public errorMsg = ""

  constructor(private managerService : ManagerFeedbackService) { }

  ngOnInit(): void {
    this.managerService.getSurveyBreakdown().subscribe(data => this.surveyBreakdowns = data,
      error => this.errorMsg = "Couldn't load survey breakdown");
  }

  getReducedPrecision(numberToRound: number): number {
    return Math.round(numberToRound*100)/100;
  }
}
