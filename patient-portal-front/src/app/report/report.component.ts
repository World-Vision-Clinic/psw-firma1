import { Component, OnInit, Type } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-report',
  templateUrl: './report.component.html',
  styleUrls: ['./report.component.css']
})
export class ReportComponent implements OnInit {

  id: any;
  date: any;
  doctor: any;

  constructor(
    private router: Router,
    private route: ActivatedRoute 
  ) { 
    this.route.queryParams
    .subscribe(params => {
      this.id = params.id;
      this.date = new Date(params.date);
      this.doctor = params.doctor;
    });
  }
  
  ngOnInit(): void {
  }

}
