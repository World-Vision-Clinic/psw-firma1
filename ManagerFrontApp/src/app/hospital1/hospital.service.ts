import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Building } from '../data/building';

@Injectable({
  providedIn: 'root',
})
export class HospitalService {
  constructor(private http: HttpClient) {}

  getHospital(id): Observable<Building> {
    return this.http.get<Building>(
      `http://localhost:39901/api/Buildings/${id}`
    );
  }
  updateHospital(id, hospital): Observable<boolean> {
    return this.http.put<boolean>(
      `http://localhost:39901/api/Buildings/${id}`,
      hospital
    );
  }

  getFloors(id): Observable<Building> {
    return this.http.get<Building>(
      `http://localhost:39901/api/floors?buildingId=${id}`
    );
  }
}
