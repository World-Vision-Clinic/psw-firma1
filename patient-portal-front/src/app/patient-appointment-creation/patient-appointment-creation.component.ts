import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AppointmentRequest } from 'src/appointment-request';
import { AppointmentCreationService } from '../appointment-creation.service';

@Component({
  selector: 'app-patient-appointment-creation',
  templateUrl: './patient-appointment-creation.component.html',
  styleUrls: ['./patient-appointment-creation.component.css']
})
export class PatientAppointmentCreationComponent implements OnInit {

  errorMsg: string = "";

  public appointmentRequest: AppointmentRequest = new AppointmentRequest();

  public availableTimes: string[] = [
    "10:00:00",
    "11:00:00",
    "12:00:00",
    "13:00:00",
    "14:00:00",
  ]
  public doctors = [] as any;
  public appointments = [] as any;

  constructor(private router: Router, private _appointmentCreationService: AppointmentCreationService) { }


  ngOnInit(): void {
    this._appointmentCreationService.getDoctors().subscribe(data => {this.doctors = data},
                                                error => this.errorMsg = "Couldn't load doctors");
  }

  send_request() {
    this._appointmentCreationService.requestAppointmentRecommendationDoctorPriority(this.appointmentRequest).subscribe(data => this.appointments = data);
  }

}
