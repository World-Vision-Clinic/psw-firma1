import { DatePipe } from "@angular/common";

export class Appointment {
    patientForeignKey: number = 0;
    doctorForeignKey: number = 0;
    doctorName: string;
    type: number = 0;
    date: Date = new Date();
    isCancelled: boolean;
    isUpcoming: boolean;
    hasCompletedSurvey: boolean;

    constructor(patientForeignKey: number, doctorForeignKey: number, type: number, date: Date, isCanceled: boolean, isFinished: boolean, isUpcoming: boolean  )
    {
        this.patientForeignKey = patientForeignKey;
        this.doctorForeignKey = doctorForeignKey;
        this.doctorName = "Undefined Doctor";
        this.type = type;
        this.date = date; 
        this.isCancelled = isCanceled;
        this.isUpcoming = isUpcoming;
        this.hasCompletedSurvey = false;
    }

}