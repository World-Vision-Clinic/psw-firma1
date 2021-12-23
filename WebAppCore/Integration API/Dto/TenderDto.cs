using Integration.Partnership.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Integration_API.Dto
{
    public class TenderDto
    {
        public TenderDto()
        {
        }

        public string TenderHash { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<TenderItem> TenderItems { get; set; }
        public DateTime? EndTime { get; set; }
        public ICollection<TenderOffer> TenderOffers { get; set; }

        public TenderDto(string tenderHash, string title, string description, ICollection<TenderItem> tenderItems, DateTime? endTime, ICollection<TenderOffer> tenderOffers)
        {
            TenderHash = tenderHash;
            Title = title;
            Description = description;
            TenderItems = tenderItems;
            EndTime = endTime;
            TenderOffers = tenderOffers;
        }

    }
}
