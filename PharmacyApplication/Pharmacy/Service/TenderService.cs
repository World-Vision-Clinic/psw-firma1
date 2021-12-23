using Pharmacy.Model;
using Pharmacy.Repository.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.Service
{
   public class TenderService
    {
        ITendersRepository tenderRepository;
        public TenderService(ITendersRepository tendersRepository)
        {
            this.tenderRepository = tendersRepository;
        }

        public Tender GetTenderById(string id)
        {
            return tenderRepository.GetById(id);
        }
    }
}
