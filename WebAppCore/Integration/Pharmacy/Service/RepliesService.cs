using Integration.Pharmacy.Model;
using Integration.Pharmacy.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Integration.Pharmacy.Service
{
    public class RepliesService
    {
        RepliesRepository repository = new RepliesRepository();
        public void AddNewReply(Reply newReply)
        {
            repository.Save(newReply);
        }
    }
}
