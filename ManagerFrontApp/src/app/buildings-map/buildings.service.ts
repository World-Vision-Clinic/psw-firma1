import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Building } from '../data/building';

@Injectable({
  providedIn: 'root',
})
export class BuildingsService {
  constructor(private http: HttpClient) {}

  getBuildings(): Observable<Building[]> {
    return this.http.get<Building[]>('http://localhost:8080/api/Buildings');
  }
}
