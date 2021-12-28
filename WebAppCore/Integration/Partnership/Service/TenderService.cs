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

        public Dictionary<string, List<float>> GetNumberOfOffersForAllTenders()
        {
            int tenderNumber = 0;
            Dictionary<string, List<float>> data = new Dictionary<string, List<float>>();

            foreach (PharmacyProfile ph in pharmaciesService.GetAll())
                data.Add(ph.Name, new List<float>());

            foreach (Tender t in GetTendersWithOffers())
            {
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

        public Dictionary<string, List<float>> GetMaxPricesForAllTenders()
        {
            int tenderNumber = 0;
            Dictionary<string, List<float>> data = new Dictionary<string, List<float>>();

            foreach (PharmacyProfile ph in pharmaciesService.GetAll())
                data.Add(ph.Name, new List<float>());

            foreach (Tender t in GetTendersWithOffers())
            {
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
                    if(price > maxPrice)
                        data[pharmacyName][tenderNumber] = (float) price;
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

        public Dictionary<string, List<float>> GetMinPricesForAllTenders()
        {
            int tenderNumber = 0;
            Dictionary<string, List<float>> data = new Dictionary<string, List<float>>();

            foreach (PharmacyProfile ph in pharmaciesService.GetAll())
                data.Add(ph.Name, new List<float>());

            foreach (Tender t in GetTendersWithOffers())
            {
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
