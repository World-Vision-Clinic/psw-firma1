using Integration.Pharmacy.Model;
using Integration_API.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Integration_API.Mapper
{
    public class NewsMapper
    {
        public static NewsDto NewsToNewsDto(News news)
        {
            NewsDto dto = new NewsDto();
            dto.Id = news.IdEncoded;
            dto.Content = news.Content;
            dto.FromDate = news.DateRange.FromDate;
            dto.ToDate = news.DateRange.ToDate;
            dto.Title = news.Title;
            dto.Posted = news.Posted;
            dto.PharmacyName = news.PharmacyName;
            return dto;
        }
    }
}
