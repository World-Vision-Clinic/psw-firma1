import { Component, OnInit } from '@angular/core';
import { Doctor } from '../data/doctor';
import { DoctorsManagementService } from '../doctors-management/doctors-management.service';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-doctor-on-duty-container',
  templateUrl: './doctor-on-duty-container.component.html',
  styleUrls: ['./doctor-on-duty-container.component.css'],
})
export class DoctorOnDutyContainerComponent implements OnInit {
  constructor(
    private doctorsManagementService: DoctorsManagementService, // private onDutyService: OnDutyServic
    public datepipe: DatePipe
  ) {}

  selectedOnDuty: any | null;
  onDutyList: any[] = [];
  buttonsBox: boolean = true;
  onDutyContainerVisible: boolean = false;
  selectedDoctor: Doctor = {} as Doctor;
  doctors: Doctor[] = [];

  picker: any;

  ngOnInit(): void {
    this.loadDoctors();
  }

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
    this.selectedOnDuty = null;
  };

  convertDateToString = (date: Date): string => {
    const dateString = this.datepipe.transform(date, 'yyyy-MM-dd');

    return !!dateString ? dateString : '';
  };

  save = () => {
    if (this.selectedOnDuty.doctor == null || this.selectedOnDuty.date == null)
      return;
    this.selectedOnDuty.date = this.convertDateToString(
      this.selectedOnDuty.date
    );
    console.log(this.selectedOnDuty);

    if (this.selectedOnDuty.new) this.createNewOnDuty();
    else this.updateOnDuty();
  };

  createNewOnDuty = async () => {
    delete this.selectedOnDuty.new;
  };
  updateOnDuty = async () => {};

  selectOnDuty = (onDuty) => {
    this.selectedOnDuty = onDuty;
  };

  openNewOnDuty = () => {
    this.onDutyContainerVisible = true;
    this.buttonsBox = false;
    this.selectedOnDuty = {};
    this.selectedOnDuty.new = true;
  };
  openEditOnDuty = () => {
    if (this.selectedOnDuty == null) return;
    this.onDutyContainerVisible = true;
    this.buttonsBox = false;
  };
}
