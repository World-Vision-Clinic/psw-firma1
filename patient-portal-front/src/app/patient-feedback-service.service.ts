import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IFeedback } from './feedback';

@Injectable({
  providedIn: 'root'
})
export class PatientFeedbackServiceService {

  private _url: string ="http://localhost:39901/api/Feedbacks"

  constructor(private http: HttpClient) { }


  getFeedback2() : Observable<IFeedback[]>{
    return this.http.get<IFeedback[]>(this._url)
  }
}
