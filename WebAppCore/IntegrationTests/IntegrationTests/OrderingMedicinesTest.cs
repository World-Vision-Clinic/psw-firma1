using Integration;
using Integration.Pharmacy.Repository;
using Integration_API.Controller;
using Integration_API.Dto;
using IntegrationTests.UnitTests.mocks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
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
        public void CheckIf_medicine_is_ordered(OrderingMedicineDTO omd, Times expectedHttp, Times expectedGrpc)
        {
            var httpConnectionMock = new Mock<IPharmacyHttpConnection>();
            var grpcConnectionMock = new Mock<IPharmacyGrpcConnection>();
            MedicinesController medicinesController = new MedicinesController(httpConnectionMock.Object, grpcConnectionMock.Object, new HubMock());

            var requestOk = medicinesController.Order(omd);

            httpConnectionMock.Verify(x => x.SendMedicineOrderingRequestHTTP(omd), expectedHttp);
            grpcConnectionMock.Verify(x => x.SendMedicineOrderingRequestGRPC(omd), expectedGrpc);
        }

        public static IEnumerable<object[]> Data()
        {
            var retVal = new List<object[]>();

            retVal.Add(new object[] { new OrderingMedicineDTO("http://localhost:34616", "Brufen", "100", "2"), Times.Once(), Times.Never() });
            retVal.Add(new object[] { new OrderingMedicineDTO("127.0.0.1:5000", "Brufen", "100", "2"), Times.Never(), Times.Once() });

            return retVal;
        }
    }
}
