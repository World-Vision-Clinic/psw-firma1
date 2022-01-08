import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';
import { DoctorsManagementService } from './doctors-management.service';
import {Doctor} from '../data/doctor';
import { Vacation } from '../data/vacation';
import { Shift } from '../data/shift';

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
  
  shifts:Shift[]=[];
  doctors:Doctor[]=[];
  vacations: Vacation[] = [];
  selectedVacation: Vacation | null = null;
  vacation: Vacation = {
    id: -1,
    description: "",
    start: -1,
    end: -1,
    doctorId: -1,
    fullName: ""

  };

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
    this.doctorsManagementService.deleteVacation(2);
    
    this.loadVacations();
    console.log(this.vacations)
  }

  addNewVacation(){
    this.doctorsManagementService.addVacation(this.vacation);
    this.loadVacations();
  }

  selectVacation(id){
    
  }

  async loadShifts(){
    this.doctorsManagementService.getAllShifts().subscribe((data)=>{this.shifts=data}, (error) => {console.log(error);
    })
  }

  fetchDoctorsAndShifts(id){
    for(let i=0; i<this.shifts.length;i++){
      if(this.shifts[i].id===id){
        return this.shifts[i].name
      } 
    }
    return ''
  }

}
