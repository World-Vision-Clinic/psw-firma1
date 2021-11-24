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

namespace HospitalTests.EditorTests
{
    public class BuildingControllerTest
    {
        [Fact]
        public void building_controller_test_1()
        {                
                var controller = new BuildingsController();
                var response = controller.GetBuilding(3);
                response.Value.ShouldBeEquivalentTo(null);
           
        }

        [Fact]
        public void building_controller_test_2()
        {
            
                var controller = new BuildingsController();
                var response = controller.GetBuilding(2);
                response.Value.ShouldNotBeNull();

        }
    }
}
