import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Building } from '../data/building';
import { Equipment } from '../data/equipment';
import { Room } from '../data/room';

@Injectable({
  providedIn: 'root',
})
export class HospitalService {
  constructor(private http: HttpClient) {}

  getHospital(id: string): Observable<Building> {
    return this.http.get<Building>(
      `http://localhost:39901/api/Buildings/${id}`
    );
  }

  updateHospital(id: string, hospital: Building): Observable<boolean> {
    const headers = { 'content-type': 'application/json' };
    return this.http.put<boolean>(
      `http://localhost:39901/api/buildings/${id}`,
      hospital,
      { headers }
    );
  }

  updateRoom(id: string, room: Room): Observable<boolean> {
    console.log(room);

    const headers = { 'content-type': 'application/json' };
    return this.http.put<boolean>(
      `http://localhost:39901/api/Rooms/${id}`,
      room,
      { headers }
    );
  }

  getFloors(id: string): Observable<Building> {
    return this.http.get<Building>(
      `http://localhost:39901/api/floors?buildingId=${id}`
    );
  }

  getEquipment(roomid: number): Observable<Room>{
    return this.http.get<Room>(
      `http://localhost:39901/api/Rooms/${roomid}`
    );
  }
}
