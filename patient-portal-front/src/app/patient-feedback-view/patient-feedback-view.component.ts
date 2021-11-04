import { Component, OnInit } from '@angular/core';
import { PatientFeedbackServiceService } from '../patient-feedback-service.service';

@Component({
  selector: 'app-patient-feedback-view',
  template: `
        <h2>Hospital Feedback</h2>
        <div *ngFor = "let f of feedback">
          <hr>
          <div *ngIf="f.isPublic">
            {{f.content}}<br>
            <span *ngIf="f.isAnonymous; then thenBlock else elseBlock"></span>
            <ng-template #thenBlock>by Anonymous</ng-template>
            <ng-template #elseBlock>by User</ng-template>
          </div>
        </div> 
        <hr>  
  
  `,
  styleUrls: ['./patient-feedback-view.component.css']
})
export class PatientFeedbackViewComponent implements OnInit {

  public feedback = [] as any

  constructor(private _patientService : PatientFeedbackServiceService) { }

  ngOnInit(): void {
    this._patientService.getFeedback2().subscribe(data => this.feedback = data);
  }

}
