using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Integration_API.Controller
{
    public class SftpHandler
    {
        public bool DownloadSpecification(string path)
        {
            try
            {
                using (SftpClient client = new SftpClient(new PasswordConnectionInfo("192.168.0.21", "user", "password")))
                {
                    client.Connect();

                    string serverFile = @path;
                    string localFile = @"Specification.txt";
                    using (Stream stream = System.IO.File.OpenWrite(localFile))
                    {
                        client.DownloadFile(serverFile, stream);
                    }

                    client.Disconnect();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool fileExists(string path)
        {
            return File.Exists(@path);
        }
    }
}
