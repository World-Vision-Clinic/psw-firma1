import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Doctor } from '../data/doctor';

@Injectable({
    providedIn: 'root',
  })

  export class DoctorsManagementService {
    constructor(private http: HttpClient) {}

    getDoctors(){
        return this.http.get<Doctor[]>('http://localhost:39901/api/Doctors')
    }

  }