import { DatePipe } from "@angular/common";

export class User {
    UserName: string = "";
    Password: string = "";
    FirstName: string = "";
    LastName: string = "";
    EMail: string = "";
    Gender: string = "";
    Jmbg: string = "";
    DateOfBirth: Date = new Date();
    Country: string = "";
    Address: string = "";
    City: string = "";
    Phone: string = "";
    Allergens: number[] = [];
    PreferedDoctor: number = -1;
    Weight: number = -1;
    Height: number = -1;
    BloodType: string = "";
    
    constructor()
    {
    }
}