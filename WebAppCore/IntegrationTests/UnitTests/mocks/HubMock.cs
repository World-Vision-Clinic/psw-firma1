using Integration.Pharmacy.Service;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationTests.UnitTests.mocks
{
    class HubMock : IHubContext<SignalServer>
    {
        public IHubClients Clients => throw new NotImplementedException();

        public IGroupManager Groups => throw new NotImplementedException();
    }
}
