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
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import {MatStepperModule} from '@angular/material/stepper';
import { ReactiveFormsModule } from '@angular/forms';

import { PatientFeedbackServiceService } from './patient-feedback-service.service';
import { PatientFeedbackViewComponent } from './patient-feedback-view/patient-feedback-view.component';
import { LoginComponent } from './login/login.component';
import { PatientCreateFeedbackComponent } from './patient-create-feedback/patient-create-feedback.component';
import { PatientCreateFeedbackService } from './patient-create-feedback.service';
import { HomePageComponent } from './homepage/homepage.component';
import { SurveyComponent } from './survey/survey/survey.component';
import { SurveyService } from './survey.service';
import { MedicalRecordViewComponent } from './medical-record-view/medical-record-view.component';
import { RegisterComponent } from './register/register.component';
import { PatientAppointmentCreationComponent } from './patient-appointment-creation/patient-appointment-creation.component';
import { Appointment4stepComponent } from './appointment4step/appointment4step.component';
import { AuthInterceptor } from './interceptor';
import { AuthGuard } from './auth.guard';
import { JwtHelperService, JWT_OPTIONS } from '@auth0/angular-jwt';
import { ReportComponent } from './report/report.component';

@NgModule({
  declarations: [
    AppComponent,
    PatientCreateFeedbackComponent,
    PatientFeedbackViewComponent,
    LoginComponent,
    HomePageComponent,
    SurveyComponent,
    MedicalRecordViewComponent,
    RegisterComponent,
    PatientAppointmentCreationComponent,
    Appointment4stepComponent,
    ReportComponent
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
    BrowserAnimationsModule,
    MatStepperModule,
    ReactiveFormsModule
  ],
  providers: [
    PatientCreateFeedbackService,
    PatientFeedbackServiceService,
    SurveyService,
    AuthGuard,
    {provide: JWT_OPTIONS, useValue: JWT_OPTIONS },
    JwtHelperService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
