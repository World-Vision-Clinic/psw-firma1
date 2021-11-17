using Pharmacy.Model;
using Pharmacy.Repository;
using Pharmacy.Service;
using Shouldly;
using System;
using System.Collections.Generic;
using Xunit;

namespace PharmacyApplicationTests
{
    public class MedicineInformationTests
    {
        [Fact]
        public void Find_specific_medicine()
        {
            MedicineService service = new MedicineService(new MedicineRepository());
            Medicine medicine = service.GetById(1L);
            Assert.NotNull(medicine);
        }

        [Fact]
        public void Find_substitute_medicines()
        {
            List<long> expectedSubstituteMedicinesId = new List<long>();
            expectedSubstituteMedicinesId.Add(2L);

            MedicineService service = new MedicineService(new MedicineRepository());

            Medicine medicine = service.GetById(1L);
            List<long> actualSubstituteMedicinesId = FindAllSubstituteMedicinesId(medicine);

            actualSubstituteMedicinesId.ShouldBeEquivalentTo(expectedSubstituteMedicinesId);
        }

        private static List<long> FindAllSubstituteMedicinesId(Medicine medicine)
        {
            List<long> actualSubstituteMedicinesId = new List<long>();
            foreach (SubstituteMedicine actualSubstituteMedicine in medicine.SubstituteMedicines)
            {
                actualSubstituteMedicinesId.Add(actualSubstituteMedicine.SubstituteId);
            }
            return actualSubstituteMedicinesId;
        }
    }
}
