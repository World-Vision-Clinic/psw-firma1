import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PatientCreateFeedbackComponent } from './patient-create-feedback/patient-create-feedback.component';

const routes: Routes = [
{path: "create-feedback", component: PatientCreateFeedbackComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }