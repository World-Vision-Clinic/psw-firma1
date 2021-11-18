using Hospital_API;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace HospitalTests.Patient
{
    class ClientProvider
    {
        private TestServer Server;
        public HttpClient Client { get; set; }

        public ClientProvider() {
            Server = new TestServer(new WebHostBuilder().UseStartup<Startup>());

            Client = Server.CreateClient();
        }

        public void Dispose (){
            Server?.Dispose();
            Client?.Dispose();
        }
    
    }
}
