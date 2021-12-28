import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BuildingsMapComponent } from './buildings-map/buildings-map.component';
import { Hospital1Component } from './hospital1/hospital1.component';
import { FrontPageComponent } from './front-page/front-page.component';
//import { PharmacyRegistrationComponent } from './pharmacy-registration/pharmacy-registration.component';
//import { ManagerIntegrationFrontAppComponent } from './manager-integration-front-app/manager-integration-front-app.component';
import { ManagerFeedbackViewComponent } from './manager-feedback-view/manager-feedback-view.component';
//import { OverviewObjectionsRepliesComponent } from './manager-integration-front-app/overview-objections-replies/overview-objections-replies.component';
//import { ObjectionFormPageComponent } from './manager-integration-front-app/objection-form-page/objection-form-page.component';
//import { PharmaciesComponent } from './manager-integration-front-app/pharmacies/pharmacies.component';
//import { NewsComponent } from './manager-integration-front-app/news/news.component';
import { ViewSurveyResultsComponent } from './view-survey-results/view-survey-results.component';
//import { MedicineConsumptionComponent } from './manager-integration-front-app/medicine-consumption/medicine-consumption.component';
//import { GetSpecificationComponent } from './manager-integration-front-app/get-specification/get-specification.component';
//import { ViewFilesComponent } from './manager-integration-front-app/view-files/view-files.component';
import { BlockPatientsComponent } from './block-patients/block-patients.component';
import { DashboardComponent } from './dashboard/dashboard.component';
//import { MedicineConsumptionComponent } from './manager-integration-front-app/medicine-consumption/medicine-consumption.component';
import { GetSpecificationComponent } from './manager-integration-front-app/get-specification/get-specification.component';
import { ViewFilesComponent } from './manager-integration-front-app/view-files/view-files.component';
import { LoginComponent } from './login/login.component';
import { AuthGuard } from './auth.guard';
import { TenderCreationComponent } from './manager-integration-front-app/tender-creation/tender-creation.component';
import { ManagerIntegrationFrontAppComponent } from './manager-integration-front-app/manager-integration-front-app.component';

const routes: Routes = [
  {path: "", component: FrontPageComponent},
  {path:"buildings", component: BuildingsMapComponent},
  {path:"hospital/:hospitalId", component: Hospital1Component},
  //{path:"pharmacy-registration", component: PharmacyRegistrationComponent},
  {path:"survey-results", component: ViewSurveyResultsComponent, canActivate:[AuthGuard]},
  {path:"homepage", component: DashboardComponent},
  //{path:"manager-front-app", component: ManagerIntegrationFrontAppComponent, children: [
  //{path: "overview-objections-replies", outlet: "showObjRepl", component:OverviewObjectionsRepliesComponent},
  //{path: "overview-pharmacies", outlet: "showObjRepl", component:PharmaciesComponent},

  //{path: "create-objection", outlet: "showObjRepl", component:ObjectionFormPageComponent},
  //{path: "news", outlet: "showObjRepl", component:NewsComponent},
  //{path: "medicine-consumption-report", outlet: "showObjRepl", component:MedicineConsumptionComponent},
  //{path: "get-specification", outlet:"showObjRepl", component: GetSpecificationComponent},
  //{path: "view-files", outlet:"showObjRepl", component: ViewFilesComponent}
  //]},
  {path:"manager-feedback", component: ManagerFeedbackViewComponent, canActivate:[AuthGuard]},
  {path:"block-patients", component: BlockPatientsComponent, canActivate:[AuthGuard]},
  //{path:"pharmacy-registration", component: PharmacyRegistrationComponent},
  //{path:"survey-results", component: ViewSurveyResultsComponent,canActivate:[AuthGuard]},
  {path:"manager-front-app", component: ManagerIntegrationFrontAppComponent, children: [
  //{path: "overview-objections-replies", outlet: "showObjRepl", component:OverviewObjectionsRepliesComponent},
  //{path: "overview-pharmacies", outlet: "showObjRepl", component:PharmaciesComponent},

  //{path: "create-objection", outlet: "showObjRepl", component:ObjectionFormPageComponent},
  //{path: "news", outlet: "showObjRepl", component:NewsComponent},
  //{path: "medicine-consumption-report", outlet: "showObjRepl", component:MedicineConsumptionComponent},
  {path: "get-specification", outlet:"showObjRepl", component: GetSpecificationComponent},
  {path: "view-files", outlet:"showObjRepl", component: ViewFilesComponent},
  {path: "create-tender", outlet:"showObjRepl", component:TenderCreationComponent}
  ]},
  {path:"manager-feedback", component: ManagerFeedbackViewComponent, canActivate:[AuthGuard]},
  {path:"login", component: LoginComponent}
  ];

@NgModule({
  imports: [
    RouterModule.forRoot(routes),
 
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
