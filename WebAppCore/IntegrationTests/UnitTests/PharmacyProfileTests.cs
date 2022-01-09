using Integration.Pharmacy.Model;
using Integration.Pharmacy.Repository.RepositoryInterfaces;
using Integration.Pharmacy.Service;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Shouldly;

namespace IntegrationTests.UnitTests
{
    public class PharmacyProfileTests
    {
        [Theory]
        [MemberData(nameof(Data))]
        public void Change_view_status(PharmacyProfile editedPharmacy, bool exists)
        {

            PharmaciesService service = new PharmaciesService(CreateStubRepository());

            PharmacyProfile newPharmacy = service.Edit(editedPharmacy);

            if (exists)
            {
                Assert.Equal(newPharmacy.Address.Street, editedPharmacy.Address.Street);
                Assert.Equal(newPharmacy.Address.City, editedPharmacy.Address.City);
            }
            else
            {
                Assert.Null(newPharmacy);
            }
            
        }

        public static IEnumerable<object[]> Data()
        {
            var retVal = new List<object[]>();

            retVal.Add(new object[] { new PharmacyProfile("Jankovic", "abcabc", "http://localhost:34616", ProtocolType.HTTP, "Kralja Petra 100", "Beograd", "Najbolja apoteka"), true });
            retVal.Add(new object[] { new PharmacyProfile("Jankovic", "abcabc", "http://localhost:30000", ProtocolType.HTTP, "Bulevar Jovana Ducica 3", "Novi Sad", "Najbolja apoteka"), false });

            return retVal;
        }

        private static IPharmaciesRepository CreateStubRepository()
        {
            var stubRepository = new Mock<IPharmaciesRepository>();
            var pharmacies = new List<PharmacyProfile>();

            pharmacies.Add(new PharmacyProfile("Jankovic", "abcabc", "http://localhost:34616", ProtocolType.HTTP, "Bulevar Jovana Ducica 3", "Novi Sad", "Najbolja apoteka"));
            pharmacies.Add(new PharmacyProfile("Benu", "abcabc", "http://localhost:34000", ProtocolType.HTTP, "Jevrejska 100", "Novi Sad", "Losa apoteka"));
            stubRepository.Setup(m => m.GetAll()).Returns(pharmacies);

            return stubRepository.Object;
        }
    }
}
