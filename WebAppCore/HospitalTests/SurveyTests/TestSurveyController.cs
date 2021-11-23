using Hospital.Schedule.Model;
using Hospital.Schedule.Repository;
using Hospital.Schedule.Service;
using Hospital.SharedModel;
using Hospital_API;
using Hspital_API.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace HospitalTests.SurveyTests
{
    public class TestSurveyController
    {
        public ISurveyRepository inMemoryRepo;

        public TestSurveyController() { }

        private ISurveyRepository GetInMemorySurveyRepository()
        {
            DbContextOptions<TestContext> options;
            var builder = new DbContextOptionsBuilder<TestContext>();
            builder.UseInMemoryDatabase("TestDb");
            options = builder.Options;
            TestContext hospitalContext = new TestContext(options);
            hospitalContext.Database.EnsureDeleted();
            hospitalContext.Database.EnsureCreated();

            return new SurveyRepository(hospitalContext);
        }

        [Fact]
        public void Test_retrieve_questions()
        {   //Arrange
            inMemoryRepo = GetInMemorySurveyRepository();

            SurveyQuestion question1 = new SurveyQuestion()
            {
                Id = 1,
                Question = "Pitanje1",
                Section = SurveySectionType.Doctor,
                IdSurvey = 3
            };
            SurveyQuestion question2 = new SurveyQuestion()
            {
                Id = 2,
                Question = "Pitanje2",
                Section = SurveySectionType.Hospital,
                IdSurvey = 3
            };
            //Act
            inMemoryRepo.AddSurveyQuestion(question1);
            inMemoryRepo.AddSurveyQuestion(question2);

            var controller = new SurveyController();
            controller.surveyService = new SurveyService(inMemoryRepo);
            var response = controller.GetQuestions();
            var result = response.Result as OkObjectResult;
            //Assert
            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(result.Value);
        }

        [Fact]
        public void Test_survey_found()
        {   //Arrange
            inMemoryRepo = GetInMemorySurveyRepository();

            Survey survey = new Survey()
            {
                IdSurvey = 4,
                CreationDate = DateTime.Now,
                IdAppointment = 1
            };
            //Act
            inMemoryRepo.AddSurvey(survey);

            var controller = new SurveyController();
            controller.surveyService = new SurveyService(inMemoryRepo);
            var response = controller.GetSurvey(4);
            //Assert
            Assert.Equal(4, response.Value.IdSurvey);
        }

        [Fact]
        public void Test_post_correct_answers()
        {   //Arrange
            inMemoryRepo = GetInMemorySurveyRepository();
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
                Answer = 2
            };
            //Act
            dtos.Add(answer1);
            dtos.Add(answer2);

            var controller = new SurveyController();
            controller.surveyService = new SurveyService(inMemoryRepo);
            var response = controller.PostSuveyQuestions(dtos);
            var result = response.Result as OkObjectResult;
            //Assert
            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(result.Value);
        }

        [Fact]
        public void Test_post_wrong_answers()
        {   //Arrange
            inMemoryRepo = GetInMemorySurveyRepository();
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

            var controller = new SurveyController();
            controller.surveyService = new SurveyService(inMemoryRepo);
            var response = controller.PostSuveyQuestions(dtos);
            var result = response.Result as BadRequestResult;
            //Assert
            Assert.Equal(400, result.StatusCode);
        }
    }
}
