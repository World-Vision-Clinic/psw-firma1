using Integration.Pharmacy.Model;
using Integration.Pharmacy.Repository.RepositoryInterfaces;
using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Timers;

namespace Integration.Pharmacy.Service
{
    public class FilesService
    {

        private IFilesRepository repository;

        public FilesService(IFilesRepository filesRepository)
        {
            repository = filesRepository;
        }

        public void SaveFile(Model.File file)
        {
            repository.Save(file);
        }

        public void UpdateSpecification(Model.File dowloadedSpec)
        {
            if (repository.GetByName(dowloadedSpec.Name) == null)
            {
                repository.Save(dowloadedSpec);
            }
        }

        public static void CompressFiles(object sender, ElapsedEventArgs e)
        {
            string[] filePaths = GetFilePathsForCompression();

            if(filePaths.Length != 0)
                Compress(filePaths, DateTime.Now.Month + "-" + DateTime.Now.Day + "-" + DateTime.Now.Year + ".zip");

            DeleteFiles(filePaths);
        }

        public static string[] GetFilePathsForCompression()
        {
            string[] reports = GetFilesPathsFromDirectory(@"Reports\");
            string[] prescriptions = GetFilesPathsFromDirectory(@"Prescriptions\");
            string[] filePaths = { };

            if (reports.Length != 0 && prescriptions.Length != 0)
                filePaths = reports.Union(prescriptions).ToArray();
            else if (reports.Length != 0)
                filePaths = reports;
            else if (prescriptions.Length != 0)
                filePaths = prescriptions;
            return filePaths;
        }

        public static string[] GetFilesPathsFromDirectory(string directory)
        {
            string[] pdfs = Directory.GetFiles(directory, "*.pdf", SearchOption.AllDirectories);
            string[] txts = Directory.GetFiles(directory, "*.txt", SearchOption.AllDirectories);
            string[] paths = pdfs.Union(txts).ToArray();
            return paths;
        }

        public static void Compress(string[] filePaths, string zipName)
        {
            using (ZipFile zip = new ZipFile())
            {
                foreach (string path in filePaths)
                {
                    if (System.IO.File.GetLastWriteTime(path) < DateTime.Now)
                    {
                        zip.AddFile(path, DateTime.Now.Month + "-" + DateTime.Now.Day + "-" + DateTime.Now.Year);
                    }
                }

                zip.Save(zipName);
            }
        }

        public static void DeleteFiles(string[] filePaths)
        {
            foreach (string path in filePaths)
            {
                if (System.IO.File.GetLastWriteTime(path) < DateTime.Now)
                {
                    System.IO.File.Delete(path);
                }
            }
        }
    }
}
