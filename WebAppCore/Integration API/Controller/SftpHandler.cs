using Integration.Pharmacy.Repository;
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
        FilesRepository repository = new FilesRepository();
        public bool DownloadSpecification(string fromPath, string localPath)
        {
            try
            {
                using (SftpClient client = new SftpClient(new PasswordConnectionInfo("192.168.0.22", "user", "password")))
                {
                    client.Connect();

                    Directory.CreateDirectory("Specifications");  // create directory

                    string serverFile = @fromPath;
                    string localFile = @localPath;
                    using (Stream stream = System.IO.File.OpenWrite(localFile))
                    {
                        client.DownloadFile(serverFile, stream);
                    }

                    client.Disconnect();
                }

                Integration.Pharmacy.Model.File file = new Integration.Pharmacy.Model.File { Name = localPath.Split("/")[1].Split(".")[0], Extension = localPath.Split("/")[1].Split(".")[1], Path = localPath };
                repository.Save(file);

                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return false;
               
            }
        }

        public void UploadFile()
        {
            using (SftpClient client = new SftpClient(new PasswordConnectionInfo("192.168.0.28", "user", "password")))
            {
                client.Connect();
                string sourceFile = @"consumed-medicine.txt";
                using (Stream stream = System.IO.File.OpenRead(sourceFile))
                {
                    client.UploadFile(stream, @"\public\" + Path.GetFileName(sourceFile), x => { Console.WriteLine(x); });
                }
                client.Disconnect();
            }
        }
    }
}
