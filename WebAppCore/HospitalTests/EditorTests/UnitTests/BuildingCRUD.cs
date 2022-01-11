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
using Hospital.ShiftsAndVacations.Repository.RepositoryInterfaces;
using Hospital.ShiftsAndVacations.Model;
using Hospital.ShiftsAndVacations.Service;
using Hospital.ShiftsAndVacations.Repository;

namespace HospitalTests.EditorTests
{
    public class OnCallShiftCRUD
    {
        private static OnCallShiftRepository CreateStubRepository()
        {
            var stubRepository = new Mock<OnCallShiftRepository>();
            var shifts = new List<OnCallShift>();


            OnCallShift shift1 = new OnCallShift(0, 1, new DateTime(2022, 01,02));
            OnCallShift shift2 = new OnCallShift(0, 4, new DateTime(2022, 01, 12));
            OnCallShift shift3 = new OnCallShift(0, 4, new DateTime(2022, 01, 15));

            shifts.Add(shift1);
            shifts.Add(shift2);
            shifts.Add(shift3);

            stubRepository.Setup(m => m.GetAll()).Returns(shifts);
            return stubRepository.Object;
        }

        [Fact]
        public void building_service_test()
        {
            OnCallShiftRepository repo = CreateStubRepository();

            OnCallShiftService service = new OnCallShiftService(repo);
            service.getAll().Count.ShouldBeEquivalentTo(3);
        }

        [Fact]
        public void building_findById_test_not_found()
        {
            OnCallShiftRepository repo = CreateStubRepository();

            OnCallShiftService service = new OnCallShiftService(repo);
            OnCallShift shift = service.getById(3);
            Assert.Null(shift);

        }
        /*
        [Fact]
        public void building_findById_test_found()
        {
            IBuildingRepository repo = CreateStubRepository();

            BuildingService service = new BuildingService(repo);
            Building building = service.GetById(1);
            Assert.NotNull(building);

        }*/ 
    }
}
