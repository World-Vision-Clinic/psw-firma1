using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TestPatientPortal.Patient
{
    class TestPatientController
    {
        [Test]
        public async Task Test_token_not_found()
        {
            using (var client = new ClientProvider().Client)
            {

                var response = await client.GetAsync("api/Patients/activate?token=1");

                Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            }
        }

        [Test]
        public async Task Test_token_found()
        {
            using (var client = new ClientProvider().Client) 
            { 

                var response = await client.GetAsync("api/Patients/activate?token=ecc0024b9feaa167cf8c5bc4819bc03aa8ed88d86524bd647db6f3363dfabd13");

                Assert.AreEqual(HttpStatusCode.Redirect, response.StatusCode);
            }
        }

        [Test]
        public async Task Test_token_found_no_redirect()
        {
            using (var client = new ClientProvider().Client)
            {

                var response = await client.GetAsync("api/Patients/activate?token=ecc0024b9feaa167cf8c5bc4819bc03aa8ed88d86524bd647db6f3363dfabd13");

                Assert.AreNotEqual(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Test]
        public async Task Test_id_not_found()
        {
            using (var client = new ClientProvider().Client)
            {

                var response = await client.GetAsync("api/Patients/0");

                Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            }
        }

        [Test]
        public async Task Test_id_found()
        {
            using (var client = new ClientProvider().Client)
            {

                var response = await client.GetAsync("api/Patients/1");

                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }
        }
    }
}
