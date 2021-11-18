using Xunit;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HospitalTests.Patient
{
    public class TestPatientController
    {
        [Fact]
        public async Task Test_token_not_found()
        {
            using (var client = new ClientProvider().Client)
            {

                var response = await client.GetAsync("api/Patients/activate?token=1");

                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            }
        }

        [Fact]
        public async Task Test_token_found()
        {
            using (var client = new ClientProvider().Client) 
            { 

                var response = await client.GetAsync("api/Patients/activate?token=ecc0024b9feaa167cf8c5bc4819bc03aa8ed88d86524bd647db6f3363dfabd13");

                Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            }
        }
    }
}
