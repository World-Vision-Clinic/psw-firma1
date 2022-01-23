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
    return this.http.get<Doctor[]>('/api/Doctors');
  }

  getVacations() {
    return this.http.get<Vacation[]>('/api/vacations');
  }

  deleteVacation(id: number) {
    this.http
      .delete('/api/vacations/' + id)
      .subscribe((data) => console.log(data));
  }

  addVacation(vacation: Vacation) {
    const headers = { 'content-type': 'application/json' };
    const body = JSON.stringify(vacation);
    return this.http
      .post('/api/vacations', body, { headers: headers })
      .subscribe((data) => {
        console.log(data);
      });
  }

  updateVacation(vacation: Vacation) {
    const headers = { 'content-type': 'application/json' };
    const body = JSON.stringify(vacation);
    return this.http
      .put('/api/vacations', body, { headers: headers })
      .subscribe((data) => {
        console.log(data);
      });
  }

  makeNewShift(shift: ShiftSend) {
    const headers = { 'content-type': 'application/json' };
    const body = JSON.stringify(shift);
    return this.http.post('/api/shifts/newShift', body, {
      headers: headers,
    });
  }

  updateShift(shift: ShiftSend) {
    const headers = { 'content-type': 'application/json' };
    const body = JSON.stringify(shift);
    return this.http.post('/api/shifts/update', body, {
      headers: headers,
    });
  }

  getAllShifts() {
    return this.http.get<Shift[]>('/api/shifts/getAll');
  }

  deleteShift(id: number) {
    return this.http.delete('/api/shifts/' + id);
  }

  changeShift(dto) {
    const headers = { 'content-type': 'application/json' };
    const body = JSON.stringify(dto);
    return this.http.post('/api/Doctors/addShift', body, {
      headers: headers,
    });
  }

  getOnCallShiftsForDoctor(doctorId: number) {
    return this.http.get<OnCallShift[]>(
      `/api/onCallShifts/doctorsDuty/${doctorId}`
    );
  }

  getVacationsForDoctor(doctorId: number) {
    return this.http.get<Vacation[]>(
      `/api/vacations/doctorsVacations/${doctorId}`
    );
  }

  getShiftById(shiftId: number) {
    return this.http.get<Shift>(`/api/shifts/${shiftId}`);
  }
}
