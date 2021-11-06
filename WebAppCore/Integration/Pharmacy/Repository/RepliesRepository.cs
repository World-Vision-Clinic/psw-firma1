using Integration.Pharmacy.Model;
using Integration.SharedModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Integration.Pharmacy.Repository
{
    class RepliesRepository
    {
        private IntegrationDbContext dbContext = new IntegrationDbContext();

        public void Save(Reply reply)
        {
            dbContext.Replies.Add(reply);
            dbContext.SaveChanges();
        }
    }
}
