import { Component, OnInit } from '@angular/core';
import { Doctor } from '../data/doctor';
import { DoctorsManagementService } from '../doctors-management/doctors-management.service';
import { DatePipe } from '@angular/common';
import { OnDutyService } from './doctors-on-duty-container.service';
import { OnCallShift } from '../data/onCallShift';

@Component({
  selector: 'app-doctor-on-duty-container',
  templateUrl: './doctor-on-duty-container.component.html',
  styleUrls: ['./doctor-on-duty-container.component.css'],
})
export class DoctorOnDutyContainerComponent implements OnInit {
  constructor(
    private doctorsManagementService: DoctorsManagementService,
    private onDutyService: OnDutyService,
    public datepipe: DatePipe
  ) {}

  selectedOnDuty: OnCallShift = {} as OnCallShift;
  onDutyList: OnCallShift[] = [];
  buttonsBox: boolean = true;
  onDutyContainerVisible: boolean = false;
  selectedDoctor: Doctor = {} as Doctor;
  doctors: Doctor[] = [];
  selectedDoctorsId: number = -1;
  picker: any;

  ngOnInit(): void {
    this.loadOnCallDuties();
    this.loadDoctors();
  }

  loadOnCallDuties = () => {
    this.onDutyService.getOnCallShifts().subscribe(
      (data) => {
        console.log(data);

        this.onDutyList = data;
      },
      (error) => {
        console.log(error);
      }
    );
  };

  loadDoctors = async () => {
    this.doctorsManagementService.getDoctors().subscribe(
      (data) => {
        console.log(data);

        this.doctors = data;
      },
      (error) => {
        console.log(error);
      }
    );
  };

  close = () => {
    this.onDutyContainerVisible = false;
    this.buttonsBox = true;
    this.selectedOnDuty = {} as OnCallShift;
    this.selectedDoctorsId = -1;
  };

  convertDateToString = (date: Date): string => {
    const dateString = this.datepipe.transform(date, 'yyyy-MM-dd');

    return !!dateString ? dateString : '';
  };

  save = () => {
    if (this.selectedDoctorsId == -1 || this.selectedOnDuty.date == null)
      return;
    const index = this.doctors.findIndex((e) => e.id == this.selectedDoctorsId);
    this.selectedOnDuty.doctor = this.doctors[index];
    this.selectedOnDuty.doctorId = this.selectedDoctorsId;
    this.selectedOnDuty.date.setHours(this.selectedOnDuty.date.getHours() + 4);

    if (this.selectedOnDuty.new) this.createNewOnDuty();
    else this.updateOnDuty();
  };

  createNewOnDuty = () => {
    delete this.selectedOnDuty?.new;
    this.onDutyService.addOnCallShift(this.selectedOnDuty).subscribe(
      (data: any) => {
        if (data.statusCode == 409) {
          alert(
            `${this.selectedOnDuty.doctor.firstName} already has On-call shift on that day!`
          );
          return;
        }

        this.selectedDoctorsId = -1;
        this.loadOnCallDuties();
        this.close();
      },
      (error) => {
        console.log(error);

        alert(
          `${this.selectedOnDuty.doctor.firstName} already has On-call shift on that day!`
        );
      }
    );
  };
  updateOnDuty = async () => {
    delete this.selectedOnDuty?.new;
    await this.onDutyService.updateOnCallShift(this.selectedOnDuty).subscribe(
      (data: any) => {
        if (data.statusCode == 409) {
          alert(
            `${this.selectedOnDuty.doctor.firstName} already has On-call shift on that day!`
          );
          return;
        }
        console.log(data);
        this.loadOnCallDuties();
        this.close();
      },
      (error) => {
        alert(
          `${this.selectedDoctor.firstName} already has On-call shift on that day!`
        );
        console.log(error);
      }
    );
  };

  selectOnDuty = (onDuty) => {
    this.selectedOnDuty = onDuty;
    this.selectedDoctorsId = onDuty.doctor.id;
  };

  openNewOnDuty = () => {
    this.onDutyContainerVisible = true;
    this.buttonsBox = false;
    this.selectedOnDuty = {} as OnCallShift;
    this.selectedOnDuty.new = true;
  };
  openEditOnDuty = () => {
    if (this.selectedOnDuty == ({} as OnCallShift)) return;
    this.onDutyContainerVisible = true;
    this.buttonsBox = false;
    console.log(this.selectedOnDuty);
  };
}
