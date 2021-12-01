using Integration_API.Controller;
using Integration_API.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace IntegrationTests.IntegrationTests
{
    public class GrpcTests
    {
        [Fact]
        public void Check_medicine_existence_when_medicine_exists_grpc()
        {
            MedicineDto omd = new MedicineDto("Aspirin", 200, 2);
            MedicinesController mc = new MedicinesController(new PharmacyHTTPConnection());

            bool requestOk = mc.SendRequestToCheckAvailabilityGrpc("127.0.0.1:5000", omd);

            Assert.True(requestOk);
        }

        [Fact]
        public void Check_medicine_existence_when_medicine_does_not_exists_grpc()
        {
            MedicineDto omd = new MedicineDto("Aspirin", 200, 1000);
            MedicinesController mc = new MedicinesController(new PharmacyHTTPConnection());

            bool requestOk = mc.SendRequestToCheckAvailabilityGrpc("127.0.0.1:5000", omd);

            Assert.False(requestOk);
        }
    }
}
