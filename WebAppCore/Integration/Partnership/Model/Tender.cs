using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Integration.Partnership.Model
{
    public class Tender
    {
        public int TenderId { get; set; }
        public string TenderHash { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string WinningPharmacyName { get; set; }
        public ICollection<TenderItem> TenderItems { get; set; }
        public DateTime? EndTime { get; set; }
        public ICollection<TenderOffer> TenderOffers { get; set; }

    }
}
