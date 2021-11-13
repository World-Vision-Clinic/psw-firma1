import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IFeedback } from './feedback';

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
}