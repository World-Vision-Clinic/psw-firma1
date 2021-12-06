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
        public bool DownloadSpecification(string path)
        {
            try
            {
                using (SftpClient client = new SftpClient(new PasswordConnectionInfo("192.168.0.22", "user", "password")))
                {
                    client.Connect();

                    string serverFile = @path;
                    string localFile = @"Specification.pdf";
                    using (Stream stream = System.IO.File.OpenWrite(localFile))
                    {
                        client.DownloadFile(serverFile, stream);
                    }

                    client.Disconnect();
                }

                Integration.Pharmacy.Model.File file = new Integration.Pharmacy.Model.File { Name = "Specification", Extension = "pdf", Path = "Specification.pdf" };
                repository.Save(file);

                return true;
            }
            catch
            {
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

        public bool fileExists(string path)
        {
            return File.Exists(@path);
        }
    }
}
