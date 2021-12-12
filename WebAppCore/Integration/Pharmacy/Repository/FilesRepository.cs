using Integration.Pharmacy.Model;
using Integration.Pharmacy.Repository.RepositoryInterfaces;
using Integration.SharedModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Integration.Pharmacy.Repository
{
    public class FilesRepository : IFilesRepository
    {
        IntegrationDbContext dbContext = new IntegrationDbContext();
        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public List<File> GetAll()
        {
            return dbContext.Files.ToList();
        }

        public File GetByID(string id)
        {
            throw new NotImplementedException();
        }

        public File GetByName(string fileName)
        {
            return dbContext.Files.Where(f => f.Name == fileName).SingleOrDefault();
        }

        public void Save(File parameter)
        {
            dbContext.Files.Add(parameter);
            dbContext.SaveChanges();
        }

        public void DeleteByPath(string path)
        {
            File file = dbContext.Files.Where(f => f.Path == path).SingleOrDefault();
            dbContext.Files.Remove(file);
            dbContext.SaveChanges();
        }
    }
}
