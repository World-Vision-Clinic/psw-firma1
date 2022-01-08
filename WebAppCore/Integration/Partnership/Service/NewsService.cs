using Integration.Pharmacy.Model;
using Integration.Pharmacy.Repository.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Integration.Pharmacy.Service
{
    public class NewsService
    {
        INewsRepository newsRepository;

        public NewsService(INewsRepository repository)
        {
            newsRepository = repository;
        }

        public List<News> GetAll()
        {
            return newsRepository.GetAll();
        }

        public void Update(News pieceOfNews)
        {
            pieceOfNews.ChangeStatus();
            newsRepository.Update();
        }
    }
}
