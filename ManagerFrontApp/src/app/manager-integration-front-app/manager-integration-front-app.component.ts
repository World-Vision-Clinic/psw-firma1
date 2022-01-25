import { Component, OnDestroy, OnInit } from '@angular/core';
import { SignalService } from '../signalr.service';
import { BadgeModule } from './badge/badge.module';

@Component({
  selector: 'app-manager-integration-front-app',
  templateUrl: './manager-integration-front-app.component.html',
  styleUrls: ['./manager-integration-front-app.component.css']
})
export class ManagerIntegrationFrontAppComponent implements OnInit,OnDestroy {

  isNotificationVisible = false;
  constructor(public signalrServece: SignalService) { 
    
    this.list = signalrServece.list;
    this.list.lenght = signalrServece.list.lenght;
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

  showNotifications()
  {
    this.isNotificationVisible = !this.isNotificationVisible;
    if(this.isNotificationVisible == false)
    {
      for(let data of this.list)
      {
        data.seen = true;
      }
      this.signalrServece.numberOfNotifications = 0;

    }
  }

  addNotification(text:any)
  {
    this.list.push(text);
  }

}
