using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Hospital.Service;
using Hospital.Repository;

namespace TestPatientPortal.Patient
{
    class TestPatientService
    {
        [Test]
        public void TestGenerateTokenSHA256()
        {
            PatientService _service = new PatientService(new PatientRepository());
            Assert.AreEqual("ecc0024b9feaa167cf8c5bc4819bc03aa8ed88d86524bd647db6f3363dfabd13",_service.TokenizeSHA256("mihajlo"));
        }
    }
}
