import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IFeedback } from './feedback';

@Injectable({
  providedIn: 'root'
})
export class PatientFeedbackServiceService {


  constructor(private http: HttpClient) { }


  getFeedback() : Observable<IFeedback[]>{
    return this.http.get<IFeedback[]>("/api/Feedbacks/published")
  }
}
