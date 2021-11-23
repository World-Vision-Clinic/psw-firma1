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
using System.Linq;
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
        {

            //Arrange
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
        public void Test_post_correct_answers()
        {

            //Arrange
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
        {

            //Arrange
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

        [Fact]
        public void Test_check_survey_breakdown()
        {
            inMemoryRepo = GetInMemorySurveyRepository();

            AnsweredSurveyQuestion question1Answer1 = new AnsweredSurveyQuestion
            {
                Id = 1,
                Question = "Pitanje1",
                Section = SurveySectionType.Hospital,
                SurveyForeignKey = 3,
                PatientForeignKey = 1,
                Answer = 5
            };

            AnsweredSurveyQuestion question2Answer1 = new AnsweredSurveyQuestion
            {
                Id = 2,
                Question = "Pitanje2",
                Section = SurveySectionType.Hospital,
                SurveyForeignKey = 3,
                PatientForeignKey = 1,
                Answer = 2
            };

            AnsweredSurveyQuestion question1Answer2 = new AnsweredSurveyQuestion
            {
                Id = 3,
                Question = "Pitanje1",
                Section = SurveySectionType.Hospital,
                SurveyForeignKey = 4,
                PatientForeignKey = 1,
                Answer = 1
            };

            AnsweredSurveyQuestion question2Answer2 = new AnsweredSurveyQuestion
            {
                Id = 4,
                Question = "Pitanje2",
                Section = SurveySectionType.Hospital,
                SurveyForeignKey = 4,
                PatientForeignKey = 1,
                Answer = 2
            };

            inMemoryRepo.AddAnswer(question1Answer1);
            inMemoryRepo.AddAnswer(question2Answer1);
            inMemoryRepo.AddAnswer(question1Answer2);
            inMemoryRepo.AddAnswer(question2Answer2);

            var controller = new SurveyController();
            controller.surveyService = new SurveyService(inMemoryRepo);

            var response = controller.GetAnsweredQuestionsBreakdown();
            Assert.Equal(2, response.Value.Count());
            Assert.Equal(3, response.Value.ElementAt(0).Average);
            Assert.Equal(2, response.Value.ElementAt(1).Average);
        }
    }
}
