﻿using Integration.Pharmacy.Model;
using Integration.Pharmacy.Repository;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Integration_API.Controller
{
    public class SftpHandler
    {
        const String SFTP_ADDRESS = "192.168.56.1";
        FilesRepository repository = new FilesRepository();
        public File DownloadSpecification(string fromPath, string localPath)
        {
            try
            {

                using (SftpClient client = new SftpClient(new PasswordConnectionInfo(SFTP_ADDRESS, "user", "password")))
                {
                    client.Connect();

                    System.IO.Directory.CreateDirectory("Specifications");  // create directory

                    string serverFile = @fromPath;
                    string localFile = @localPath;
                    using (System.IO.Stream stream = System.IO.File.OpenWrite(localFile))
                    {
                        client.DownloadFile(serverFile, stream);
                    }

                    client.Disconnect();
                }

                File file = new Integration.Pharmacy.Model.File { Name = localPath.Split("/")[1].Split(".")[0], Extension = localPath.Split("/")[1].Split(".")[1], Path = localPath };

                return file;
            }
            catch
            {
                return null;
               
            }
        }

        public void UploadFile()
        {

            using (SftpClient client = new SftpClient(new PasswordConnectionInfo(SFTP_ADDRESS, "user", "password")))
            {
                client.Connect();
                string sourceFile = @"Reports\ConsumedMedicineReport.pdf";
                using (System.IO.Stream stream = System.IO.File.OpenRead(sourceFile))
                {
                    client.UploadFile(stream, @"\public\" + System.IO.Path.GetFileName(sourceFile), x => { Console.WriteLine(x); });
                }
                client.Disconnect();
            }
        }

        public void UploadPdfFile(String filename)
        {
            using (SftpClient client = new SftpClient(new PasswordConnectionInfo(SFTP_ADDRESS, "user", "password")))
            {
                client.Connect();
                string sourceFile = filename;
                using (System.IO.Stream stream = System.IO.File.OpenRead(sourceFile))
                {
                    client.UploadFile(stream, @"\public\" + System.IO.Path.GetFileName(sourceFile), x => { Console.WriteLine(x); });
                }
                client.Disconnect();
            }
        }
    }
}
