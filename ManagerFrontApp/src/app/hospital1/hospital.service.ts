import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Building } from '../data/building';
import { Equipment } from '../data/equipment';
import { Floor } from '../data/floor';
import { iEquipmentRoom } from '../data/iEquipmentRoom';
import { Room } from '../data/room';
import {Shift, ShiftSend } from '../data/shift';
import { Doctor } from '../data/doctor';

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

  orderMoving(data: {
    TargetRoomId: number | undefined;
    TargetEqupmentId: number | undefined;
    startDate: any;
    endDate: any;
    Amount: number | null;
  }): Observable<any> {
    const headers = { 'content-type': 'application/json' };
    return this.http.post<any>(
      `http://localhost:39901/api/transportPeriod`,
      data,
      { headers: headers }
    );
  }

  quickTransport(data:{
    TargetRoomId: number | undefined;
    TargetEqupmentId: number | undefined;
    startDate: any;
    endDate: any;
    Amount: number | null;
  }): Observable<any> {
    const headers = { 'content-type': 'application/json' };
    return this.http.post<any>(
      `http://localhost:39901/api/transportPeriod/quickTransport`,
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
      `http://localhost:39901/api/transportPeriod?buildingId=${buildingId}&transportDurationInHours=${etimateTime}&startDateTimeStamp=${startDate.getTime()}&endDateTimeStamp=${endDate.getTime()}`
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

  mergeRooms(mergingDTO){
    const headers={'content-type':'application/json'};  
    const body=JSON.stringify(mergingDTO)     
    return this.http.post('http://localhost:39901/api/Rooms/merge', body,{'headers': headers}).subscribe(data => {console.log(data)
    });
  }

  splitRoom(splitDTO){
    const headers={'content-type':'application/json'};  
    const body=JSON.stringify(splitDTO)  
    return this.http.post('http://localhost:39901/api/Rooms/split', body,{'headers': headers}).subscribe(data => {console.log(data)});
  }

  makeNewShift(shift:ShiftSend){
    const headers={'content-type':'application/json'};  
    const body=JSON.stringify(shift) 
    return this.http.post('http://localhost:39901/api/shifts/newShift', body,{'headers': headers}).subscribe(data => {console.log(data)});
  }

  updateShift(shift:ShiftSend){
    const headers={'content-type':'application/json'};  
    const body=JSON.stringify(shift) 
    return this.http.post('http://localhost:39901/api/shifts/update', body,{'headers': headers}).subscribe(data => {console.log(data)});
  }

  getAllShifts(){
    return this.http.get<Shift[]>('http://localhost:39901/api/shifts/getAll')
  }

  deleteShift(id:number){
    this.http.delete('http://localhost:39901/api/shifts/'+id).subscribe(data=>console.log(data)
    );
  }

  getDoctors(){
    return this.http.get<Doctor[]>('http://localhost:39901/api/Doctors')
  }

  changeShift(dto){
    const headers={'content-type':'application/json'};  
    const body=JSON.stringify(dto) 
    this.http.post('http://localhost:39901/api/Doctors/addShift', body,{'headers': headers}).subscribe(data => {console.log(data)});
  }
}
