import { DatePipe } from "@angular/common";

export class SurveyBreakdown {
    question: string = "";
    average: number = 0;
    ratingsCount: number[] = [0, 0, 0, 0, 0];
}