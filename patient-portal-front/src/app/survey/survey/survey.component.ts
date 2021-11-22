import { Component, OnInit } from '@angular/core';
import { SurveyService } from 'src/app/survey.service';

@Component({
  selector: 'app-survey',
  templateUrl: './survey.component.html',
  styleUrls: ['./survey.component.css']
})
export class SurveyComponent implements OnInit {

  public questions = [] as any
  public errorMsg = ""

  public doctorSection = [] as any
  public hospitalSection = [] as any
  public staffSection = [] as any

  constructor(private _surveyService : SurveyService) { }

  sortQuestions(): void {
    for (var question of this.questions){
      if(question.section == 0){
        this.hospitalSection.push(question);
      }
      else if(question.section == 1){
        this.doctorSection.push(question);
      }
      else if(question.section == 2){
        this.staffSection.push(question);
      }
    }     

  }

  submitSurvey(): void {
    console.log("komponenta");
    this._surveyService.addSurvey(this.questions).subscribe(
      data => console.log(data));
  }

  ngOnInit(): void {
    this._surveyService.getQuestions().subscribe(data => this.questions = data,
    error => this.errorMsg = "Couldn't load user questions", 
    ()  => this.sortQuestions());

                                           
  }

}
