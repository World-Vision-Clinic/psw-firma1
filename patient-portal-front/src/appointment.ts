import { DatePipe } from "@angular/common";

export class Appointment {
    patientForeignKey: number = 0;
    doctorForeignKey: number = 0;
    type: number = 0;
    date: Date = new Date();
    isCanceled: boolean;
    isFinished: boolean;
    isUpcoming: boolean;

    constructor(patientForeignKey: number, doctorForeignKey: number, type: number, date: Date, isCanceled: boolean, isFinished: boolean, isUpcoming: boolean  )
    {
        this.patientForeignKey = patientForeignKey;
        this.doctorForeignKey = doctorForeignKey;
        this.type = type;
        this.date = date; 
        this.isCanceled = isCanceled;
        this.isFinished = isFinished;
        this.isUpcoming = isUpcoming;
        
    }

}