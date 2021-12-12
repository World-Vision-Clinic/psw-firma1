import { Component, OnInit } from '@angular/core';
import { Appointment } from 'src/appointment';
import { MedicalRecord } from 'src/medical-record';
import { Router } from '@angular/router';
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
  public patient: MedicalRecord = {} as MedicalRecord;

  public appointments = [] as any;
  public selectedDay: string = 'Monday';

  constructor(private router: Router, private _patientService : PatientFeedbackServiceService) { }

  ngOnInit(): void {
    this._patientService.getPatient(1).subscribe(data => (this.patient = data),
      error => this.errorMsg = "Couldn't load user feedback");
    
    this._patientService.getPatientAppointments(1).subscribe(data => this.appointments = data, error => this.errorMsg = "Couldn't load user appointments");
  }

  getSortedAppointments(): any[] {
    return this.appointments.sort((a: any, b: any) => Date.parse(b.date) - Date.parse(a.date))
  }

  getFormattedDay(day: string): string {
    if(this.selectedDay == day)
    {
      return day
    }
    return day.substring(0,2)
  }

  getAppointmentType(appointmentType: number): string {
    if(appointmentType == 1)
    {
      return "Operation";
    }
    if(appointmentType == 2)
    {
      return "Intervention";
    }
    return "Appointment";
  }

  getAppointmentStatus(appointment: Appointment): string {
    if(appointment.isCancelled)
      return "Cancelled";
    else if(appointment.isUpcoming)
      return "Upcoming";
    return "Finished"
  }

  isCancelValid(appointment: Appointment): boolean{
    if(appointment.isCancelled === true || appointment.isUpcoming === false)
      return false;
    return true;
  }

  canSurveyBeDone(appointment: Appointment): boolean {
    if(appointment.isCancelled === true || appointment.isUpcoming === true || appointment.hasCompletedSurvey === true)
      return false;
    return true;
  }

  doSurveyForAppointment(appointmentId: number) {
    this.router.navigate(['/survey/' + appointmentId]);
  }

  cancelAppointment(appointmentId: number) {
    this._patientService.cancelAppointment(appointmentId).subscribe();
    window.location.reload();
  }
}
