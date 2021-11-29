import { DatePipe } from "@angular/common";

export class Allergen {
    Name: string = "";
    Id: number = -1;

    constructor(Name: string, Id: number)
    {
        this.Name = Name;
        this.Id = Id;
    }
}