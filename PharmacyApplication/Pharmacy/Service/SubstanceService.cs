using Pharmacy.Model;
using Pharmacy.Repository.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.Service
{
    public class SubstanceService
    {
        ISubstanceRepository substanceRepository;

        public SubstanceService(ISubstanceRepository substanceRepository)
        {
            this.substanceRepository = substanceRepository;
        }


        public List<Substance> GetAll()
        {
            return substanceRepository.GetAll();
        }

        public List<long> GetMedicineIdsBySubstance(long substanceId)
        {
            List<long> medicines = new List<long>();

            foreach (Substance s in GetAll())
            {
                if (s.SubstanceId == substanceId)
                {
                    medicines.Add(s.MedicineId);
                }
            }

            return medicines;
        }
    }
}