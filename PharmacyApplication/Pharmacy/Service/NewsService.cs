using Pharmacy.Model;
using Pharmacy.Repository.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.Service
{
    public class NewsService
    {
        INewsRepository newsRepository;

        public NewsService(INewsRepository repository)
        {
            newsRepository = repository;
        }

        public void Save(News news)
        {
            newsRepository.Save(news);
        }
    }
}
