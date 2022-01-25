using Integration;
using Integration.Pharmacy.Repository;
using Integration_API.Controller;
using Integration_API.Dto;
using IntegrationTests.UnitTests.mocks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
            MedicinesController mc = new MedicinesController(new PharmacyHTTPConnection(), new HubMock());

            var result = mc.OrderedHTTP(omd);

            var statusCodeResult = (IStatusCodeActionResult)result;
            
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [SkippableTheory]
        [MemberData(nameof(Data))]
        public void CheckIf_medicine_is_ordered(OrderingMedicineDTO omd, bool isHttp)
        {
            Skip.IfNot(development);
            PharmacyHTTPConnection httpConnection = new PharmacyHTTPConnection();
            PharmacyGRPConnection grpcConnection = new PharmacyGRPConnection();
            bool requestOk = false;
            if (isHttp)
                requestOk = httpConnection.SendMedicineOrderingRequestHTTP(omd, true);
            else
                requestOk = grpcConnection.SendMedicineOrderingRequestGRPC(omd, true);
            Assert.True(requestOk);
        }

        public static IEnumerable<object[]> Data()
        {
            var retVal = new List<object[]>();

            retVal.Add(new object[] { new OrderingMedicineDTO("http://localhost:34616", "Brufen", "100", "2"), true });
            //retVal.Add(new object[] { new OrderingMedicineDTO("127.0.0.1:5000", "Brufen", "100", "2"), false });

            return retVal;
        }
    }
}
