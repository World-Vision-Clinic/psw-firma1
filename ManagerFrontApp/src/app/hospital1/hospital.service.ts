import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Building } from '../data/building';
import { Equipment } from '../data/equipment';
import { Floor } from '../data/floor';
import { iEquipmentRoom } from '../data/iEquipmentRoom';
import { Room } from '../data/room';

@Injectable({
  providedIn: 'root',
})
export class HospitalService {
  constructor(private http: HttpClient) {}

  getHospital(id: string): Observable<Building> {
    return this.http.get<Building>(
      `http://localhost:8080/api/Buildings/${id}`
    );
  }

  updateHospital(id: string, hospital: Building): Observable<boolean> {
    const headers = { 'content-type': 'application/json' };
    return this.http.put<boolean>(
      `http://localhost:8080/api/buildings/${id}`,
      hospital,
      { headers }
    );
  }
  orderMoving(data: {
    TargetRoomId: number | undefined;
    TargetEqupmentId: number | undefined;
    startDate: any;
    endDate: any;
    Amount: number | null;
  }): Observable<any> {
    const headers = { 'content-type': 'application/json' };
    return this.http.post<any>(
      `http://localhost:8080/api/transportPeriod`,
      data,
      { headers: headers }
    );
  }
  getSuggestionForPeriod(
    buildingId: string,
    startDate: Date,
    endDate: Date,
    etimateTime: number
  ): Observable<any> {
    const headers = { 'content-type': 'application/json' };
    return this.http.get<any>(
      `http://localhost:8080/api/transportPeriod?buildingId=${buildingId}&transportDurationInHours=${etimateTime}&startDateTimeStamp=${startDate.getTime()}&endDateTimeStamp=${endDate.getTime()}`
    );
  }

  updateRoom(id: string, room: Room): Observable<boolean> {
    console.log(room);

    const headers = { 'content-type': 'application/json' };
    return this.http.put<boolean>(
      `http://localhost:8080/api/Rooms/${id}`,
      room,
      { headers }
    );
  }

  getFloors(id: string): Observable<Floor[]> {
    return this.http.get<Floor[]>(
      `http://localhost:8080/api/floors?buildingId=${id}`
    );
  }

  getEquipments(id: string, searchText: string): Observable<Equipment[]> {
    return this.http.get<Equipment[]>(
      `http://localhost:8080/api/equipment?buildingId=${id}&searchText=${searchText}`
    );
  }

  getEquipmentRooms(
    id: string,
    equipmentName: string
  ): Observable<iEquipmentRoom[]> {
    return this.http.get<iEquipmentRoom[]>(
      `http://localhost:8080/api/equipment/byRooms?buildingId=${id}&equipmentName=${equipmentName}`
    );
  }

  getEquipment(roomid: number): Observable<Room> {
    return this.http.get<Room>(`http://localhost:8080/api/Rooms/${roomid}`);
  }
}
