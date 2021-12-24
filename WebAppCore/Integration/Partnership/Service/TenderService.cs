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

        public List<int> GetPharmacyWinningStatistic(String pharmacyName)
        {
            List<int> statistic = new List<int>();

            int numberOfTendersPharmacyParticipated = GetNumberOfTendersPharmacyParticipated(pharmacyName);

            int numberOfTendersPharmacyWon = GetNumberOfTendersPharmacyWon(pharmacyName);

            statistic.Add(numberOfTendersPharmacyParticipated - numberOfTendersPharmacyWon);
            statistic.Add(numberOfTendersPharmacyWon);

            return statistic;
        }

        public List<Tender> GetTendersPharmacyParticipated(String pharmacyName)
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

        private int GetNumberOfTendersPharmacyParticipated(String pharmacyName)
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

        private int GetNumberOfTendersPharmacyWon(String pharmacyName)
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
