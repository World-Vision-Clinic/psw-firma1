using Pharmacy.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.Repository.RepositoryInterfaces
{
    public interface IRepliesRepository
    {
        public List<Reply> GetAll();
        public void Save(Reply reply);
    }
}
