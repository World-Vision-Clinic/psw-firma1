import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatButtonModule} from '@angular/material/button';
import {MatToolbarModule} from '@angular/material/toolbar';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import {MatRadioModule} from '@angular/material/radio';
import {MatListModule} from '@angular/material/list';
import {MatTooltipModule} from '@angular/material/tooltip';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { PatientFeedbackServiceService } from './patient-feedback-service.service';
import { PatientFeedbackViewComponent } from './patient-feedback-view/patient-feedback-view.component';
import { LoginComponent } from './login/login.component';
import { PatientCreateFeedbackComponent } from './patient-create-feedback/patient-create-feedback.component';
import { PatientCreateFeedbackService } from './patient-create-feedback.service';
import { HomePageComponent } from './homepage/homepage.component';
import { SurveyComponent } from './survey/survey/survey.component';
import { SurveyService } from './survey.service';
import { MedicalRecordViewComponent } from './medical-record-view/medical-record-view.component';

@NgModule({
  declarations: [
    AppComponent,
    PatientCreateFeedbackComponent,
    PatientFeedbackViewComponent,
    LoginComponent,
    HomePageComponent,
    SurveyComponent,
    MedicalRecordViewComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatToolbarModule,
    MatButtonModule,
    MatIconModule,
    CommonModule,
    FormsModule,
    HttpClientModule,
    AppRoutingModule,
    MatRadioModule,
    MatListModule,
    MatCardModule,
    MatTooltipModule,
    BrowserAnimationsModule
  ],
  providers: [
    PatientCreateFeedbackService,
    PatientFeedbackServiceService,
    SurveyService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
