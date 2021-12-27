import { Component, OnInit } from '@angular/core';
import { formatDate } from '@angular/common';
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

  currentDate: Date
  minDate = ""
  maxDate =""

  constructor(private router: Router, private _appointmentCreationService: AppointmentCreationService) {
    this.currentDate = new Date()
    let tmpDateVariable = this.currentDate
    tmpDateVariable.setDate(this.currentDate.getDate() + 2)
    this.minDate = formatDate(tmpDateVariable, 'YYYY-MM-dd', 'en');
    tmpDateVariable.setFullYear(this.currentDate.getFullYear() + 1)
    this.maxDate = formatDate(tmpDateVariable, 'YYYY-MM-dd', 'en');
  }


  ngOnInit(): void {
    this._appointmentCreationService.getDoctors().subscribe(data => {this.doctors = data},
                                                error => this.errorMsg = "Couldn't load doctors");
  }

  send_request() {
    this._appointmentCreationService.requestAppointmentRecommendationDoctorPriority(this.appointmentRequest).subscribe(data => this.appointments = data);
  }

  createAppointment(appointment: any) {
    appointment.patientForeignKey = 0  //1
    this._appointmentCreationService.createAppointment(appointment).subscribe();
    this.router.navigate(['/medical-record']);
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

  getDoctorName(doctorId: number): string {
    for(let d of this.doctors)
    {
      if(d.id === doctorId)
      {
        return d.firstName + " " + d.lastName;
      }
    }
    return ""
  }

  getFormattedLowerDateRange(): string {
    if(this.appointmentRequest.LowerDateRange === undefined)
    {
      let tmpDateVariable = this.currentDate
      tmpDateVariable.setDate(this.currentDate.getDate() + 2)
      return formatDate(tmpDateVariable, 'YYYY-MM-dd', 'en');
    }
    return formatDate(this.appointmentRequest.LowerDateRange, 'YYYY-MM-dd', 'en');
  }

  getFormattedUpperDateRange(): string {
    if(this.appointmentRequest.UpperDateRange === undefined)
    {
      let tmpDateVariable = this.currentDate
      tmpDateVariable.setFullYear(this.currentDate.getFullYear() + 1)
      return formatDate(tmpDateVariable, 'YYYY-MM-dd', 'en');
    }
    return formatDate(this.appointmentRequest.UpperDateRange, 'YYYY-MM-dd', 'en');
  }

  contentIsValid(): boolean {
    if(this.appointmentRequest.LowerDateRange === undefined || this.appointmentRequest.UpperDateRange === undefined)
    {
      return false
    }
    if(this.appointmentRequest.LowerDateRange > this.appointmentRequest.UpperDateRange)
    {
      return false
    }
    if(this.appointmentRequest.LowerDateRange <= this.currentDate || this.appointmentRequest.UpperDateRange <= this.currentDate)
    {
      return false
    }
    if(this.firstTimeIsGreaterThanSecondTime(this.appointmentRequest.LowerTimeRange, this.appointmentRequest.UpperTimeRange))
    {
      return false
    }
    return true
  }
}
