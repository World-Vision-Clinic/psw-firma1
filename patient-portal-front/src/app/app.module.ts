import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { PatientFeedbackServiceService } from './patient-feedback-service.service';
import { PatientFeedbackViewComponent } from './patient-feedback-view/patient-feedback-view.component';
import { PatientCreateFeedbackComponent } from './patient-create-feedback/patient-create-feedback.component';
import { PatientCreateFeedbackService } from './patient-create-feedback.service';

@NgModule({
  declarations: [
    AppComponent,
    PatientFeedbackViewComponent,
    PatientCreateFeedbackComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    AppRoutingModule
  ],
  providers: [
    PatientCreateFeedbackService,
    PatientFeedbackServiceService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
