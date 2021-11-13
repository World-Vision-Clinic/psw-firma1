import { Component, OnInit } from '@angular/core';
import { Feedback } from 'src/feedback';
import { PatientCreateFeedbackService } from '../patient-create-feedback.service';
import {
  AUTO_STYLE,
  animate,
  state,
  style,
  transition,
  trigger
} from '@angular/animations';
import { Router } from '@angular/router';

const DEFAULT_DURATION = 300;

@Component({
  selector: 'app-patient-create-feedback',
  templateUrl: './patient-create-feedback.component.html',
  styleUrls: ['./patient-create-feedback.component.css'],
  animations: [
    trigger('collapse', [
      state('false', style({ height: AUTO_STYLE, visibility: AUTO_STYLE })),
      state('true', style({ height: '0', visibility: 'hidden', padding: '0' })),
      transition('false => true', animate(DEFAULT_DURATION + 'ms ease-in')),
      transition('true => false', animate(DEFAULT_DURATION + 'ms ease-out'))
    ]),
    trigger('colorState', [
      state('true', style({ background: 'linear-gradient(to right, rgba(79, 172, 254, 1), #08d)'})),
      state('false', style({ background: 'linear-gradient(to right, rgba(67, 233, 123, 1), rgba(56, 249, 215, 1))'}))
    ]),
    trigger('borderRoundingState', [
      state('true', style({ 'border-radius': '20px 20px 0px 0px'})),
      state('false', style({ 'border-radius': '20px 20px 20px 20px'})),
      transition('false => true', animate(DEFAULT_DURATION + 'ms ease-in')),
      transition('true => false', animate(DEFAULT_DURATION + 'ms ease-out'))
    ])
  ]
})
export class PatientCreateFeedbackComponent implements OnInit {
  content: string = "";
  isPublic: boolean = true;
  isAnonymous: boolean = false;
  UserName: string = "Default User";
  feedbackSent: boolean = false;
  errorMsg: string = "";

  constructor(private router: Router, private _patientCreateFeedbackService: PatientCreateFeedbackService) { }

  ngOnInit(): void { }

  createFeedback() {
    if(this.contentIsValid())
    {
      let feedback = new Feedback(this.content,this.isPublic,this.isAnonymous, this.UserName);
      this.feedbackSent = true;
      this._patientCreateFeedbackService.addFeedback(feedback).subscribe(
        success => setTimeout(() => {
          this.router.navigate(['view-feedback']);
      }, 800));
    }
  }

  contentIsValid() {
    return this.content;
  }
}
