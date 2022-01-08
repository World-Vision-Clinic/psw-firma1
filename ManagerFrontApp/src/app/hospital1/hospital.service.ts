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
  getRenovation(id: number): Observable<any> {
    return this.http.get<any>(
      `http://localhost:39901/api/renovation/room/${id}`
    );
  }
  cancelRenovation(id: number): Observable<any> {
    return this.http.delete<any>(
      `http://localhost:39901/api/renovation/${id}`
    );
  }

  updateHospital(id: string, hospital: any): Observable<any> {
    const headers = { 'Content-Type': 'application/json' };
    const body = JSON.stringify(hospital);
    console.log(body);
    return this.http.put<any>(
      `http://localhost:39901/api/buildings/${id}`,
      body,
      { 'headers':headers }
    );
  }

  orderMoving(data: {
    TargetRoomId: number | undefined;
    TargetEqupmentId: number | undefined;
    startDate: any;
    endDate: any;
    Amount: number | null;
  }): Observable<any> {
    const headers = { 'Content-Type': 'application/json' };
    return this.http.post<any>(
      `http://localhost:39901/api/transportPeriod`,
      data,
      { 'headers': headers }
    );
  }
  getSuggestionForPeriod(
    buildingId: string,
    startDate: Date,
    endDate: Date,
    etimateTime: number
  ): Observable<any> {
    const headers = { 'Content-Type': 'application/json' };
    return this.http.get<any>(
      `http://localhost:39901/api/transportPeriod?buildingId=${buildingId}&transportDurationInHours=${etimateTime}&startDateTimeStamp=${startDate.getTime()}&endDateTimeStamp=${endDate.getTime()}`
    );
  }

  updateRoom(id: string, room: Room): Observable<boolean> {
    console.log(room);

    const headers = { 'Content-Type': 'application/json' };
    return this.http.put<boolean>(
      `http://localhost:39901/api/Rooms/${id}`,
      room,
      { headers }
    );
  }

  getFloors(id: string): Observable<Floor[]> {
    return this.http.get<Floor[]>(
      `http://localhost:39901/api/floors?buildingId=${id}`
    );
  }

  getEquipments(id: string, searchText: string): Observable<Equipment[]> {
    return this.http.get<Equipment[]>(
      `http://localhost:39901/api/equipment?buildingId=${id}&searchText=${searchText}`
    );
  }

  getEquipmentRooms(
    id: string,
    equipmentName: string
  ): Observable<iEquipmentRoom[]> {
    return this.http.get<iEquipmentRoom[]>(
      `http://localhost:39901/api/equipment/byRooms?buildingId=${id}&equipmentName=${equipmentName}`
    );
  }

  getEquipment(roomid: number): Observable<Room> {
    return this.http.get<Room>(`http://localhost:39901/api/Rooms/${roomid}`);
  }

  getAppointments(roomid: number): Observable<Appointment> {
    return this.http.get<Appointment>(`http://localhost:39901/api/Appointment/room/${roomid}`);
  }

  cancelTransport(id: number){
 
    return this.http.get(
      `http://localhost:39901/api/equipment/${id}`);
  }
  

  mergeRooms(mergingDTO){
    const headers={'Content-Type':'application/json'};  
    const body=JSON.stringify(mergingDTO)     
    return this.http.post('http://localhost:39901/api/Rooms/merge', body,{headers: headers}).subscribe(data => {console.log(data)
    });
  }

  splitRoom(splitDTO){
    const headers={'Content-Type':'application/json'};  
    const body=JSON.stringify(splitDTO)  
    return this.http.post('http://localhost:39901/api/Rooms/split', body,{headers: headers}).subscribe(data => {console.log(data)});
  }
  scheduleRenovation(data){
    const headers={'Content-Type':'application/json'};  
    const body=JSON.stringify(data)
    console.log(body);
      
    return this.http.post('http://localhost:39901/api/Renovation', body,{headers: headers});
  }

  getSuggestionForRenovation(data): Observable<any>{
    const headers={'Content-Type':'application/json'}; 
    const body = JSON.stringify(data)
    return this.http.post<any>('http://localhost:39901/api/renovationPeriod/suggestion', body,{headers: headers});
  }

}
