﻿using Integration.Pharmacy.Model;
using Renci.SshNet;
using System;

namespace Integration_API.Controller
{
    public class SftpHandler
    {
        const string SFTP_ADDRESS = "192.168.0.16";
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

                return new File { Name = localPath.Split("/")[1].Split(".")[0], Extension = localPath.Split("/")[1].Split(".")[1], Path = localPath };
            }
            catch
            {
                return null;
               
            }
        }

        public bool UploadFile()
        {
            try
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
            catch
            {
                return false;
            }

            return true;
        }

        public bool UploadPdfFile(String filename)
        {
            try
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
            catch
            {
                return false;
            }

            return true;
        }
    }
}
