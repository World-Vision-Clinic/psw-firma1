import { Component, OnInit } from '@angular/core';
import { PatientFeedbackServiceService } from '../patient-feedback-service.service';

@Component({
  selector: 'app-patient-feedback-view',
  template: `
        <h2>Hospital Feedback</h2>
        <span *ngIf="errorMsg; then printError else printFeedback"></span>
        <ng-template #printError>{{errorMsg}}</ng-template>
        <ng-template #printFeedback>
          <div *ngFor = "let f of feedback">
            <hr>
            {{f.content}}<br>
            <span *ngIf="f.isAnonymous; then thenBlock else elseBlock"></span>
            <ng-template #thenBlock>by Anonymous</ng-template>
            <ng-template #elseBlock>by User</ng-template>
          </div> 
          <hr>  
        </ng-template>

  
  `,
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
