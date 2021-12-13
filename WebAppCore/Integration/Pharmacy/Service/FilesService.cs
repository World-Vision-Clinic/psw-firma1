using Integration.Pharmacy.Model;
using Integration.Pharmacy.Repository.RepositoryInterfaces;
using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
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

        public void UpdateSpecification(Model.File dowloadedSpec)
        {
            if(repository.GetByName(dowloadedSpec.Name) == null)
            {
                repository.Save(dowloadedSpec);
            }
        }

        public static void CompressFiles(object sender, ElapsedEventArgs e)
        {
            using (ZipFile zip = new ZipFile())
            {
                string[] filePaths = Directory.GetFiles(@"Specifications\", "*.pdf", SearchOption.AllDirectories);

                foreach (string path in filePaths)
                {
                    if (System.IO.File.GetLastWriteTime(path) < DateTime.Now)
                    {
                        zip.AddFile(path, DateTime.Now.Month + "-" + DateTime.Now.Day + "-" + DateTime.Now.Year);
                    }
                }

                string zipName = DateTime.Now.Month + "-" + DateTime.Now.Day + "-" + DateTime.Now.Year + ".zip";
                zip.Save(zipName);

                DeleteFiles(filePaths);
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
