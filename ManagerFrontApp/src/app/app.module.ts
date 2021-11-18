import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BuildingsMapComponent } from './buildings-map/buildings-map.component';
import { Hospital1Component } from './hospital1/hospital1.component';
import { FrontPageComponent } from './front-page/front-page.component';
import { RoomComponent } from './room/room.component';
import { FormsModule } from '@angular/forms';
import { PharmacyRegistrationComponent } from './pharmacy-registration/pharmacy-registration.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ManagerIntegrationFrontAppComponent } from './manager-integration-front-app/manager-integration-front-app.component';
import { HttpClientModule } from '@angular/common/http';
import { OverviewObjectionsRepliesComponent } from './manager-integration-front-app/overview-objections-replies/overview-objections-replies.component'; 
import { ReactiveFormsModule } from '@angular/forms';
import { ObjectionFormPageComponent } from './manager-integration-front-app/objection-form-page/objection-form-page.component';
import { ManagerFeedbackViewComponent } from './manager-feedback-view/manager-feedback-view.component';
import { PharmaciesComponent } from './manager-integration-front-app/pharmacies/pharmacies.component';



@NgModule({
  declarations: [
    AppComponent,
    BuildingsMapComponent,
    Hospital1Component,
    FrontPageComponent,
    RoomComponent,
    PharmacyRegistrationComponent,
    ManagerIntegrationFrontAppComponent,
    OverviewObjectionsRepliesComponent,
    ObjectionFormPageComponent,
    ManagerFeedbackViewComponent,
    ManagerIntegrationFrontAppComponent,
    PharmaciesComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    BrowserAnimationsModule,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
