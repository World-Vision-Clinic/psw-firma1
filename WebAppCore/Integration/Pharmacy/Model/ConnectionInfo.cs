using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Integration.Pharmacy.Model
{
    [Owned]
    public class ConnectionInfo
    {
        public string Key { get; private set; }
        public string Domain { get; private set; }
        public ProtocolType Protocol { get; private set; }

        private ConnectionInfo()
        { }

        public ConnectionInfo(string key, string domain, ProtocolType protocol)
        {
            if(key.Length>0 && domain.Length>0 && (protocol==ProtocolType.GRPC || protocol == ProtocolType.HTTP))
            {
                Key = key;
                Domain = domain;
                Protocol = protocol;
            }
            else
            {
                throw new Exception("Invalid connection info");
            }
        }

    }
}
