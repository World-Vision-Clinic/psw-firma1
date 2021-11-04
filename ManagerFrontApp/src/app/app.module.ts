import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BuildingsMapComponent } from './buildings-map/buildings-map.component';
import { Hospital1Component } from './hospital1/hospital1.component';
import { FrontPageComponent } from './front-page/front-page.component';
import { RoomComponent } from './room/room.component';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    AppComponent,
    BuildingsMapComponent,
    Hospital1Component,
    FrontPageComponent,
    RoomComponent,

  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
