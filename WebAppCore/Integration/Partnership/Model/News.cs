using System;
using System.Collections.Generic;
using System.Text;

namespace Integration.Pharmacy.Model
{
    public class News
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string IdEncoded { get; set; }

        public Boolean Posted { get; set; }
        public string PharmacyName {get;set;}

        public News()
        { }

        public News(int id, string title, string content, DateTime fromDate, DateTime toDate, string idEncoded, bool posted, string pharmacyName)
        {
            Id = id;
            Title = title;
            Content = content;
            FromDate = fromDate;
            ToDate = toDate;
            IdEncoded = idEncoded;
            Posted = posted;
            PharmacyName = pharmacyName;
        }
    }
}
