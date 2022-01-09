import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';
import { DoctorsManagementService } from './doctors-management.service';
import { Doctor } from '../data/doctor';
import { Vacation } from '../data/vacation';
import { Shift } from '../data/shift';
import { ShiftSend } from '../data/shift';

@Component({
  selector: 'app-doctors-management',
  templateUrl: './doctors-management.component.html',
  styleUrls: ['./doctors-management.component.css'],
})
export class DoctorsManagementComponent implements OnInit {
  initialTableBox = true;
  vacationTableBox = false;
  addVacationBox = false;
  editVacationBox = false;
  buttonsBox = false;

  doctors: Doctor[] = [];
  vacations: Vacation[] = [];
  selectedVacation: Vacation | null = null;
  selectedRowIndex = -1;
  vacation: Vacation = {
    id: -1,
    description: '',
    start: -1,
    end: -1,
    doctorId: -1,
    fullName: '',
  };

  toolbarButtons = [
    {
      name: 'Init table',
      codeName: 'initTable',
    },
    {
      name: 'Shifts',
      codeName: 'shifts',
    },
    {
      name: 'Vacations',
      codeName: 'vacations',
    },
    {
      name: 'On duty',
      codeName: 'onDuty',
    },
    {
      name: 'Charts',
      codeName: 'charts',
    },
  ];

  listOfShiftsBox: boolean = true;
  shiftsBtnsBox: boolean = false;
  shiftsListBox: boolean = false;
  shiftsDoctorsBox: boolean = false;
  shiftsBox: boolean = false;
  createShiftBox: boolean = false;
  updateShiftBox: boolean = false;
  shiftInfoBox: boolean = false;
  pickShiftBox: boolean = false;
  onDutyTableBox: boolean = false;

  newShift: ShiftSend = {
    Id: 0,
    Name: '',
    Start: -1,
    End: -1,
  };
  selectedShift: ShiftSend = {
    Id: -1,
    Name: '',
    Start: -1,
    End: -1,
  };
  shifts: Shift[] = [];

  selectedDoctor: Doctor | null = null;
  docShiftDTO = {
    doctorId: -1,
    shiftId: -1,
  };

  selectedDoctorV: Doctor = {
    id: -1,
    firstName: '',
    lastName: '',
    shiftId: -1,
    roomId: -1,
    type: '',
  };

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private doctorsManagementService: DoctorsManagementService
  ) {}

  ngOnInit(): void {
    this.doctorsManagementService.getDoctors().subscribe(
      (data) => {
        this.doctors = data;
      },
      (error) => {
        console.log(error);
      }
    );
    this.loadShifts();
    this.loadDoctors();
  }

  async loadDoctors() {
    this.doctorsManagementService.getDoctors().subscribe(
      (data) => {
        this.doctors = data;
      },
      (error) => {
        console.log(error);
      }
    );
  }

  loadVacations() {
    this.doctorsManagementService.getVacations().subscribe(
      (data) => {
        this.vacations = data;
      },
      (error) => {
        console.log(error);
      }
    );
  }

  deleteVacation() {
    this.doctorsManagementService.deleteVacation(this.selectedVacation?.id!);
    alert('Vacation deleted!');
    this.loadVacations();
  }

  addNewVacation() {
    this.vacation.doctorId = this.selectedDoctor?.id!;
    this.vacation.fullName =
      this.selectedDoctor?.firstName + ' ' + this.selectedDoctor?.lastName;
    console.log(this.vacation);
    this.doctorsManagementService.addVacation(this.vacation);
    alert('Vacation added!');
    this.addVacationBox = false;
    this.buttonsBox = false;
    this.loadVacations();
  }

  editVacation() {
    this.vacation.doctorId = this.selectedDoctor?.id!;
    this.vacation.fullName =
      this.selectedDoctor?.firstName + ' ' + this.selectedDoctor?.lastName;
    this.vacation.id = this.selectedVacation?.id!;
    this.doctorsManagementService.updateVacation(this.vacation);
    alert('Vacation edited!');
    this.editVacationBox = false;
    this.buttonsBox = false;
    this.loadVacations();
  }

  //SHIFTS
  createNewShiftBox() {
    this.createShiftBox = true;
    this.listOfShiftsBox = false;
    this.shiftInfoBox = false;
    this.updateShiftBox = false;
  }
  openshiftInfoBox() {
    if (this.selectedShift) {
      this.createShiftBox = false;
      this.listOfShiftsBox = false;
      this.shiftInfoBox = true;
      this.updateShiftBox = false;
    }
  }
  openUpdateShiftBox() {
    if (this.selectedShift) {
      this.createShiftBox = false;
      this.listOfShiftsBox = false;
      this.shiftInfoBox = false;
      this.updateShiftBox = true;
    }
  }

  selectVacation(vacation) {
    this.selectedVacation = vacation;
    this.selectedRowIndex = vacation.id;
  }

  updateShift() {
    this.doctorsManagementService.updateShift(this.selectedShift);
    this.loadShifts();
  }

  createShift() {
    this.doctorsManagementService.makeNewShift(this.newShift);
    this.loadShifts();
  }

  selectShift(item) {
    this.selectedShift.Id = item.id;
    this.selectedShift.Name = item.name;
    this.selectedShift.Start = item.start;
    this.selectedShift.End = item.end;
    alert('Shift is selected.');
  }

  deleteShift() {
    this.doctorsManagementService.deleteShift(this.selectedShift.Id);
    this.loadShifts();
  }

  async loadShifts() {
    this.doctorsManagementService.getAllShifts().subscribe(
      (data) => {
        this.shifts = data;
      },
      (error) => {
        console.log(error);
      }
    );
  }

  fetchDoctorsAndShifts(id) {
    for (let i = 0; i < this.shifts.length; i++) {
      if (this.shifts[i].id === id) {
        return this.shifts[i].name;
      }
    }
    return '';
  }

  selectDoctor(doctor) {
    this.selectedDoctor = doctor;
    alert('Doctor is selected.');
  }

  pickShift() {
    this.addOrChangeDocShift();
    this.shiftsBox = false;
    this.pickShiftBox = false;
    this.shiftInfoBox = false;
    this.shiftsBtnsBox = false;
    this.shiftsListBox = false;
    this.shiftsDoctorsBox = false;
    this.loadDoctors;
  }

  addOrChangeDocShift() {
    if (this.selectedDoctor != null) {
      this.docShiftDTO.doctorId = this.selectedDoctor.id;
    } else {
      alert('Select doctor first!');
      return;
    }
    this.docShiftDTO.shiftId = this.selectedShift.Id;
    this.doctorsManagementService.changeShift(this.docShiftDTO);
  }

  pickDoctor() {
    if (this.selectedDoctor != null) {
      this.shiftsDoctorsBox = false;
      this.pickShiftBox = true;
      this.listOfShiftsBox = true;
      this.shiftsListBox = true;
    } else {
      alert('You must pick a doctor first.');
    }
  }
  //END OF SHIFTS PART

  openOnDuty = () => {
    this.vacationTableBox = false;
    this.initialTableBox = false;
    this.onDutyTableBox = true;
  };

  selectedModule: string = 'initTable';
  openModule = (module: string) => {
    if (module == 'vacations') {
      this.buttonsBox = false;
      this.addVacationBox = false;
      this.editVacationBox = false;

      this.loadVacations();
    }
    this.selectedModule = module;
  };
}
