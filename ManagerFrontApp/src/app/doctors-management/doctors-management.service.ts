import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Doctor } from '../data/doctor';
import { Vacation } from '../data/vacation';
import { ThumbSettings } from '@syncfusion/ej2-angular-charts';

@Injectable({
    providedIn: 'root',
  })

  export class DoctorsManagementService {
    constructor(private http: HttpClient) {}

    getDoctors(){
        return this.http.get<Doctor[]>('http://localhost:39901/api/Doctors')
    }

    getVacations(){
        return this.http.get<Vacation[]>('http://localhost:39901/api/vacations')
    }

    deleteVacation(id: number){
      this.http.delete('http://localhost:39901/api/vacations/'+id).subscribe(data=>console.log(data)
      );
    }

  }