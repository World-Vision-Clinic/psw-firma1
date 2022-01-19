using Hospital.MedicalRecords.Repository;
using Hospital.Schedule.Model;
using Hospital.Schedule.Repository;
using Hospital.Schedule.Service;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace HospitalTests.PatientPortalTests.UnitTests
{
    public class TestAppointmentService
    {
        [Fact]
        public void TestTermsOverlap()
        {
            AppointmentService _service = new AppointmentService(new AppointmentRepository(), new DoctorRepository());
            TimeSpan firstTimeSpan = new TimeSpan(0, 0, 50, 0, 0);
            TimeSpan secondTimeSpan = new TimeSpan(0, 1, 30, 0, 0);

            TimeRange timeRange1 = new TimeRange(firstTimeSpan, firstTimeSpan.Add(new TimeSpan(0, 0, 45, 0, 0)));
            TimeRange timeRange2 = new TimeRange(secondTimeSpan, secondTimeSpan.Add(new TimeSpan(0, 0, 30, 0, 0)));

            Assert.True(timeRange1.OverlapsWith(timeRange2));
        }

        [Fact]
        public void TestTermsDoNotOverlap()
        {
            AppointmentService _service = new AppointmentService(new AppointmentRepository(), new DoctorRepository());
            TimeSpan firstTimeSpan = new TimeSpan(0, 0, 50, 0, 0);
            TimeSpan secondTimeSpan = new TimeSpan(0, 5, 30, 0, 0);

            TimeRange timeRange1 = new TimeRange(firstTimeSpan, firstTimeSpan.Add(new TimeSpan(0, 0, 45, 0, 0)));
            TimeRange timeRange2 = new TimeRange(secondTimeSpan, secondTimeSpan.Add(new TimeSpan(0, 0, 30, 0, 0)));
            Assert.False(timeRange1.OverlapsWith(timeRange2));
        }

        [Fact]
        public void TestFreeAppointmentsListGenerated()
        {
            AppointmentService _service = new AppointmentService(new AppointmentRepository(), new DoctorRepository());
            DateTime firstDate = new DateTime(2021, 6, 6, 0, 0, 0);
            DateTime secondDate = new DateTime(2021, 6, 8, 23, 59, 59);
            DateRange range = new DateRange(firstDate, secondDate);
            TimeSpan firstTime = new TimeSpan(0, 12, 0, 0, 0);
            TimeSpan secondTime = new TimeSpan(0, 14, 0, 0, 0);
            TimeRange timeRange = new TimeRange(firstTime, secondTime);
            List<Appointment> freeAppointmentList = _service.GenerateFreeAppointmentList(range, timeRange, new TimeSpan(1, 30, 0));
            Assert.NotNull(freeAppointmentList);
            Assert.Equal(6, freeAppointmentList.Count);
        }
    }
}
