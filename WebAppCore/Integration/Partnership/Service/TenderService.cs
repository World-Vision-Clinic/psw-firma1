using Integration.Partnership.Model;
using Integration.Partnership.Repository.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Integration.Partnership.Service
{
    public class TenderService
    {
        ITenderRepository tenderRepository;
        public TenderService(ITenderRepository repository)
        {
            tenderRepository = repository;
        }

        public void SaveTender(Tender tender)
        {
            tenderRepository.Save(tender);
        }

        public List<int> GetPharmacyWinningStatistic(string pharmacyName)
        {
            List<int> statistic = new List<int>();

            int numberOfTendersPharmacyParticipated = GetNumberOfTendersPharmacyParticipated(pharmacyName);

            int numberOfTendersPharmacyWon = GetNumberOfTendersPharmacyWon(pharmacyName);

            statistic.Add(numberOfTendersPharmacyParticipated - numberOfTendersPharmacyWon);
            statistic.Add(numberOfTendersPharmacyWon);

            return statistic;
        }

        public List<Tender> GetTendersPharmacyParticipated(string pharmacyName)
        {
            List<Tender> tendersPharmacyParticipated = new List<Tender>();
            foreach (Tender tender in tenderRepository.GetAll())
            {
                foreach (TenderOffer tenderOffer in tender.TenderOffers)
                {
                    if (tenderOffer.PharmacyName.Equals(pharmacyName))
                    {
                        tendersPharmacyParticipated.Add(tender);
                        break;
                    }
                }
            }
            return tendersPharmacyParticipated;
        }

        public List<TenderOffer> GetOffersForTender(string tenderHash, string pharmacyName)
        {
            List<TenderOffer> offersForTender = new List<TenderOffer>();
            foreach (Tender tender in tenderRepository.GetAll())
            {
                if (tender.TenderHash.Equals(tenderHash)) {
                    foreach(TenderOffer offer in tender.TenderOffers)
                    {
                        if (offer.PharmacyName.Equals(pharmacyName))
                        {
                            offersForTender.Add(offer);
                        }
                    }
                }
            }
            return offersForTender;
        }

        public List<TenderOffer> GetWinningOffersForPharmacy(string pharmacyName)
        {
            List<TenderOffer> winningOffers = new List<TenderOffer>();
            foreach (Tender tender in tenderRepository.GetAll())
            {
                foreach (TenderOffer offer in tender.TenderOffers)
                {
                    if (offer.PharmacyName.Equals(pharmacyName) && offer.Winner)
                    {
                        winningOffers.Add(offer);
                        break;
                    }
                }
            }
            
            return winningOffers;
        }

        public string GetTenderName(string offerHash)
        {
            string tenderName = "";
            foreach (Tender tender in tenderRepository.GetAll())
            {
                foreach (TenderOffer offer in tender.TenderOffers)
                {
                    if (offer.TenderOfferHash.Equals(offerHash))
                    {
                        return tender.Title;
                    }
                }
            }
            return tenderName;
        }

        private int GetNumberOfTendersPharmacyParticipated(string pharmacyName)
        {
            int numberOfTendersPharmacyParticipated = 0;
            foreach (Tender tender in tenderRepository.GetAll())
            {
                foreach (TenderOffer tenderOffer in tender.TenderOffers)
                {
                    if (tenderOffer.PharmacyName.Equals(pharmacyName))
                    {
                        numberOfTendersPharmacyParticipated += 1;
                        break;
                    }
                }
            }
            return numberOfTendersPharmacyParticipated;
        }

        private int GetNumberOfTendersPharmacyWon(string pharmacyName)
        {
            int numberOfTendersPharmacyWon = 0;

            foreach (Tender tender in tenderRepository.GetAll())
            {
                foreach(TenderOffer tenderOffer in tender.TenderOffers)
                {
                    if (tenderOffer.Winner && tenderOffer.PharmacyName.Equals(pharmacyName))
                    {
                        numberOfTendersPharmacyWon += 1;
                        break;
                    }
                }
            }

            return numberOfTendersPharmacyWon;
        }

    }
}
