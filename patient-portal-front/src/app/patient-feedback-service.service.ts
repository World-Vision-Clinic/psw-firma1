import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Feedback } from 'src/feedback';

@Injectable({
  providedIn: 'root'
})
export class PatientFeedbackServiceService {


  constructor(private http: HttpClient) { }


  getFeedback() : Observable<Feedback[]>{
    return this.http.get<Feedback[]>("/api/Feedbacks/published")
  }
}
