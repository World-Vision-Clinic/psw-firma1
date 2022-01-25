using Integration.Partnership.Model;
using Integration.Partnership.Repository.RepositoryInterfaces;
using Integration.Pharmacy.Model;
using Integration.Pharmacy.Repository;
using Integration.Pharmacy.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Integration.Partnership.Service
{
    public class TenderService
    {
        ITenderRepository tenderRepository;
        PharmaciesService pharmaciesService = new PharmaciesService(new PharmaciesRepository());
        public TenderService(ITenderRepository repository)
        {
            tenderRepository = repository;
        }

        public List<Tender> GetTendersWithOffers()
        {
            return tenderRepository.GetAll();
        }
        public List<Tender> GetTenders()
        {
            return tenderRepository.GetAll();
        }

        public void EditTenderEndTimeByHash(Tender tender)
        {
            tenderRepository.EditTenderEndTimeByHash(tender);
        }

        public void EditTenderOfferById(TenderOffer offer)
        {
            tenderRepository.EditTenderOfferById(offer);
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

        public Tender GetByTenderHash(string id)
        {
            return tenderRepository.GetByTenderHash(id);
        }

        public TenderOffer GetTenderOfferWithOfferItems(string pharmacyName, string offerHash)
        {
            return tenderRepository.GetTenderOfferWithOfferItems(pharmacyName, offerHash);
        }

        public Dictionary<string, List<float>> GetNumberOfOffersForAllTenders(DateTime start, DateTime end)
        {
            int tenderNumber = 0;
            Dictionary<string, List<float>> data = new Dictionary<string, List<float>>();

            foreach (PharmacyProfile ph in pharmaciesService.GetAll())
                data.Add(ph.Name, new List<float>());

            foreach (Tender t in GetTendersWithOffers())
            {
                if (t.EndTime < start || t.EndTime > end)
                    continue;

                List<string> pharmacies = new List<string>();
                string pharmacyName = "";
                for (int j = 0; j < t.TenderOffers.Count; j++)
                {
                    pharmacyName = t.TenderOffers.ElementAt(j).PharmacyName;
                    if (!pharmacies.Contains(pharmacyName))
                    {
                        pharmacies.Add(pharmacyName);
                        data[pharmacyName].Add(1);
                        continue;
                    }

                    float number = data[pharmacyName][tenderNumber];
                    data[pharmacyName][tenderNumber] = ++number;
                }

                foreach (string k in data.Keys)
                {
                    if (!pharmacies.Contains(k))
                        data[k].Add(0);
                }
                tenderNumber++;
            }
            return data;
        }

        public Dictionary<string, List<float>> GetMaxPricesForAllTenders(DateTime start, DateTime end)
        {
            int tenderNumber = 0;
            Dictionary<string, List<float>> data = new Dictionary<string, List<float>>();

            foreach (PharmacyProfile ph in pharmaciesService.GetAll())
                data.Add(ph.Name, new List<float>());

            foreach (Tender t in GetTendersWithOffers())
            {
                if (t.EndTime < start || t.EndTime > end)
                    continue;

                List<string> pharmacies = new List<string>();
                string pharmacyName = "";
                for (int j = 0; j < t.TenderOffers.Count; j++)
                {
                    pharmacyName = t.TenderOffers.ElementAt(j).PharmacyName;
                    double price = 0.0;
                    if (!pharmacies.Contains(pharmacyName))
                    {

                        pharmacies.Add(pharmacyName);
                        price = t.TenderOffers.ElementAt(j).TotalPrice;
                        data[pharmacyName].Add((float)(price));
                        continue;
                    }

                    float maxPrice = data[pharmacyName][tenderNumber];
                    if (price > maxPrice)
                        data[pharmacyName][tenderNumber] = (float)price;
                }

                foreach (string k in data.Keys)
                {
                    if (!pharmacies.Contains(k))
                        data[k].Add(0);
                }
                tenderNumber++;
            }
            return data;
        }

        public Dictionary<string, List<float>> GetMinPricesForAllTenders(DateTime start, DateTime end)
        {
            int tenderNumber = 0;
            Dictionary<string, List<float>> data = new Dictionary<string, List<float>>();

            foreach (PharmacyProfile ph in pharmaciesService.GetAll())
                data.Add(ph.Name, new List<float>());

            foreach (Tender t in GetTendersWithOffers())
            {
                if (t.EndTime < start || t.EndTime > end)
                    continue;

                List<string> pharmacies = new List<string>();
                string pharmacyName = "";
                for (int j = 0; j < t.TenderOffers.Count; j++)
                {
                    pharmacyName = t.TenderOffers.ElementAt(j).PharmacyName;
                    double price = t.TenderOffers.ElementAt(j).TotalPrice;
                    if (!pharmacies.Contains(pharmacyName))
                    {
                        pharmacies.Add(pharmacyName);
                        data[pharmacyName].Add((float)(price));
                        continue;
                    }

                    float minPrice = data[pharmacyName][tenderNumber];
                    if (price < minPrice)
                        data[pharmacyName][tenderNumber] = (float)price;
                }

                foreach (string k in data.Keys)
                {
                    if (!pharmacies.Contains(k))
                        data[k].Add(0);
                }
                tenderNumber++;
            }
            return data;
        }
    }
}
