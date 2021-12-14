using Xunit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Hospital_API.Controllers;
using Microsoft.EntityFrameworkCore;
using Hospital.SharedModel;
using Microsoft.AspNetCore.Mvc;
using Hospital.RepositoryInterfaces;
using Shouldly;
using Moq;
using Hospital.GraphicalEditor.Model;
using Hospital.GraphicalEditor.Service;

namespace HospitalTests.EditorTests
{
    public class BuildingFindById
    {
        private static IBuildingRepository CreateStubRepository()
        {
            var stubRepository = new Mock<IBuildingRepository>();
            var buildings = new List<Building>();


            Building hospital1 = new Building { id = 1, Name = "Hospital I", Area = null, Info = "Gynecology", MapPositionId = 1 };
            Building hospital2 = new Building { id = 2, Name = "Hospital II", Area = null, Info = "Oncology", MapPositionId = 2 };
            
            buildings.Add(hospital1);
            buildings.Add(hospital2);

            stubRepository.Setup(m => m.GetAll()).Returns(buildings);
            return stubRepository.Object;
        }

        [Fact]
        public void building_findById_test_1()
        {
            IBuildingRepository repo = CreateStubRepository();

            BuildingService service = new BuildingService(repo);
            Building building = service.GetById(3);
            Assert.Null(building);
           
        }

    }
}
