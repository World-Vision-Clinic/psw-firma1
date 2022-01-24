import { Component, OnInit, Type } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PatientFeedbackServiceService } from '../patient-feedback-service.service';

@Component({
  selector: 'app-report',
  templateUrl: './report.component.html',
  styleUrls: ['./report.component.css']
})
export class ReportComponent implements OnInit {

  id: any;
  date: any;
  doctor: any;
  errorMsg = "";
  report = [] as any

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private _patientService : PatientFeedbackServiceService
  ) { 
    this.route.queryParams
    .subscribe(params => {
      this.id = params.id;
      this.date = new Date(params.date);
      this.doctor = params.doctor;
    });
    
  }
  
  ngOnInit(): void {
    this._patientService.getReport(this.id).subscribe(data => (this.report = data),
    error => this.errorMsg = "Couldn't load");
  }

}
