using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Integration_API.Dto
{
    public class NewsDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public Boolean Posted { get; set; }
        public string PharmacyName { get; set; }

        public NewsDto()
        {   }

        public NewsDto(string id, string title, string content, DateTime fromDate, DateTime toDate, bool posted, string pharmacyName)
        {
            Id = id;
            Title = title;
            Content = content;
            FromDate = fromDate;
            ToDate = toDate;
            Posted = posted;
            PharmacyName = pharmacyName;
        }
    }
}
