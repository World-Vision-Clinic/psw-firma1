using Integration.Pharmacy.Model;
using Integration.SharedModel;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public List<Reply> GetObjectionReplies(string objectionId)
        {
            List<Reply> replies = new List<Reply>();
            List<Reply> objectionReplies = new List<Reply>();
            dbContext.Replies.ToList().ForEach(reply => replies.Add(reply));
            foreach(Reply reply in replies)
            {
                if (reply.ObjectionId.Equals(objectionId))
                {
                    objectionReplies.Add(reply);
                }
            }
            return objectionReplies;
        }
    }
}
