using Integration_API.Controller;
using Integration_API.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace IntegrationTests.IntegrationTests
{
    public class GrpcMedicinesOrderingTests
    {
        [Fact]
        public void Check_if_medicine_is_ordered()
        {
            OrderingMedicineDTO omd = new OrderingMedicineDTO("127.0.0.1:5000", "Brufen", "100", "2");
            MedicinesController mc = new MedicinesController(new PharmacyHTTPConnection());

            bool requestOk = mc.SendMedicineOrderingRequestGRPC(omd, true);

            Assert.True(requestOk);
        }
    }
}
