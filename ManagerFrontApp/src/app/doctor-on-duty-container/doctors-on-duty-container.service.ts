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
export class OnDutyService {
  constructor(private http: HttpClient) {}

  getOnCallShifts() {
    return this.http.get<OnCallShift[]>('/api/onCallShifts');
  }

  addOnCallShift(vacation: OnCallShift | null) {
    const headers = { 'content-type': 'application/json' };
    const body = JSON.stringify(vacation);
    return this.http.post('/api/onCallShifts', body, {
      headers: headers,
    });
  }

  updateOnCallShift(shift: OnCallShift | null) {
    const headers = { 'content-type': 'application/json' };
    const body = JSON.stringify(shift);
    return this.http.put('/api/onCallShifts', body, {
      headers: headers,
    });
  }
}
