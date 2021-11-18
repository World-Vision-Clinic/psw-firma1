import { DatePipe } from "@angular/common";

export class Patient {
    userName: string = "";
    eMail: string = "";
    residence: string = "";
    contactPhone: string = "";
    bloodType: string = "";
    doctorName: string = "";

    gender: number = 1;
    height: number = 190;
    weight: number = 90;
    
    constructor()
    {

    }
}