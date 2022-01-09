using Pharmacy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyAPI.Dto
{
    public class NewsToSendDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateRange DateRange { get; set; }

        public string IdEncoded { get; set; }

        public NewsToSendDto() { }
        public NewsToSendDto(string title, string content, DateRange dateRange, string idEncoded)
        {
            Title = title;
            Content = content;
            DateRange = dateRange;
            IdEncoded = idEncoded;
        }
    }
}
