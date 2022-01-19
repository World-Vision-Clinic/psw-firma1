import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { EventStatistic } from './event-statistic';
import { IFeedback } from './feedback';
import { SurveyBreakdown } from './survey-breakdown';

@Injectable({
  providedIn: 'root'
})
export class ManagerFeedbackService {

  constructor(private http: HttpClient) { }


  getFeedback() : Observable<IFeedback[]>{
    return this.http.get<IFeedback[]>("/api/Feedbacks")
  }

  publishFeedback(feedback) : Observable<IFeedback>{
    console.log("Here");
    feedback.isPublic = true;
    return this.http.put<IFeedback>("/api/Feedbacks/"+feedback.id, feedback); //TODO Errorcheck
  }

  unpublishFeedback(feedback) : Observable<IFeedback>{
    console.log("Here");
    feedback.isPublic = false;
    return this.http.put<IFeedback>("/api/Feedbacks/"+feedback.id, feedback); //TODO Errorcheck
  }

  getSurveyBreakdown() : Observable<SurveyBreakdown[]>{
    return this.http.get<SurveyBreakdown[]>("/api/Survey/answered_questions_breakdown")
  }

  getEventStatistics() : Observable<EventStatistic[]>{ 
    return this.http.get<EventStatistic[]>("/api/Event/statistics")
  }
}