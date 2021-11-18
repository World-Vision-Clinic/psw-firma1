using Pharmacy.Model;
using PharmacyAPI.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyAPI.Mapper
{
    public class NewsMapper
    {
        public static News NewsDtoToNews(NewsDto newsDto, String generatedId)
        {
            News news = new News();
            news.FromDate = newsDto.FromDate;
            news.ToDate = newsDto.ToDate;
            news.Title = newsDto.Title;
            news.Content = newsDto.Content;
            news.IdEncoded = generatedId;
            return news;
        }
    }
}
