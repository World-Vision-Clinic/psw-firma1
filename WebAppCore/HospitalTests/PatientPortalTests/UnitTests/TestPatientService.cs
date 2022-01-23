using Xunit;
using System;
using System.Collections.Generic;
using System.Text;
using Hospital.MedicalRecords.Service;
using Hospital.MedicalRecords.Repository;
using Hospital.Schedule.Repository;
using Hospital.MedicalRecords.Model;

namespace HospitalTests.PatientPortalTests.UnitTests
{
    public class TestPatientService
    {
        [Fact]
        public void TestGenerateTokenSHA256()
        {
            PatientService _service = new PatientService(new PatientRepository(), new AppointmentRepository());
            Assert.Equal("ecc0024b9feaa167cf8c5bc4819bc03aa8ed88d86524bd647db6f3363dfabd13", Patient.TokenizeSHA256("mihajlo"));
        }
    }
}
