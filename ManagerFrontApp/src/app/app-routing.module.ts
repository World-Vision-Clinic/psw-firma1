import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BuildingsMapComponent } from './buildings-map/buildings-map.component';
import { Hospital1Component } from './hospital1/hospital1.component';
import { FrontPageComponent } from './front-page/front-page.component';
import { PharmacyRegistrationComponent } from './pharmacy-registration/pharmacy-registration.component';
import { ManagerObjectionsComponent } from './manager-objections/manager-objections.component';
import { ManagerFeedbackViewComponent } from './manager-feedback-view/manager-feedback-view.component';

const routes: Routes = [
{path: "", component: FrontPageComponent},
{path:"buildings", component: BuildingsMapComponent},
{path:"hospital1", component: Hospital1Component},
{path:"pharmacy-registration", component: PharmacyRegistrationComponent},
{path:"manager-objections", component: ManagerObjectionsComponent},
{path:"manager-feedback", component: ManagerFeedbackViewComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
