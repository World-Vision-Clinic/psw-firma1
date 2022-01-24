import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Appointment } from '../data/appointment';
import { AppointmentForRoom } from '../data/appointmentForRoom';
import { Building } from '../data/building';
import { Equipment } from '../data/equipment';
import { Floor } from '../data/floor';
import { iEquipmentRoom } from '../data/iEquipmentRoom';
import { Room } from '../data/room';
import { Shift, ShiftSend } from '../data/shift';
import { Doctor } from '../data/doctor';

@Injectable({
  providedIn: 'root',
})
export class HospitalService {
  constructor(private http: HttpClient) {}

  getHospital(id: string): Observable<Building> {
    return this.http.get<Building>(`/api/Buildings/${id}`);
  }
  getRenovation(id: number): Observable<any> {
    return this.http.get<any>(`/api/renovation/room/${id}`);
  }
  cancelRenovation(id: number): Observable<any> {
    return this.http.delete<any>(`/api/renovation/${id}`);
  }

  updateHospital(id: string, hospital: any): Observable<any> {
    const headers = { 'Content-Type': 'application/json' };
    const body = JSON.stringify(hospital);
    console.log(body);
    return this.http.put<any>(`/api/buildings/${id}`, body, {
      headers: headers,
    });
  }

  orderMoving(data: {
    TargetRoomId: number | undefined;
    TargetEqupmentId: number | undefined;
    startDate: any;
    endDate: any;
    Amount: number | null;
  }): Observable<any> {
    const headers = { 'Content-Type': 'application/json' };
    return this.http.post<any>(`/api/transportPeriod`, data, {
      headers: headers,
    });
  }
  getSuggestionForPeriod(
    buildingId: string,
    startDate: Date,
    endDate: Date,
    etimateTime: number
  ): Observable<any> {
    const headers = { 'Content-Type': 'application/json' };
    return this.http.get<any>(
      `/api/transportPeriod?buildingId=${buildingId}&transportDurationInHours=${etimateTime}&startDateTimeStamp=${startDate.getTime()}&endDateTimeStamp=${endDate.getTime()}`
    );
  }

  updateRoom(id: string, room: Room): Observable<boolean> {
    console.log(room);

    const headers = { 'Content-Type': 'application/json' };
    return this.http.put<boolean>(`/api/Rooms/${id}`, room, { headers });
  }

  getFloors(id: string): Observable<Floor[]> {
    return this.http.get<Floor[]>(`/api/floors?buildingId=${id}`);
  }

  getEquipments(id: string, searchText: string): Observable<Equipment[]> {
    return this.http.get<Equipment[]>(
      `/api/equipment?buildingId=${id}&searchText=${searchText}`
    );
  }

  getEquipmentRooms(
    id: string,
    equipmentName: string
  ): Observable<iEquipmentRoom[]> {
    return this.http.get<iEquipmentRoom[]>(
      `/api/equipment/byRooms?buildingId=${id}&equipmentName=${equipmentName}`
    );
  }

  getEquipment(roomid: number): Observable<Room> {
    return this.http.get<Room>(`/api/Rooms/${roomid}`);
  }

  getAppointments(roomid: number): Observable<Appointment> {
    return this.http.get<Appointment>(`/api/Appointment/room/${roomid}`);
  }

  cancelTransport(id: number) {
    return this.http.get(`/api/equipment/${id}`);
  }

  mergeRooms(mergingDTO) {
    const headers = { 'Content-Type': 'application/json' };
    const body = JSON.stringify(mergingDTO);
    return this.http
      .post('/api/Rooms/merge', body, {
        headers: headers,
      })
      .subscribe((data) => {
        console.log(data);
      });
  }

  splitRoom(splitDTO) {
    const headers = { 'Content-Type': 'application/json' };
    const body = JSON.stringify(splitDTO);
    return this.http
      .post('/api/Rooms/split', body, {
        headers: headers,
      })
      .subscribe((data) => {
        console.log(data);
      });
  }
  scheduleRenovation(data) {
    const headers = { 'Content-Type': 'application/json' };
    const body = JSON.stringify(data);
    console.log(body);

    return this.http.post('/api/Renovation', body, {
      headers: headers,
    });
  }

  getSuggestionForRenovation(data): Observable<any> {
    const headers = { 'Content-Type': 'application/json' };
    const body = JSON.stringify(data);
    return this.http.post<any>('/api/renovationPeriod/suggestion', body, {
      headers: headers,
    });
  }

  makeNewShift(shift: ShiftSend) {
    const headers = { 'content-type': 'application/json' };
    const body = JSON.stringify(shift);
    return this.http
      .post('/api/shifts/newShift', body, {
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
      .post('/api/shifts/update', body, {
        headers: headers,
      })
      .subscribe((data) => {
        console.log(data);
      });
  }

  getAllShifts() {
    return this.http.get<Shift[]>('/api/shifts/getAll');
  }

  deleteShift(id: number) {
    this.http
      .delete('/api/shifts/' + id)
      .subscribe((data) => console.log(data));
  }

  getDoctors() {
    return this.http.get<Doctor[]>('/api/Doctors');
  }

  changeShift(dto) {
    const headers = { 'content-type': 'application/json' };
    const body = JSON.stringify(dto);
    this.http
      .post('/api/Doctors/addShift', body, {
        headers: headers,
      })
      .subscribe((data) => {
        console.log(data);
      });
  }
}
