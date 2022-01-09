using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyAPI.Dto
{
    public class NewsDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public NewsDto()
        { }

        public NewsDto(string title, string content, DateTime fromDate, DateTime toDate)
        {
            Title = title;
            Content = content;
            FromDate = fromDate;
            ToDate = toDate;
        }
    }
}