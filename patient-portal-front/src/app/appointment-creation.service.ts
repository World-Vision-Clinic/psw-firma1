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
    return this.http.post<AppointmentRequest>("/api/Appointment/recommendation_doctor", JSON.stringify(appointmentRequest), this.httpOptions);
  }

  createAppointment(appointmentRequest: AppointmentRequest): Observable<AppointmentRequest> {
    return this.http.post<AppointmentRequest>("/api/Appointment/add_appointment", JSON.stringify(appointmentRequest), this.httpOptions);
  }

  makeAppointment(appointment: any): Observable<any> {
    return this.http.post<any>("/api/Appointment/add_appointment", JSON.stringify(appointment), this.httpOptions);
  }

  getDoctors() : Observable<Doctor[]>{
    return this.http.get<Doctor[]>("/api/Doctors")
  }

  getDoctorsForSpecialty(specialty: any) : Observable<Doctor[]>{
    return this.http.get<Doctor[]>("/api/Doctors/get_by_specialty/" + specialty)
  }

  getAppointments4Step(doctorId: any,date: any) : Observable<Appointment[]>{
    return this.http.get<Appointment[]>("/api/Appointment/4step/"+ doctorId + "/" + date)
  }

  getPatientAppointments(id: number) : Observable<Appointment[]>{
    return this.http.get<Appointment[]>("/api/Appointment/patient/1")
  }


}
