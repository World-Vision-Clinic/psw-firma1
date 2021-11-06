import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BuildingsMapComponent } from './buildings-map/buildings-map.component';
import { Hospital1Component } from './hospital1/hospital1.component';
import { FrontPageComponent } from './front-page/front-page.component';
import { PharmacyRegistrationComponent } from './pharmacy-registration/pharmacy-registration.component';
import { ManagerObjectionsComponent } from './manager-objections/manager-objections.component';
import { OverviewObjectionsRepliesComponent } from './manager-objections/overview-objections-replies/overview-objections-replies.component';
import { ObjectionFormPageComponent } from './manager-objections/objection-form-page/objection-form-page.component';

const routes: Routes = [
  {path: "", component: FrontPageComponent},
  {path:"buildings", component: BuildingsMapComponent},
  {path:"hospital1", component: Hospital1Component},
  {path:"pharmacy-registration", component: PharmacyRegistrationComponent},
  {path:"manager-objections", component: ManagerObjectionsComponent, children: [
  {path: "overview-objections-replies", outlet: "showObjRepl", component:OverviewObjectionsRepliesComponent},
  {path: "create-objection", outlet: "showObjRepl", component:ObjectionFormPageComponent}
  ]}
  ];

@NgModule({
  imports: [
    RouterModule.forRoot(routes),
 
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
