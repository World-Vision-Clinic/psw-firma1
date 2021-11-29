import { DatePipe } from "@angular/common";

export class Doctor {
    FirstName: string = "";
    LastName: string = "";
    Id: number = -1;

    constructor(FirstName: string, LastName: string, Id: number)
    {
        this.FirstName = FirstName;
        this.LastName = LastName;
        this.Id = Id;
    }
}