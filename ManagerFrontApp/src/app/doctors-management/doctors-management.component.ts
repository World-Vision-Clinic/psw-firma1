import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';
import { DoctorsManagementService } from './doctors-management.service';
import {Doctor} from '../data/doctor';
import { Vacation } from '../data/vacation';

@Component({
  selector: 'app-doctors-management',
  templateUrl: './doctors-management.component.html',
  styleUrls: ['./doctors-management.component.css']
})
export class DoctorsManagementComponent implements OnInit {

  initialTableBox = true;
  vacationTableBox = false;
  addVacationBox = false;
  editVacationBox = false;
  buttonsBox = false;
  
  doctors:Doctor[]=[];
  vacations: Vacation[] = [];

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private doctorsManagementService : DoctorsManagementService
  ) { }


  ngOnInit(): void { 
    this.doctorsManagementService.getDoctors().subscribe((data)=>{this.doctors=data}, (error)=>{console.log(error);
    })
  }

  async loadDoctors(){
    this.doctorsManagementService.getDoctors().subscribe((data)=>{this.doctors=data}, (error)=>{console.log(error);
    })
  }

  loadVacations(){
    this.doctorsManagementService.getVacations().subscribe((data)=>{this.vacations=data}, (error)=>{console.log(error);
    })
  }

  deleteVacation(){
    this.doctorsManagementService.deleteVacation(4)
    this.loadVacations()
  }

}
