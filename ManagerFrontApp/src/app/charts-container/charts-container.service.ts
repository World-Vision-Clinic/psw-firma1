import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Doctor } from '../data/doctor';
import { Vacation } from '../data/vacation';
import { Shift, ShiftSend } from '../data/shift';
import { ThumbSettings } from '@syncfusion/ej2-angular-charts';
import { OnCallShift } from '../data/onCallShift';
import { DoctorStatsRequest } from '../data/doctorStatsRequest';
import { DoctorStat } from '../data/doctorStat';
// import { DoctorStat } from '../data/DoctorStat';

@Injectable({
  providedIn: 'root',
})
export class ChartsService {
  constructor(private http: HttpClient) {}

  getStats(data: DoctorStatsRequest) {
    const headers = { 'content-type': 'application/json' };
    const body = JSON.stringify(data);
    return this.http.post<DoctorStat[]>('/api/doctorStats', body, {
      headers: headers,
    });
  }
}
