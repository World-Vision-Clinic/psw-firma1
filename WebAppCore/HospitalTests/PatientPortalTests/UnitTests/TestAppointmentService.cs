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
            AppointmentService _service = new AppointmentService(new AppointmentRepository());
            DateTime firstDate = new DateTime(2021, 6, 6, 12, 0, 0);
            TimeSpan firstTimeSpan = new TimeSpan(0, 0, 45, 0, 0);
            DateTime secondDate = new DateTime(2021, 6, 6, 11, 30, 0);
            TimeSpan secondTimeSpan = new TimeSpan(0, 1, 30, 0, 0);
            Assert.True(_service.DatesOverlap(firstDate, firstTimeSpan, secondDate, secondTimeSpan));
        }

        [Fact]
        public void TestTermsDoNotOverlap()
        {
            AppointmentService _service = new AppointmentService(new AppointmentRepository());
            DateTime firstDate = new DateTime(2021, 6, 6, 17, 0, 0);
            TimeSpan firstTimeSpan = new TimeSpan(0, 0, 45, 0, 0);
            DateTime secondDate = new DateTime(2021, 6, 6, 11, 30, 0);
            TimeSpan secondTimeSpan = new TimeSpan(0, 1, 30, 0, 0);
            Assert.False(_service.DatesOverlap(firstDate, firstTimeSpan, secondDate, secondTimeSpan));
        }

        [Fact]
        public void TestFreeAppointmentsListGenerated()
        {
            AppointmentService _service = new AppointmentService(new AppointmentRepository());
            DateTime firstDate = new DateTime(2021, 6, 6, 0, 0, 0);
            DateTime secondDate = new DateTime(2021, 6, 8, 23, 59, 59);
            List<Appointment> freeAppointmentList = _service.GenerateFreeAppointmentList(firstDate, secondDate, new TimeSpan(12, 0, 0), new TimeSpan(14, 0, 0), new TimeSpan(1, 0, 0));
            Assert.NotNull(freeAppointmentList);
            Assert.Equal(6, freeAppointmentList.Count);
        }
    }
}
