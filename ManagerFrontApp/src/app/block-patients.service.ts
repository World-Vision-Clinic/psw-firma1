import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IFeedback } from './feedback';
import { SurveyBreakdown } from './survey-breakdown';
import { IPatient } from './patient'

@Injectable({
  providedIn: 'root'
})
export class BlockPatientsService {

  constructor(private http: HttpClient) { }


  getMaliciousPatients() : Observable<IPatient[]>{
    return this.http.get<IPatient[]>("/api/Patients/malicious")
  }

  blockPatient(username) : Observable<IPatient>{
    console.log("Here");
    return this.http.get<IPatient>("/api/Patients/block/"+username);
  }
}