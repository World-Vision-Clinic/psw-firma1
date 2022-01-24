import { Injectable } from "@angular/core";
import * as signalR from '@aspnet/signalr';
import { ManagerIntegrationFrontAppComponent } from "./manager-integration-front-app/manager-integration-front-app.component";

@Injectable ({providedIn: 'root'})

export class SignalService {

    constructor(
        ) { }

    numberOfNotifications = 0;
    hubConnection:any;
    list: any=[];
    startConnection = () => {
        this.hubConnection = new signalR.HubConnectionBuilder()
        .withUrl('http://localhost:43818/signalServer', {
            skipNegotiation: true,
            transport: signalR.HttpTransportType.WebSockets
        })
        .build();
    
        this.hubConnection
        .start()
        .then(() => {
            console.log('Hub Connection Started!');
        })
        .catch(err => console.log('Error while starting connection: ' + err))
    }


    askServer() {
        this.hubConnection.invoke("askServer", "hey")
            .catch(err => console.error(err));
    }
    
    askServerListener() {
        this.hubConnection.on("askServerResponse", (someText) => {
            if(someText=='message was hey')
            {
                console.log(someText);
                return;
            }
            console.log(someText);
            
            //this.list = [{text:someText, seen:false}].concat(this.list);
            this.list.unshift({text:someText, seen:false});
            //this.list.push({text:someText, seen:false});
            this.numberOfNotifications = this.numberOfNotifications+1;
        })
    }

}