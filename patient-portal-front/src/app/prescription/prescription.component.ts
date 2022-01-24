import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PatientFeedbackServiceService } from '../patient-feedback-service.service';

@Component({
  selector: 'app-prescription',
  templateUrl: './prescription.component.html',
  styleUrls: ['./prescription.component.css']
})
export class PrescriptionComponent implements OnInit {

  id: any;
  date: any;
  doctor: any;
  errorMsg = "";
  public prescriptions = [] as any

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private service: PatientFeedbackServiceService
  ) {
    this.route.queryParams
    .subscribe(params => {
      this.id = params.id;
      this.date = new Date(params.date);
      this.doctor = params.doctor;
    });
  }

  ngOnInit(): void {
    this.service.getPrescription(this.id).subscribe(data => (this.prescriptions = data),
    error => this.errorMsg = "Couldn't load");
  }

}
