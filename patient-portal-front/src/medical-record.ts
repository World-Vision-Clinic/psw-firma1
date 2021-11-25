import { DatePipe } from "@angular/common";

export class MedicalRecord {
    userName: string = "";
    firstName: string = "";
    lastName: string = "";
    eMail: string = "";
    jmbg: string = "";
    country: string = "";
    city: string = "";
    address: string = "";
    phone: string = "";
    bloodType: string = "";
    preferedDoctorName: string = "";

    dateOfBirth: Date = new Date();

    gender: string = "";
    height: number = 190;
    weight: number = 90;

    allergenList: string[] = [] as any;
    
    constructor()
    {

    }
}