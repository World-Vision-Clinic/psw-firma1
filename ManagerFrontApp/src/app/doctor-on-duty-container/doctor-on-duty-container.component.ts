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
  };

  convertDateToString = (date: Date): string => {
    const dateString = this.datepipe.transform(date, 'yyyy-MM-dd');

    return !!dateString ? dateString : '';
  };

  save = () => {
    if (this.selectedOnDuty?.doctor == null || this.selectedOnDuty.date == null)
      return;
    // this.selectedOnDuty.date = this.convertDateToString(
    //   this.selectedOnDuty.date
    // );

    if (this.selectedOnDuty.new) this.createNewOnDuty();
    else this.updateOnDuty();
  };

  createNewOnDuty = async () => {
    delete this.selectedOnDuty?.new;
    await this.onDutyService.addOnCallShift(this.selectedOnDuty);
    await this.loadOnCallDuties();
    this.close();
  };
  updateOnDuty = async () => {
    delete this.selectedOnDuty?.new;
    await this.onDutyService.updateOnCallShift(this.selectedOnDuty);
    await this.loadOnCallDuties();
    this.close();
  };

  selectOnDuty = (onDuty) => {
    this.selectedOnDuty = onDuty;
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
  };
}
