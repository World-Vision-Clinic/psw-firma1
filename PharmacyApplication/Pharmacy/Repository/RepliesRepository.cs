using Pharmacy.Model;
using Pharmacy.Repository.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pharmacy.Repository
{
    public class RepliesRepository : IRepliesRepository
    {
        private PharmacyDbContext dbContext = new PharmacyDbContext();
        public List<Reply> GetAll()
        {
            List<Reply> replies = new List<Reply>();
            dbContext.Replies.ToList().ForEach(reply => replies.Add(reply));
            return replies;
        }

        public void Save(Reply reply)
        {
            dbContext.Replies.Add(reply);
            dbContext.SaveChanges();
        }
    }
}
