import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Feedback } from 'src/feedback';
import {catchError} from 'rxjs/operators'; 
import { map } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class PatientCreateFeedbackService {
  private _url: string = "api/Feedbacks";
  httpOptions = {
    headers: new HttpHeaders().set('Content-Type', 'application/json')
  };

  constructor(private http: HttpClient) { }

  addFeedback(feedback: Feedback): Observable<Feedback> {
    return this.http.post<Feedback>(this._url, JSON.stringify(feedback), this.httpOptions);
  }
}
