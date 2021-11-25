using Xunit;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Hospital_API.Controllers;
using Microsoft.EntityFrameworkCore;
using Hospital.SharedModel;
using Hospital.MedicalRecords.Repository;
using Hospital.MedicalRecords.Model;
using Hospital.MedicalRecords.Service;
using Microsoft.AspNetCore.Mvc;
using Hospital.RepositoryInterfaces;
using Hospital.GraphicalEditor.Repository;
using Hospital.GraphicalEditor.Model;
using Hospital.GraphicalEditor.Service;
using Shouldly;
using Moq;

namespace HospitalTests.EditorTests
{
    public class BuildingFindAllUnitTest
    {
        private static IBuildingRepository CreateStubRepository()
        {
            var stubRepository = new Mock<IBuildingRepository>();
            var buildings = new List<Building>();


            Building hospital1 = new Building { id = 1, Name = "Hospital I", Area = null, Info = "Gynecology", MapPositionId = 1 };
            Building hospital2 = new Building { id = 2, Name = "Hospital II", Area = null, Info = "Oncology", MapPositionId = 2 };
            Building hospital3 = new Building { id = 3, Name = "Hospital III", Area = null, Info = "Dermatology", MapPositionId = 3 };

            buildings.Add(hospital1);
            buildings.Add(hospital2);
            buildings.Add(hospital3);

            stubRepository.Setup(m => m.GetAll()).Returns(buildings);
            return stubRepository.Object;
        }

        [Fact]
        public void building_service_test_1()
        {
            IBuildingRepository repo = CreateStubRepository();

            BuildingService service = new BuildingService(repo);
            service.GetAll().Count.ShouldBeEquivalentTo(3);
        }
    }
}
