using Integration_API.Controller;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
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
            MedicinesController controller = new MedicinesController(mock.Object);

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
            MedicinesController controller = new MedicinesController(mock.Object);

            // Act
            var result = controller.CheckMedicineAvailability(medicineName, dosageInMg, quantity);
            var okResult = result as OkObjectResult;

            // Assert
            Assert.Equal(200, okResult.StatusCode);
            Assert.Empty((System.Collections.IEnumerable)okResult.Value);

        }
    }
}
