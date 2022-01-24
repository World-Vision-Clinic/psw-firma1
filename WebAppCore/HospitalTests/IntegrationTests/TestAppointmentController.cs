﻿using Hospital.MedicalRecords.Model;
using Hospital.MedicalRecords.Repository;
using Hospital.MedicalRecords.Service;
using Hospital.Schedule.Model;
using Hospital.Schedule.Repository;
using Hospital.Schedule.Service;
using Hospital.SharedModel;
using Hospital_API.Controllers;
using Hospital_API.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using Xunit;

namespace HospitalTests.PatientPortalTests.IntegrationTests
{
    public class TestAppointmentController
    {
        public AppointmentRepository _appointmentRepository;
        public DoctorRepository _doctorRepository;
        public PatientRepository _patientRepository;
        public AppointmentController _appointmentController;

        public TestAppointmentController()
        {
            GetInMemoryPersonRepository();
        }

        private void GetInMemoryPersonRepository()
        {
            DbContextOptions<TestContext> options;
            var builder = new DbContextOptionsBuilder<TestContext>();
            builder.UseInMemoryDatabase("TestDb");
            options = builder.Options;
            TestContext hospitalContext = new TestContext(options);
            hospitalContext.Database.EnsureCreated();
            _appointmentRepository = new AppointmentRepository(hospitalContext);
            _patientRepository = new PatientRepository(hospitalContext);
            _doctorRepository = new DoctorRepository(hospitalContext, _patientRepository);
            _appointmentController = new AppointmentController(new AppointmentService(_appointmentRepository, _doctorRepository));
            _appointmentController._patientService = new PatientService(_patientRepository, _appointmentRepository);
            _appointmentController.test = true;
        }

        [Fact]
        public void Test_appointment_by_patient_not_found()
        {
            var controller = new AppointmentController();
            /*var response = controller.GetAppointmentsByPatientId(50);

            //Assert
            Assert.Empty(response.Value.ToList());*/

        }

        [Fact]
        public void Test_get_appointments_4step_bad()
        {
            Doctor doctor = new Doctor(5,"TestDoktorIme","TestDoktorPrezime", -1,-1, DoctorType.Family_physician, false);
            _doctorRepository.AddDoctor(doctor);

            var controller = new AppointmentController();
            var response = controller.GetAppointments4Step(5, "2020-12-08").Result as BadRequestObjectResult;

            //Assert
            Assert.Equal(400,response.StatusCode);

        }

        [Fact]
        public void Test_get_appointments_4step_good()
        {
            Doctor doctor = new Doctor(6, "TestDoktorIme", "TestDoktorPrezime", -1, -1, DoctorType.Family_physician, false);
            _doctorRepository.AddDoctor(doctor);
            DateTime dateForTest = DateTime.Now.Date;
            if (dateForTest.DayOfWeek == DayOfWeek.Friday || dateForTest.DayOfWeek == DayOfWeek.Saturday || dateForTest.DayOfWeek == DayOfWeek.Sunday)
            {
                dateForTest = dateForTest.AddDays(3);
            }
            else 
            {
                dateForTest = dateForTest.AddDays(1);
            }

            var response = (OkObjectResult) _appointmentController.GetAppointments4Step(6, dateForTest.ToString()).Result;
            var data = response.Value as List<Appointment>;

            //Assert
            Assert.Equal(200, response.StatusCode);
            Assert.NotEmpty(data);
        }

        [Fact]
        public void Test_appointment_by_patient_found()
        {
            Appointment appointment = new Appointment()
            {
                Id = 50,
                PatientForeignKey = 1,
                DoctorForeignKey = 1,
                Type = AppointmentType.Appointment,
                Date = DateTime.Now,
                Time = TimeSpan.Zero
            };
            _appointmentRepository.AddAppointment(appointment);
            /*var response = _appointmentController.GetAppointmentsByPatientId(1);

            Assert.NotNull(response);
            foreach (AppointmentDTO appointmentIterator in response.Value)
            {
                Assert.Equal(1, appointmentIterator.PatientForeignKey);
            }*/
        }

        [Fact]
        public void Test_appointment_by_doctor_found()
        {
            var response = _appointmentController.GetAppointmentsByDoctorId(1);

            Assert.NotNull(response);
            foreach (Appointment appointmentIterator in response.Value)
            {
                Assert.Equal(1, appointmentIterator.DoctorForeignKey);
            }
        }

