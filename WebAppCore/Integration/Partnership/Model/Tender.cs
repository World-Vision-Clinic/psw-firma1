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

        public DateTime? EndTime { get; set; }

        private readonly List<TenderItem> _tenderItems;
        public IReadOnlyCollection<TenderItem> TenderItems => _tenderItems;

        private readonly List<TenderOffer> _tenderOffers;
        public IReadOnlyCollection<TenderOffer> TenderOffers => _tenderOffers;

        public Tender()
        {
            _tenderItems = new List<TenderItem>();
            _tenderOffers = new List<TenderOffer>();
        }

        public Tender(int tenderId, string tenderHash, string title, string description, DateTime? endTime)
        {
            TenderId = tenderId;
            TenderHash = tenderHash;
            Title = title;
            Description = description;
            EndTime = endTime;
            _tenderItems = new List<TenderItem>();
            _tenderOffers = new List<TenderOffer>();
        }

        public void AddTenderItem(string medicineName, double dosage, int quantity)
        {
            TenderItem orderItem = new TenderItem(medicineName, dosage, quantity);

            _tenderItems.Add(orderItem);

        }

        public void AddTenderOffer(string tenderOfferHash, string tenderHash, string pharmacyName, double totalPrice, ICollection<OfferItem> offerItems, bool winner)
        {
            TenderOffer orderItem = new TenderOffer(tenderOfferHash, tenderHash, pharmacyName, totalPrice, offerItems, winner);

            _tenderOffers.Add(orderItem);

        }
    }
}
