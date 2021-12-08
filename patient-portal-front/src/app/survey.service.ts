import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { SurveyQuestion } from 'src/surveyQuestion';

@Injectable({
  providedIn: 'root'
})
export class SurveyService {

  
  constructor(private http: HttpClient) { }
  httpOptions = {
    headers: new HttpHeaders().set('Content-Type', 'application/json')
  };

  getQuestions() : Observable<SurveyQuestion[]>{
    return this.http.get<SurveyQuestion[]>("/api/Survey"); 
    }

addSurvey(questions: SurveyQuestion[]): Observable<SurveyQuestion[]> {
  console.log("servis");
  return this.http.post<SurveyQuestion[]>("api/Survey", JSON.stringify(questions), this.httpOptions);
}
 
}





 

