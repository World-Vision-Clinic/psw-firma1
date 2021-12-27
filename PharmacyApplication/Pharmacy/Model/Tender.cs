using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Pharmacy.Model
{
    public class Tender
    {
        public int TenderId { get; set; }
        public string TenderHash { get; set; }
		public string HospitalName {get;set;}
        public string Title { get; set; }
        public string Description { get; set; }
        public virtual ICollection<TenderItem> TenderItems { get; set; }
        public DateTime? EndTime { get; set; }
        public virtual ICollection<TenderOffer> TenderOffers { get; set; }
		
    }
}
