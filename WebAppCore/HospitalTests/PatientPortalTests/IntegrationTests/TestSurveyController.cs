using Hospital.MedicalRecords.Repository;
using Hospital.MedicalRecords.Service;
using Hospital.Schedule.Model;
using Hospital.Schedule.Repository;
using Hospital.Schedule.Service;
using Hospital.SharedModel;
using Hospital_API;
using Hospital_API.Controllers;
using Hspital_API.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace HospitalTests.PatientPortalTests.IntegrationTests
{
    public class TestSurveyController
    {
        public PatientRepository _patientRepository;
        public SurveyRepository _surveyRepository;
        public DoctorRepository _doctorRepository;
        public AppointmentRepository _appointmentRepository;

        public SurveyController _surveyController;

        public TestSurveyController()
        {
            DbContextOptions<TestContext> options;
            var builder = new DbContextOptionsBuilder<TestContext>();
            builder.UseInMemoryDatabase("TestDb");
            options = builder.Options;
            TestContext hospitalContext = new TestContext(options);
            hospitalContext.Database.EnsureCreated();
            _patientRepository = new PatientRepository(hospitalContext);
            _surveyRepository = new SurveyRepository(hospitalContext);
            _doctorRepository = new DoctorRepository(hospitalContext, _patientRepository);
            _appointmentRepository = new AppointmentRepository(hospitalContext);

            _surveyController = new SurveyController();
            _surveyController._patientService = new PatientService(_patientRepository, _appointmentRepository);
            _surveyController.surveyService = new SurveyService(_surveyRepository);
            _surveyController._appointmentService = new AppointmentService(_appointmentRepository, _doctorRepository);
            _surveyController.test = true;
        }


        [Fact]
        public void Test_survey_found()
        {   //Arrange

            Survey survey = new Survey
            (
                4,
                DateTime.Now
            );

            _surveyRepository.AddSurvey(survey);
            var response = _surveyController.GetSurvey(4);

            Assert.Equal(4, response.Value.Id);
        }

        [Fact]
        public void Test_retrieve_questions()
        {   //Arrange
            List<SurveyQuestion> questions = new List<SurveyQuestion>();

            SurveyQuestion question1 = new SurveyQuestion(1, "Pitanje1", SurveySectionType.Doctor);
            SurveyQuestion question2 = new SurveyQuestion(2, "Pitanje2", SurveySectionType.Hospital);
            //Act
            questions.Add(question1);
            questions.Add(question2);   
            var response = _surveyController.GetQuestions();
            var result = response.Result as OkObjectResult;
            //Assert
            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(result.Value);
            Assert.Equal(2, questions.Count);
        }

        [Fact]
        public void Test_check_survey_breakdown()
        {
            /*AnsweredSurveyQuestion question1Answer1 = new AnsweredSurveyQuestion(3, 1, 5, 3);

            AnsweredSurveyQuestion question2Answer1 = new AnsweredSurveyQuestion(2, 2, 3, 4);

            AnsweredSurveyQuestion question1Answer2 = new AnsweredSurveyQuestion(1, 1, 4, 5);

            AnsweredSurveyQuestion question2Answer2 = new AnsweredSurveyQuestion(2, 2, 4, 6);

            _surveyRepository.AddAnswer(question1Answer1);
            _surveyRepository.AddAnswer(question2Answer1);
            _surveyRepository.AddAnswer(question1Answer2);
            _surveyRepository.AddAnswer(question2Answer2);

            var response = _surveyController.GetAnsweredQuestionsBreakdown();
            Assert.Equal(2, response.Value.Count());
            Assert.Equal(4.5, response.Value.ElementAt(0).Average);
            Assert.Equal(3.5, response.Value.ElementAt(1).Average);*/
        }

        [Fact]
        public void Test_post_correct_answers()
        {   
            List<QuestionDTO> dtos = new List<QuestionDTO>();            

            QuestionDTO answer1 = new QuestionDTO()
            {
                QuestionId = 1,
                Question = "Pitanje1",
                Section = SurveySectionType.Doctor,
                Answer = 4
            };
            QuestionDTO answer2 = new QuestionDTO()
            {
                QuestionId = 2,
                Question = "Pitanje2",
                Section = SurveySectionType.Hospital,
                Answer = 2
            };
            
            dtos.Add(answer1);
            dtos.Add(answer2);
            var response = _surveyController.PostSurveyQuestions(dtos, 199);
            var result = (OkObjectResult)response.Result;
            
            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(result.Value);
            Assert.Equal(2, dtos.Count);
        }
        
        [Fact]
        public void Test_post_wrong_answers()
        {   //Arrange
            List<QuestionDTO> dtos = new List<QuestionDTO>();

            QuestionDTO answer1 = new QuestionDTO()
            {
                Question = "Pitanje1",
                Section = SurveySectionType.Doctor,
                Answer = 4
            };
            QuestionDTO answer2 = new QuestionDTO()
            {
                Question = "Pitanje2",
                Section = SurveySectionType.Hospital,
                Answer = 7
            };
            //Act
            dtos.Add(answer1);
            dtos.Add(answer2);
            Assert.Throws<ArgumentException>(() => _surveyController.PostSurveyQuestions(dtos, 199));
        }

    }
}
