import { Component, OnInit } from '@angular/core';
import { Feedback } from 'src/feedback';
import { PatientCreateFeedbackService } from '../patient-create-feedback.service';

@Component({
  selector: 'app-patient-create-feedback',
  templateUrl: './patient-create-feedback.component.html',
  styleUrls: ['./patient-create-feedback.component.css']
})
export class PatientCreateFeedbackComponent implements OnInit {

  constructor(private _patientCreateFeedbackService: PatientCreateFeedbackService) { }

  ngOnInit(): void {
  }

  createFeedback() {
    this._patientCreateFeedbackService.addFeedback(new Feedback(667,"aaa",false,false)).subscribe();
  }
}
