import { DatePipe } from "@angular/common";

export class MedicalRecord {
    userName: string = "";
    eMail: string = "";
    residence: string = "";
    contactPhone: string = "";
    bloodType: string = "";
    doctorName: string = "";

    dateOfBirth: Date = new Date();

    gender: string = "";
    height: number = 190;
    weight: number = 90;
    
    constructor()
    {

    }
}