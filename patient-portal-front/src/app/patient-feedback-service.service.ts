import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Appointment } from 'src/appointment';
import { Feedback } from 'src/feedback';
import { MedicalRecord } from 'src/medical-record';

@Injectable({
  providedIn: 'root'
})
export class PatientFeedbackServiceService {


  constructor(private http: HttpClient) { }


  getFeedback() : Observable<Feedback[]>{
    return this.http.get<Feedback[]>("/api/Feedbacks/published")
  }

  getPatient(id: number) : Observable<MedicalRecord>{
    return this.http.get<MedicalRecord>("/api/Patients")
  }

  getPatientAppointments(id: number) : Observable<Appointment[]>{
    return this.http.get<Appointment[]>("/api/Appointment/patient")
  }

  cancelAppointment(id: number) : Observable<Appointment>{
    return this.http.delete<Appointment>("/api/Appointment/" + id);
  }
}
