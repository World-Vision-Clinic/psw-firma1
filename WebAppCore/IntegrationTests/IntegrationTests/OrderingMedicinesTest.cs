using Integration_API.Controller;
using Integration_API.Dto;
using IntegrationTests.UnitTests.mocks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace IntegrationTests.IntegrationTests
{
    public class OrderingMedicinesTest
    {
        bool development = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";

        [SkippableFact]
        public void OrderingExistingMedicinesTest()
        {
            Skip.IfNot(development);
            OrderedMedicineDTO omd = new OrderedMedicineDTO("Brufen", "Zdravko", "none", "2 times a day", "100", "none", "none", "2", null, 200);
            MedicinesController mc = new MedicinesController(new PharmacyHTTPConnection(), new PharmacyGRPConnection(),new HubMock());

            var result = mc.OrderedHTTP(omd);

            var statusCodeResult = (IStatusCodeActionResult)result;
            
            Assert.Equal(200, statusCodeResult.StatusCode);
        }
        
        [Theory]
        [MemberData(nameof(Data))]
        public void CheckIf_medicine_is_ordered(OrderingMedicineDTO omd, int expectedStatusCode, Times expectedGrpc)
        {
            var httpConnectionMock = new PharmacyHTTPConnection();
            var grpcConnectionMock = new Mock<IPharmacyGrpcConnection>();
            MedicinesController medicinesController = new MedicinesController(httpConnectionMock, grpcConnectionMock.Object, new HubMock());

            ObjectResult response = (ObjectResult)medicinesController.Order(omd);

            response.StatusCode.Equals(expectedStatusCode);
            grpcConnectionMock.Verify(x => x.SendMedicineOrderingRequestGRPC(omd), expectedGrpc);
        }

        public static IEnumerable<object[]> Data()
        {
            var retVal = new List<object[]>();

            retVal.Add(new object[] { new OrderingMedicineDTO("http://localhost:34616/test", "Brufen", "100", "2"), 200, Times.Never() });
            retVal.Add(new object[] { new OrderingMedicineDTO("http://localhost:34616/test", "Andol", "100", "2"), 400, Times.Never() });
            retVal.Add(new object[] { new OrderingMedicineDTO("127.0.0.1:5000", "Brufen", "100", "2"), 200, Times.Once() });

            return retVal;
        }
    }
}
