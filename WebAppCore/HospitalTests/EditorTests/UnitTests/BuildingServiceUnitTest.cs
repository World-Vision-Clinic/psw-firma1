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

namespace HospitalTests.EditorTests
{
    public class BuildingServiceUnitTest
    {
        [Fact]
        public void building_service_test_1()
        {
            HospitalContext context = new HospitalContext();
            IBuildingRepository repo = new BuildingRepository(context);
            BuildingService service = new BuildingService(repo);
            service.GetAll().Count.ShouldBeEquivalentTo(2);
        }
    }
}
