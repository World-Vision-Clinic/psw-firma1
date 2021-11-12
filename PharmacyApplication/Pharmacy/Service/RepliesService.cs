using Pharmacy.Model;
using Pharmacy.Repository.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.Service
{
    public class RepliesService
    {
        IRepliesRepository repository;

        public RepliesService(IRepliesRepository repliesRepository)
        {
            repository = repliesRepository;
        }

        public void AddNewReply(Reply newReply)
        {
            repository.Save(newReply);
        }
    }
}
