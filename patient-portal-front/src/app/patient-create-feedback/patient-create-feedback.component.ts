import { Component, OnInit } from '@angular/core';
import { Feedback } from 'src/feedback';
import { PatientCreateFeedbackService } from '../patient-create-feedback.service';

@Component({
  selector: 'app-patient-create-feedback',
  templateUrl: './patient-create-feedback.component.html',
  styleUrls: ['./patient-create-feedback.component.css']
})
export class PatientCreateFeedbackComponent implements OnInit {
  content: string;
  isPublic: boolean;
  isAnonymous: boolean;

  constructor(private _patientCreateFeedbackService: PatientCreateFeedbackService) {
    this.content = "";
    this.isPublic = false;
    this.isAnonymous = false;
  }

  ngOnInit(): void {
  }

  createFeedback() {
    if(this.contentIsValid())
    {
      let feedback = new Feedback(this.content,this.isPublic,this.isAnonymous);
      this._patientCreateFeedbackService.addFeedback(feedback).subscribe();
    }
  }

  contentIsValid() {
    return this.content;
  }
}
