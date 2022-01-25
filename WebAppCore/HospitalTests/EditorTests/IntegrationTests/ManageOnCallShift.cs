using Hospital.MedicalRecords.Model;
using Hospital.MedicalRecords.Repository;
using Hospital.MedicalRecords.Service;
using Hospital.SharedModel;
using Hospital.ShiftsAndVacations.Repository;
using Hospital.ShiftsAndVacations.Service;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace HospitalTests.EditorTests.IntegrationTests
{
    public class ManageOnCallShift
    {
        
        public int roomId;

        [Fact]
        public void CreateOnCallForAlreadyExistingShift()
        {
            //Arrange
            OnCallShiftService service = new OnCallShiftService(new OnCallShiftRepository(new HospitalContext()));
            DoctorService doctorService = new DoctorService(new DoctorRepository(new HospitalContext()));
            Doctor doctor = doctorService.FindById(1);

            //Act


            bool success =  service.addNewOnCallShift(12, 7, new DateTime(2022, 01, 20));


            //Assert
            Assert.False(success);
        }

        [Fact]
        public void CreateOnCallForShift()
        {
            //Arrange
            OnCallShiftService service = new OnCallShiftService(new OnCallShiftRepository(new HospitalContext()));
            DoctorService doctorService = new DoctorService(new DoctorRepository(new HospitalContext()));
            Doctor doctor = doctorService.FindById(1);

            //Act


            bool success =  service.addNewOnCallShift(0, 1, new DateTime(2022, 01, 20));


            //Assert
            Assert.True(success);
        }
        
    }
}
