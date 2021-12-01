import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Appointment } from 'src/appointment';
import { AppointmentRequest } from 'src/appointment-request';
import { Doctor } from 'src/doctor';

@Injectable({
  providedIn: 'root'
})
export class AppointmentCreationService {
  httpOptions = {
    headers: new HttpHeaders().set('Content-Type', 'application/json')
  };

  constructor(private http: HttpClient) { }

  requestAppointmentRecommendationDoctorPriority(appointmentRequest: AppointmentRequest): Observable<AppointmentRequest> {
    return this.http.post<AppointmentRequest>("/api/Appointments/recommendation_doctor", JSON.stringify(appointmentRequest), this.httpOptions);
  }

  getDoctors() : Observable<Doctor[]>{
    return this.http.get<Doctor[]>("/api/Doctors")
  }

  getPatientAppointments(id: number) : Observable<Appointment[]>{
    return this.http.get<Appointment[]>("/api/Appointments/patient/1")
  }
}
