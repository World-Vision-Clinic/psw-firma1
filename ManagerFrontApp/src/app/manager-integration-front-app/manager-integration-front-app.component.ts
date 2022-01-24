import { Component, OnDestroy, OnInit } from '@angular/core';
import { SignalService } from '../signalr.service';
import { BadgeModule } from './badge/badge.module';

@Component({
  selector: 'app-manager-integration-front-app',
  templateUrl: './manager-integration-front-app.component.html',
  styleUrls: ['./manager-integration-front-app.component.css']
})
export class ManagerIntegrationFrontAppComponent implements OnInit,OnDestroy {

  constructor(public signalrServece: SignalService) { 
    this.list = [1,2,3];
  }
  list: any=[];
  ngOnInit()
  {
    
    this.signalrServece.startConnection();
    setTimeout(() => {
      this.signalrServece.askServerListener();
      this.signalrServece.askServer();
    }, 2000);
  }

  ngOnDestroy(){
      this.signalrServece.hubConnection.off("askServerResponse");
  }

}
