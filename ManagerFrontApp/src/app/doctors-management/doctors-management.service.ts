import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Doctor } from '../data/doctor';
import { Vacation } from '../data/vacation';
import { Shift, ShiftSend } from '../data/shift';
import { ThumbSettings } from '@syncfusion/ej2-angular-charts';
import { OnCallShift } from '../data/onCallShift';

@Injectable({
  providedIn: 'root',
})
export class DoctorsManagementService {
  constructor(private http: HttpClient) {}

  getDoctors() {
    return this.http.get<Doctor[]>('http://localhost:39901/api/Doctors');
  }

  getVacations() {
    return this.http.get<Vacation[]>('http://localhost:39901/api/vacations');
  }

  deleteVacation(id: number) {
    this.http
      .delete('http://localhost:39901/api/vacations/' + id)
      .subscribe((data) => console.log(data));
  }

  addVacation(vacation: Vacation) {
    const headers = { 'content-type': 'application/json' };
    const body = JSON.stringify(vacation);
    return this.http
      .post('http://localhost:39901/api/vacations', body, { headers: headers })
      .subscribe((data) => {
        console.log(data);
      });
  }

  updateVacation(vacation: Vacation) {
    const headers = { 'content-type': 'application/json' };
    const body = JSON.stringify(vacation);
    return this.http
      .put('http://localhost:39901/api/vacations', body, { headers: headers })
      .subscribe((data) => {
        console.log(data);
      });
  }

  makeNewShift(shift: ShiftSend) {
    const headers = { 'content-type': 'application/json' };
    const body = JSON.stringify(shift);
    return this.http
      .post('http://localhost:39901/api/shifts/newShift', body, {
        headers: headers,
      })
      .subscribe((data) => {
        console.log(data);
      });
  }

  updateShift(shift: ShiftSend) {
    const headers = { 'content-type': 'application/json' };
    const body = JSON.stringify(shift);
    return this.http
      .post('http://localhost:39901/api/shifts/update', body, {
        headers: headers,
      })
      .subscribe((data) => {
        console.log(data);
      });
  }

  getAllShifts() {
    return this.http.get<Shift[]>('http://localhost:39901/api/shifts/getAll');
  }

  deleteShift(id: number) {
    this.http
      .delete('http://localhost:39901/api/shifts/' + id)
      .subscribe((data) => console.log(data));
  }

  changeShift(dto) {
    const headers = { 'content-type': 'application/json' };
    const body = JSON.stringify(dto);
    this.http
      .post('http://localhost:39901/api/Doctors/addShift', body, {
        headers: headers,
      })
      .subscribe((data) => {
        console.log(data);
      });
  }

  getOnCallShiftsForDoctor(doctorId: number) {
    return this.http.get<OnCallShift[]>(
      `http://localhost:39901/api/onCallShifts/doctorsDuty/${doctorId}`
    );
  }

  getVacationsForDoctor(doctorId: number) {
    return this.http.get<Vacation[]>(
      `http://localhost:39901/api/vacations/doctorsVacations/${doctorId}`
    );
  }
}
