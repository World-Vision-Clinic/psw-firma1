using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.RepositoryInterfaces
{
        public interface IGenericRepository<T>
         where T : class
    {
        List<T> GetAll();
        void Save(T parameter);
        void Delete(int id);
        T GetByID(int id);

        void Update(T parameter);

        bool Exists(int parameter);

    }
}
