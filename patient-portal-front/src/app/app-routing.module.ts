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

const routes: Routes = [
{ path: "create-feedback", component: PatientCreateFeedbackComponent },
{ path: "view-feedback", component: PatientFeedbackViewComponent },
{ path: "medical-record", component: MedicalRecordViewComponent },
{ path: "login", component: LoginComponent },
{ path: "survey", component: SurveyComponent},
{ path: "register", component: RegisterComponent },
{ path: "create-appointment", component: PatientAppointmentCreationComponent },
{ path: "appointment4step", component: Appointment4stepComponent },
{ path: '', component: HomePageComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }