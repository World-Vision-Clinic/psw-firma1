import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AppointmentCreationService } from '../appointment-creation.service';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';

@Component({
  selector: 'app-appointment4step',
  templateUrl: './appointment4step.component.html',
  styleUrls: ['./appointment4step.component.css']
})
export class Appointment4stepComponent implements OnInit {

  firstFormGroup!: FormGroup;
  secondFormGroup!: FormGroup;
  thirdFormGroup!: FormGroup;
  fourthFormGroup!: FormGroup;

  public doctors = [] as any;
  public appointments = [] as any;
  public specialty = "";
  public errorMsg = "";
  public selectedParameters = {
    date : "",
    doctorId : ""
  }

  constructor(private router: Router, private _appointmentCreationService: AppointmentCreationService,private _formBuilder: FormBuilder) { }

  ngOnInit(): void {
    this.firstFormGroup = this._formBuilder.group({
      firstCtrl: ['', Validators.required],
    });
    this.secondFormGroup = this._formBuilder.group({
      secondCtrl: ['', Validators.required],
    });
    this.thirdFormGroup = this._formBuilder.group({
      thirdCtrl: ['', Validators.required],
    });
    this.fourthFormGroup = this._formBuilder.group({
      fourthCtrl: ['', Validators.required],
    });

  }
  printInConsole() {
    console.log(this.selectedParameters.date);
    console.log(this.specialty);
  }
  selectDoctor(d: any) {
    this.selectedParameters.doctorId = d.id
  }

  getDoctors() {
    this._appointmentCreationService.getDoctorsForSpecialty(this.specialty).subscribe(data => this.doctors = data,
      error => this.errorMsg = "Couldn't load doctors", 
      () => console.log(this.doctors)); 

  }

  getAppointments() {
    this._appointmentCreationService.getAppointments4Step(this.selectedParameters.doctorId,this.selectedParameters.date).subscribe(data => this.appointments = data,
      error => this.errorMsg = "Couldn't load appointments", 
      () => console.log(this.appointments));

  }

}
