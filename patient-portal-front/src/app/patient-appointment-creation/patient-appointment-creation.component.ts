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
    "08:00:00",
    "08:30:00",
    "09:00:00",
    "09:30:00",
    "10:00:00",
    "10:30:00",
    "11:00:00",
    "11:30:00",
    "12:00:00",
    "12:30:00",
    "13:00:00",
    "13:30:00",
    "14:00:00",
    "14:30:00",
    "15:00:00",
    "15:30:00",
    "16:00:00",
    "16:30:00",
    "17:00:00",
    "17:30:00",
    "18:00:00",
    "18:30:00",
    "19:00:00",
    "19:30:00"
  ]
  public doctors = [] as any;
  public appointments = [] as any;
  public priority: string = "Doctor";

  constructor(private router: Router, private _appointmentCreationService: AppointmentCreationService) { }


  ngOnInit(): void {
    this._appointmentCreationService.getDoctors().subscribe(data => {this.doctors = data},
                                                error => this.errorMsg = "Couldn't load doctors");
  }

  send_request() {
    this._appointmentCreationService.requestAppointmentRecommendationDoctorPriority(this.appointmentRequest).subscribe(data => this.appointments = data);
  }

  getTermEnd(dateString: string) {
    let date = new Date(dateString);
    return date.setMinutes(date.getMinutes() + 30)
  }

  firstTimeIsGreaterThanSecondTime(firstTime: string, secondTime: string): boolean {
    if(firstTime === undefined || secondTime === undefined)
    {
      return true
    }
    var firstTimeHoursUnconverted = firstTime.split(':',2)[0]
    var firstTimeHours: number = +firstTimeHoursUnconverted
    var secondTimeHoursUnconverted = secondTime.split(':',2)[0]
    var secondTimeHours: number = +secondTimeHoursUnconverted

    var firstTimeMinutesUnconverted = firstTime.split(':',2)[1]
    var firstTimeMinutes: number = +firstTimeMinutesUnconverted
    var secondTimeMinutesUnconverted = secondTime.split(':',2)[1]
    var secondTimeMinutes: number = +secondTimeMinutesUnconverted

    var firstTimeTotal = 60*firstTimeHours + firstTimeMinutes
    var secondTimeTotal = 60*secondTimeHours + secondTimeMinutes
    if(firstTimeTotal > secondTimeTotal)
    {
      return true
    }
    return false
  }

  availableLowerTimes(): string[] {
    let result: string[] = []
    this.availableTimes.forEach(element => {
      if(!this.firstTimeIsGreaterThanSecondTime(element, this.appointmentRequest.UpperTimeRange))
      {
        result.push(element)
      }
    });
    return result
  }

  availableUpperTimes(): string[] {
    let result: string[] = []
    this.availableTimes.forEach(element => {
      if(this.firstTimeIsGreaterThanSecondTime(element, this.appointmentRequest.LowerTimeRange))
      {
        result.push(element)
      }
    });
    return result
  }
}
