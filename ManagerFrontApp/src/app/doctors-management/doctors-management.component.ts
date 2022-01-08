import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-doctors-management',
  templateUrl: './doctors-management.component.html',
  styleUrls: ['./doctors-management.component.css']
})
export class DoctorsManagementComponent implements OnInit {

  initialTableBox = true;
  vacationTableBox = false;
  addVacationBox = false;
  buttonsBox = false;
  
  constructor() { }

  ngOnInit(): void {
    
  }

}
