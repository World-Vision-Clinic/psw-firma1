using Integration.Pharmacy.Model;
using Integration.Pharmacy.Repository;
using Integration.Pharmacy.Service;
using Integration_API.Controller;
using IntegrationTests.UnitTests.mocks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Http.Results;
using Xunit;

namespace IntegrationTests.UnitTests
{
    public class MedicinesUnitTests
    {
        [Fact]
        public void Check_response_when_medicine_is_available_from_mock_pharmacy()
        {
            // Arrange
            var mock = new Mock<MockConnection>();
            MedicinesController controller = new MedicinesController(mock.Object, new HubMock());

            // Act
            var result = controller.CheckMedicineAvailability("Aspirin", "200", "5");
            var okResult = result as OkObjectResult;

            // Assert
            Assert.Equal(200, okResult.StatusCode);
            Assert.NotEmpty((System.Collections.IEnumerable)okResult.Value);

        }

        [Theory]
        [InlineData("Hemomicin", "200", "5")]   // medicineName is wrong
        [InlineData("Aspirin", "500", "5")]     // medicine dosageInMg is wrong
        [InlineData("Aspirin", "200", "6")]     // medicine quantity is wrong
        public void Check_response_when_medicine_is_not_available_from_mock_pharmacy(string medicineName, string dosageInMg, string quantity)
        {
            // Arrange
            var mock = new Mock<MockConnection>();
            MedicinesController controller = new MedicinesController(mock.Object, new HubMock());

            // Act
            var result = controller.CheckMedicineAvailability(medicineName, dosageInMg, quantity);
            var okResult = result as OkObjectResult;

            // Assert
            Assert.Equal(200, okResult.StatusCode);
            Assert.Empty((System.Collections.IEnumerable)okResult.Value);

        }

        [Fact]
        public void Check_response_when_specification_for_medicine_does_not_exist_in_pharmacy()
        {
            // Arrange
            var mock = new Mock<MockConnection>();
            MedicinesController controller = new MedicinesController(mock.Object, new HubMock());

            // Act
            var result = controller.GetSpecification("someLocalhost", "Hemomicin");
            var badResult = result as BadRequestObjectResult;

            // Assert
            Assert.Equal(400, badResult.StatusCode);
            Assert.Equal("Specification does not exists", badResult.Value);
        }

        /*[Fact]  // interaction with Rebex Client
        public void Check_response_when_specification_for_medicine_exist()
        {
            // Arrange
            var mock = new Mock<MockConnection>();
            MedicinesController controller = new MedicinesController(mock.Object);

            // Act
            var result = controller.GetSpecification("someLocalhost", "Aspirin");
            var okResult = result as OkObjectResult;

            // Assert
            Assert.Equal(200, okResult.StatusCode);
        }*/
    }
}
