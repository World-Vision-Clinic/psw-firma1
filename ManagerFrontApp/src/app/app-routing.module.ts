import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BuildingsMapComponent } from './buildings-map/buildings-map.component';
import { Hospital1Component } from './hospital1/hospital1.component';
import { FrontPageComponent } from './front-page/front-page.component';
import { PharmacyRegistrationComponent } from './pharmacy-registration/pharmacy-registration.component';
import { ManagerIntegrationFrontAppComponent } from './manager-integration-front-app/manager-integration-front-app.component';
import { ManagerFeedbackViewComponent } from './manager-feedback-view/manager-feedback-view.component';
import { OverviewObjectionsRepliesComponent } from './manager-integration-front-app/overview-objections-replies/overview-objections-replies.component';
import { ObjectionFormPageComponent } from './manager-integration-front-app/objection-form-page/objection-form-page.component';

const routes: Routes = [
  {path: "", component: FrontPageComponent},
  {path:"buildings", component: BuildingsMapComponent},
  {path:"hospital1", component: Hospital1Component},
  {path:"pharmacy-registration", component: PharmacyRegistrationComponent},
  {path:"manager-front-app", component: ManagerIntegrationFrontAppComponent, children: [
  {path: "overview-objections-replies", outlet: "showObjRepl", component:OverviewObjectionsRepliesComponent},
  {path: "create-objection", outlet: "showObjRepl", component:ObjectionFormPageComponent}
  ]},
  {path:"manager-feedback", component: ManagerFeedbackViewComponent}
  ];

@NgModule({
  imports: [
    RouterModule.forRoot(routes),
 
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
