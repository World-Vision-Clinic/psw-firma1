import { Component, OnInit } from '@angular/core';
import { PatientFeedbackServiceService } from '../patient-feedback-service.service';

@Component({
  selector: 'app-patient-feedback-view',
  templateUrl: './patient-feedback-view.component.html',
  styleUrls: ['./patient-feedback-view.component.css']
})
export class PatientFeedbackViewComponent implements OnInit {

  public feedback = [] as any
  public errorMsg = ""

  constructor(private _patientService : PatientFeedbackServiceService) { }

  ngOnInit(): void {
    this._patientService.getFeedback().subscribe(data => this.feedback = data,
                                                error => this.errorMsg = "Couldn't load user feedback");
  }

}
