using Integration.Pharmacy.Model;
using Integration.Pharmacy.Repository.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Integration.Pharmacy.Service
{
    public class FilesService
    {

        private IFilesRepository repository;

        public FilesService(IFilesRepository filesRepository)
        {
            repository = filesRepository;
        }

        public void UpdateSpecification(File dowloadedSpec)
        {
            if(repository.GetByName(dowloadedSpec.Name) == null)
            {
                repository.Save(dowloadedSpec);
            }
        }
    }
}
