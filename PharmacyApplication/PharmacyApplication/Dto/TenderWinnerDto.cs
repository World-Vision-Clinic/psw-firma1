using Pharmacy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyAPI.Dto
{
    public class TenderWinnerDto
    {
        public int TenderId { get; set; }
        public string TenderHash { get; set; }
        public string HospitalName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public virtual List<TenderItem> TenderItems { get; set; }
        public DateTime? EndTime { get; set; }
        public virtual List<TenderOffer> TenderOffers { get; set; }

        public TenderWinnerDto() { }


    }
}
