import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { HomePageComponent } from './homepage/homepage.component';
import { RegisterComponent } from './register/register.component';
import { PatientCreateFeedbackComponent } from './patient-create-feedback/patient-create-feedback.component';
import { PatientFeedbackViewComponent } from './patient-feedback-view/patient-feedback-view.component';
import { SurveyComponent } from './survey/survey/survey.component';
import { MedicalRecordViewComponent } from './medical-record-view/medical-record-view.component';
import { PatientAppointmentCreationComponent } from './patient-appointment-creation/patient-appointment-creation.component';
import { Appointment4stepComponent } from './appointment4step/appointment4step.component';
import { ReportComponent } from './report/report.component';
import { AuthGuard } from './auth.guard';

const routes: Routes = [


{ path: "homepage", component: MedicalRecordViewComponent },
{ path: "create-feedback", component: PatientCreateFeedbackComponent ,canActivate:[AuthGuard]},
{ path: "view-feedback", component: PatientFeedbackViewComponent ,canActivate:[AuthGuard]},
{ path: "medical-record", component: MedicalRecordViewComponent ,canActivate:[AuthGuard]},
{ path: "login", component: LoginComponent },
{ path: "survey/:id", component: SurveyComponent,canActivate:[AuthGuard]},
{ path: "register", component: RegisterComponent },
{ path: "create-appointment", component: PatientAppointmentCreationComponent ,canActivate:[AuthGuard]},
{ path: "appointment4step", component: Appointment4stepComponent ,canActivate:[AuthGuard]},
{ path: '', component: HomePageComponent},
{ path: 'report', component: ReportComponent, canActivate:[AuthGuard]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }