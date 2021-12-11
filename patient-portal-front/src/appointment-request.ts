import { DatePipe } from "@angular/common";

export class AppointmentRequest {
    LowerDateRange: Date = new Date();
    UpperDateRange: Date = new Date();
    LowerTimeRange: string = "08:00:00";
    UpperTimeRange: string = "20:00:00";

    DoctorId: number = 0;
}