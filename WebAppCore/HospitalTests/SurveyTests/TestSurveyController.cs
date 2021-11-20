using Hospital.Schedule.Model;
using Hospital.Schedule.Repository;
using Hospital.Schedule.Service;
using Hospital.SharedModel;
using Hospital_API;
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

        public TestSurveyController()
        {

        }

        private ISurveyRepository GetInMemoryPersonRepository()
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
            inMemoryRepo = GetInMemoryPersonRepository();
            SurveyQuestion question1 = new SurveyQuestion()
            {
                Id = 1,
                Question = "Pitanje1",
                Section = SurveySectionType.Doctor
            };

            SurveyQuestion question2 = new SurveyQuestion()
            {
                Id = 2,
                Question = "Pitanje2",
                Section = SurveySectionType.Hospital
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
    }
}
