using Integration_API.Controller;
using Integration_API.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace IntegrationTests.IntegrationTests
{
    public class MedicineExistenceTestGrpc
    { 
            /*[Theory]
            [MemberData(nameof(Data))]
            public void Check_medicine_existence_grpcAsync(MedicineDto omd, bool medicineExist)
            {
                MedicinesController mc = new MedicinesController(new PharmacyHTTPConnection());

                bool requestOk = mc.SendRequestToCheckAvailabilityGrpc("127.0.0.1:5000", omd);

                if (medicineExist)
                {
                    Assert.True(requestOk);
                }
                else
                {
                    Assert.False(requestOk);
                }
            }
            public static IEnumerable<object[]> Data()
            {
                var retVal = new List<object[]>();

                retVal.Add(new object[] { new MedicineDto("Aspirin", 200, 2), true });
                retVal.Add(new object[] { new MedicineDto("Aspirin", 200, 1000), false });

                return retVal;
            }*/
        
    }
}
