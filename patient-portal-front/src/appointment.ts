import { DatePipe } from "@angular/common";

export class Appointment {
    patientForeignKey: number = 0;
    doctorForeignKey: number = 0;
    type: number = 0;
    date: Date = new Date();
}