        [Fact]
        public void Test_add_valid_appointment()
        {
            Appointment appointment = new Appointment()
            {
                Id = 2,
                PatientForeignKey = 4,
                DoctorForeignKey = 1,
                Type = AppointmentType.Appointment,
                Date = new DateTime(2025, 6, 6, 12, 0, 0),
                Time = new TimeSpan(0, 0, 45, 0, 0)
            };

            HttpResponseMessage response = _appointmentController.AddAppointment(appointment);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public void Test_add_invalid_historical_appointment()
        {
            Appointment appointment = new Appointment()
            {
                Id = 3,
                PatientForeignKey = 1,
                DoctorForeignKey = 1,
                Type = AppointmentType.Appointment,
                Date = new DateTime(1990, 6, 6, 12, 0, 0),
                Time = new TimeSpan(0, 0, 45, 0, 0)
            };

            HttpResponseMessage response = _appointmentController.AddAppointment(appointment);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public void Test_add_invalid_overlapping_appointment()
        {
            Appointment validAppointment = new Appointment()
            {
                Id = 4,
                PatientForeignKey = 4,
                DoctorForeignKey = 1,
                Type = AppointmentType.Appointment,
                Date = new DateTime(2030, 6, 6, 12, 0, 0),
                Time = new TimeSpan(0, 0, 45, 0, 0)
            };

            Appointment overlappingAppointment = new Appointment()
            {
                Id = 5,
                PatientForeignKey = 4,
                DoctorForeignKey = 1,
                Type = AppointmentType.Appointment,
                Date = new DateTime(2030, 6, 6, 11, 30, 0),
                Time = new TimeSpan(0, 0, 45, 0, 0)
            };

            HttpResponseMessage response = _appointmentController.AddAppointment(validAppointment);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            response = _appointmentController.AddAppointment(overlappingAppointment);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            Appointment addedAppointment = _appointmentController.GetAppointment(4).Value;
            Appointment notAddedAppointment = _appointmentController.GetAppointment(5).Value;

            Assert.NotNull(addedAppointment);
            Assert.Null(notAddedAppointment);
        }

        [Fact]
        public void Test_get_free_doctor_appointments_in_range()
        {
            AppointmentRecommendationRequestDTO appointmentRecommendationRequestDTO = new AppointmentRecommendationRequestDTO();
            appointmentRecommendationRequestDTO.LowerDateRange = new DateTime(2022, 6, 6, 0, 0, 0);
            appointmentRecommendationRequestDTO.UpperDateRange = new DateTime(2022, 6, 7, 23, 59, 59);
            appointmentRecommendationRequestDTO.LowerTimeRange = "12:00:00";
            appointmentRecommendationRequestDTO.UpperTimeRange = "14:00:00";
            appointmentRecommendationRequestDTO.DoctorId = 1;
            appointmentRecommendationRequestDTO.PriorityType = "DOCTOR_PRIORITY";
            List<Appointment> freeAppointmentsBeforeAddition = _appointmentController.GetRecommendedAppointments(appointmentRecommendationRequestDTO).Value.ToList();

            Assert.NotNull(freeAppointmentsBeforeAddition);
            Assert.Equal(8, freeAppointmentsBeforeAddition.Count);

            Appointment appointment = new Appointment()
            {
                Id = 6,
                PatientForeignKey = 1,
                DoctorForeignKey = 1,
                Type = AppointmentType.Appointment,
                Date = new DateTime(2022, 6, 6, 12, 0, 0),
                Time = new TimeSpan(0, 0, 30, 0, 0)
            };
            _appointmentRepository.AddAppointment(appointment);

            List<Appointment> freeAppointmentsAfterAddition = _appointmentController.GetRecommendedAppointments(appointmentRecommendationRequestDTO).Value.ToList();

            Assert.NotNull(freeAppointmentsAfterAddition);
            Assert.Equal(7, freeAppointmentsAfterAddition.Count);
        }

        [Fact]
        public void Test_get_free_doctor_appointments_in_range_with_interval_loosening()
        {
            AppointmentRecommendationRequestDTO appointmentRecommendationRequestDTO = new AppointmentRecommendationRequestDTO();
            appointmentRecommendationRequestDTO.LowerDateRange = new DateTime(2022, 7, 7, 0, 0, 0);
            appointmentRecommendationRequestDTO.UpperDateRange = new DateTime(2022, 7, 7, 23, 59, 59);
            appointmentRecommendationRequestDTO.LowerTimeRange = "12:00:00";
            appointmentRecommendationRequestDTO.UpperTimeRange = "13:00:00";
            appointmentRecommendationRequestDTO.DoctorId = 1;
            appointmentRecommendationRequestDTO.PriorityType = "DOCTOR_PRIORITY";

            Appointment appointment = new Appointment()
            {
                Id = 7,
                PatientForeignKey = 1,
                DoctorForeignKey = 1,
                Type = AppointmentType.Appointment,
                Date = new DateTime(2022, 7, 7, 12, 0, 0),
                Time = new TimeSpan(0, 0, 45, 0, 0)
            };
            _appointmentRepository.AddAppointment(appointment);

            Appointment earlierAppointment = new Appointment()
            {
                Id = 8,
                PatientForeignKey = 1,
                DoctorForeignKey = 1,
                Type = AppointmentType.Appointment,
                Date = new DateTime(2022, 7, 6, 12, 0, 0),
                Time = new TimeSpan(0, 0, 45, 0, 0)
            };
            _appointmentRepository.AddAppointment(earlierAppointment);

            List<Appointment> freeAppointmentsAfterAddition = _appointmentController.GetRecommendedAppointments(appointmentRecommendationRequestDTO).Value.ToList();

            Assert.NotNull(freeAppointmentsAfterAddition);
            Assert.Equal(18, freeAppointmentsAfterAddition.Count);
        }

        [Fact]
        public void Test_get_free_doctor_appointments_in_range_with_date_priority()
        {
            AppointmentRecommendationRequestDTO appointmentRecommendationRequestDTO = new AppointmentRecommendationRequestDTO();
            appointmentRecommendationRequestDTO.LowerDateRange = new DateTime(2022, 9, 9, 0, 0, 0);
            appointmentRecommendationRequestDTO.UpperDateRange = new DateTime(2022, 9, 9, 23, 59, 59);
            appointmentRecommendationRequestDTO.LowerTimeRange = "12:00:00";
            appointmentRecommendationRequestDTO.UpperTimeRange = "13:00:00";
            appointmentRecommendationRequestDTO.DoctorId = 11;
            appointmentRecommendationRequestDTO.PriorityType = "DATE_TIME_PRIORITY";

            Doctor doctorCardi1 = new Doctor(11,"Sava","Savić", -1, -1, DoctorType.Cardiologist, false);
            _doctorRepository.AddDoctor(doctorCardi1);

            Doctor doctorCardi2 = new Doctor(12, "Milana", "Milanović", -1, -1, DoctorType.Cardiologist, false);
            _doctorRepository.AddDoctor(doctorCardi2);

            Doctor doctorOphta1 = new Doctor(12, "Nikola", "Marković", -1, -1, DoctorType.Ophthalmologist, false);
            _doctorRepository.AddDoctor(doctorOphta1);

            Appointment appointment = new Appointment()
            {
                Id = 9 ,
                PatientForeignKey = 1,
                DoctorForeignKey = 11,
                Type = AppointmentType.Appointment,
                Date = new DateTime(2022, 9, 9, 12, 0, 0),
                Time = new TimeSpan(0, 0, 45, 0, 0)
            };
            _appointmentRepository.AddAppointment(appointment);

            List<Appointment> freeAppointmentsAfterAddition = _appointmentController.GetRecommendedAppointments(appointmentRecommendationRequestDTO).Value.ToList();

            Assert.Equal(2, freeAppointmentsAfterAddition.Count);
        }

        [Fact]
        public void Test_valid_cancel_appointment()
        {
            Appointment appointment = new Appointment()
            {
                Id = 10,
                PatientForeignKey = 4,
                DoctorForeignKey = 1,
                Type = AppointmentType.Appointment,
                Date = DateTime.Now.AddDays(5),
                Time = new TimeSpan(0, 11, 30, 0, 0),
                IsCancelled = false
            };

            _appointmentRepository.AddAppointment(appointment);
            var response = _appointmentController.CancelAppointment(10);


            var result = response.Result as OkObjectResult;
            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(result.Value);

        }

        [Fact]
        public void Test_invalid_cancel_appointment()
        {
            Appointment appointment = new Appointment()
            {
                Id = 11,
                PatientForeignKey = 4,
                DoctorForeignKey = 1,
                Type = AppointmentType.Appointment,
                Date = new DateTime(2020, 9, 9, 0, 0, 0),
                Time = new TimeSpan(0, 8, 30, 0, 0),
                IsCancelled = true
            };

            _appointmentRepository.AddAppointment(appointment);
            var response = _appointmentController.CancelAppointment(11);

            var result = response.Result as BadRequestObjectResult;
            Assert.Equal(400, result.StatusCode);

        }

    }
}
