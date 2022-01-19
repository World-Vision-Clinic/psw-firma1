import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BuildingsMapComponent } from './buildings-map/buildings-map.component';
import { Hospital1Component } from './hospital1/hospital1.component';
import { FrontPageComponent } from './front-page/front-page.component';
import { RoomComponent } from './room/room.component';
import { FormsModule } from '@angular/forms';
import { NgSelectModule } from '@ng-select/ng-select';
import { PharmacyRegistrationComponent } from './pharmacy-registration/pharmacy-registration.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ManagerIntegrationFrontAppComponent } from './manager-integration-front-app/manager-integration-front-app.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { OverviewObjectionsRepliesComponent } from './manager-integration-front-app/overview-objections-replies/overview-objections-replies.component';
import { ReactiveFormsModule } from '@angular/forms';
import { ObjectionFormPageComponent } from './manager-integration-front-app/objection-form-page/objection-form-page.component';
import { ManagerFeedbackViewComponent } from './manager-feedback-view/manager-feedback-view.component';
import { PharmaciesComponent } from './manager-integration-front-app/pharmacies/pharmacies.component';
import { NewsComponent } from './manager-integration-front-app/news/news.component';
import { ViewSurveyResultsComponent } from './view-survey-results/view-survey-results.component';
import { MedicineConsumptionComponent } from './manager-integration-front-app/medicine-consumption/medicine-consumption.component';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatNativeDateModule } from '@angular/material/core';
import { BlockPatientsComponent } from './block-patients/block-patients.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { ToastrModule } from 'ngx-toastr';
import { GetSpecificationComponent } from './manager-integration-front-app/get-specification/get-specification.component';
import { ViewFilesComponent } from './manager-integration-front-app/view-files/view-files.component';
import { LoginComponent } from './login/login.component';
import { AuthInterceptor } from './interceptor';
import { JwtHelperService, JWT_OPTIONS } from '@auth0/angular-jwt';
import { AuthGuard } from './auth.guard';
import { TenderCreationComponent } from './manager-integration-front-app/tender-creation/tender-creation.component';
import { NgChartsModule } from 'ng2-charts';
import { ChartAllModule, AccumulationChartAllModule, RangeNavigatorAllModule } from '@syncfusion/ej2-angular-charts';
import { StatisticsComponent } from './manager-integration-front-app/statistics/statistics.component';
import { TenderSelectionComponent } from './manager-integration-front-app/tender-selection/tender-selection.component';
import { DoctorsManagementComponent } from './doctors-management/doctors-management.component';
import { EventStatisticsComponent } from './event-statistics/event-statistics.component';

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
    ViewSurveyResultsComponent,
    BlockPatientsComponent,
    DashboardComponent,
    MedicineConsumptionComponent,
    NewsComponent,
    GetSpecificationComponent,
    ViewFilesComponent,
    LoginComponent,
    TenderCreationComponent,
    StatisticsComponent,
    TenderSelectionComponent,
    DoctorsManagementComponent,
    EventStatisticsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    BrowserAnimationsModule,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule,
    NgSelectModule,
    MatDatepickerModule,
    MatFormFieldModule,
    MatNativeDateModule,
    ToastrModule.forRoot(),
    BrowserModule,
    NgChartsModule,
    ChartAllModule,
    AccumulationChartAllModule,
    RangeNavigatorAllModule
    // MatDatepicker,
    // MatDateRangeInput,
    // MatDateRangePicker,
  ],
  providers: [
    {provide: JWT_OPTIONS, useValue: JWT_OPTIONS },
    AuthGuard,
    JwtHelperService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    }],
  bootstrap: [AppComponent],
})
export class AppModule {}