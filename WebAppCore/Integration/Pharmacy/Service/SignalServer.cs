using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;


namespace Integration.Pharmacy.Service
{
    public class SignalServer : Hub
    {

        public async Task askServer(String someTextFromClient)
        {
            string tempString;
            if(someTextFromClient == "hey")
                tempString = "message was hey";
            else
                tempString = "message was something else";

            await Clients.Clients(this.Context.ConnectionId).SendAsync("askServerResponse", tempString);
        }

        public async Task notify()
        {
            await Clients.Clients(this.Context.ConnectionId).SendAsync("askServerResponse", "poslata notifikacija");
        }
    }
}
