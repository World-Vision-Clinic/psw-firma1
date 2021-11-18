import { Component, OnInit } from '@angular/core';
import { Router, NavigationStart } from '@angular/router';
import { PatientFeedbackServiceService } from '../patient-feedback-service.service';
import { CommonModule } from "@angular/common";

@Component({
  selector: 'app-homepage',
  templateUrl: './homepage.component.html',
  styleUrls: ['./homepage.component.css']
})
export class HomePageComponent implements OnInit {
  menuIsOpen: boolean=true;
  public feedback = [] as any
  public errorMsg = ""

  constructor(private _patientService : PatientFeedbackServiceService, private router: Router) { }

  ngOnInit(): void {
    this._patientService.getFeedback().subscribe(data => this.feedback = data,
                                                error => this.errorMsg = "Couldn't load user feedback");
  }

}
