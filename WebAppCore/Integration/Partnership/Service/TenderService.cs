using Integration.Partnership.Repository.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Integration.Partnership.Service
{
    public class TenderService
    {
        ITenderRepository tenderRepository;
        public TenderService(ITenderRepository repository)
        {
            tenderRepository = repository;
        }
    }
}
