using Hospital.GraphicalEditor.Model;
using Hospital.GraphicalEditor.Repository;
using Hospital.GraphicalEditor.Service;
using Hospital.RepositoryInterfaces;
using Hospital.RoomsAndEquipment.Model;
using Hospital.RoomsAndEquipment.Repository;
using Hospital.RoomsAndEquipment.Service;
using Hospital.Schedule.Repository;
using Hospital.SharedModel;
using Hospital_API;
using Hospital_API.Controllers;
using Hospital_API.DTO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HospitalTests.EditorTests.IntegrationTests
{
    public class RelocatingEquipment
    {


        private TestContext GetInMemoryRepository()
        {
            DbContextOptions<TestContext> options;
            var builder = new DbContextOptionsBuilder<TestContext>();
            builder.UseInMemoryDatabase("TestDb");
            options = builder.Options;
            TestContext hospitalContext = new TestContext(options);
            hospitalContext.Database.EnsureDeleted();
            hospitalContext.Database.EnsureCreated();
            return hospitalContext;
        }

        public RelocatingEquipment()
        {
           
        }

        [Fact]
        public async Task Get_suggestion_for_relocation_period_not_found()
        {
            // Arrange
            TestContext inMemoryRepo = GetInMemoryRepository();
            EquipmentService equipmentService = new EquipmentService(new EquipmentRepository(inMemoryRepo));
            FloorService floorService = new FloorService(new FloorRepository(inMemoryRepo));
            RoomService roomService = new RoomService(new RoomRepository(inMemoryRepo));
            DateTime startDate = new DateTime(year: 2021, month: 12, day: 4, hour: 10, minute: 30, second: 0);
            DateTime endDate = new DateTime(year: 2021, month: 12, day: 4, hour: 14, minute: 30, second: 0);
            int buildingId = 1;
            int transportDurationInHours = 10;

            //Act


            DatePeriod datePeriod = equipmentService.SuggestTransportationPeriod(startDate, endDate, buildingId, floorService, roomService, equipmentService, transportDurationInHours);

            // Assert

            Assert.NotNull(datePeriod);

        }

        [Fact]
        public async Task Get_suggestion_for_relocation_period_found()
        {
            // Arrange
            TestContext inMemoryRepo = GetInMemoryRepository();
            EquipmentService equipmentService = new EquipmentService(new EquipmentRepository(inMemoryRepo));
            FloorService floorService = new FloorService(new FloorRepository(inMemoryRepo));
            RoomService roomService = new RoomService(new RoomRepository(inMemoryRepo));
            DateTime startDate = new DateTime(year: 2021, month: 12, day: 4, hour: 10, minute: 30, second: 0);
            DateTime endDate = new DateTime(year: 2021, month: 12, day: 4, hour: 14, minute: 30, second: 0);
            int buildingId = 1;
            int transportDurationInHours = 4;
            DatePeriod expectedPeriod = new DatePeriod() { endDate = startDate.AddHours(transportDurationInHours), startDate = startDate };
            //Act
            DatePeriod datePeriod = equipmentService.SuggestTransportationPeriod(startDate, endDate, buildingId, floorService, roomService, equipmentService, transportDurationInHours);

            // Assert

            Assert.Equal(datePeriod, expectedPeriod);

        }


    }
}