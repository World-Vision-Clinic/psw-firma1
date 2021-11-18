import { Component, OnInit } from '@angular/core';
import { Patient } from 'src/patient';
import { PatientFeedbackServiceService } from '../patient-feedback-service.service';

@Component({
  selector: 'app-medical-record-view',
  templateUrl: './medical-record-view.component.html',
  styleUrls: ['./medical-record-view.component.css']
})
export class MedicalRecordViewComponent implements OnInit {
  public daysOfTheWeek: { day: string, scheduleItems: { name: string, amount: string, time: string }[] }[] = [
    { "day": 'Monday', "scheduleItems": [

    ] },
    { "day": 'Tuesday', "scheduleItems": [
        { "name": 'Test1', "amount": '100mg', "time": '08:00' },
        { "name": 'Test2', "amount": '50mg', "time": '16:00' }
    ] },
    { "day": 'Wednesday', "scheduleItems": [

    ] },
    { "day": 'Thursday', "scheduleItems": [

    ] },
    { "day": 'Friday', "scheduleItems": [
      { "name": 'Test1', "amount": '150mg', "time": '12:00' },
    ] },
    { "day": 'Saturday', "scheduleItems": [

    ] },
    { "day": 'Sunday', "scheduleItems": [

    ] }
  ];
  public errorMsg = "";
  public patient: Patient = {} as Patient;

  public appointments: { date: string, name: string, doctor: string }[] = [
    { "date": '14-05-2021', "name": 'Test1', "doctor": 'Doktor Doktorić' },
    { "date": '11-05-2021', "name": 'Test2', "doctor": 'Marko Marković' },
    { "date": '09-05-2021', "name": 'Test3', "doctor": 'Petar Petrović' },
    { "date": '21-03-2021', "name": 'Test4', "doctor": 'Luka Luković' },
  ];
  public selectedDay: string = 'Monday';

  constructor(private _patientService : PatientFeedbackServiceService) { }

  ngOnInit(): void {
    this._patientService.getPatient(1).subscribe(data => (this.patient = data),
      error => this.errorMsg = "Couldn't load user feedback");
  }

  getFormattedDay(day: string): string {
    if(this.selectedDay == day)
    {
      return day
    }
    return day.substring(0,2)
  }

  getPatientGender(): string {
    if(this.patient.gender == 2)
    {
      return "Female";
    }
    return "Male";
  }

}
