import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { PatientCreateFeedbackComponent } from './patient-create-feedback/patient-create-feedback.component';
import { PatientCreateFeedbackService } from './patient-create-feedback.service';
import { PatientFeedbackServiceService } from './patient-feedback-service.service';
import { PatientFeedbackViewComponent } from './patient-feedback-view/patient-feedback-view.component';
import { LoginComponent } from './login/login.component';

@NgModule({
  declarations: [
    AppComponent,
    PatientCreateFeedbackComponent,
    PatientFeedbackViewComponent,
    LoginComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    AppRoutingModule,
    BrowserAnimationsModule
  ],
  providers: [
    PatientCreateFeedbackService,
    PatientFeedbackServiceService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
