using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace HospitalTests.PatientPortalTests.E2ETests
{
    public class PublishFeedbackE2ETest : IDisposable
    {
        private readonly IWebDriver driver;
        private LoginPageManager loginPage;
        private FeedbackPage feedbackPage;
        private HomePageManager homePage;

        public PublishFeedbackE2ETest()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("start-maximized");
            options.AddArgument("disable-infobars");
            options.AddArgument("--disable-extensions");
            options.AddArgument("--disable-gpu");
            options.AddArgument("--disable-dev-shm-usage");
            options.AddArgument("--no-sandbox");
            options.AddArgument("--dsiable-notifications");

            driver = new ChromeDriver(options);
           

            loginPage = new LoginPageManager(driver);
            loginPage.Navigate();
            loginPage.EnsurePageIsDisplayed();
            Assert.True(loginPage.LogInDisplayed());
            Assert.True(loginPage.UsernameDisplayed());
            Assert.True(loginPage.PasswordDisplayed());

            loginPage.InsertUsername("ftn");
            loginPage.InsertPassword("ftn");
            loginPage.ClickLogin();

            homePage = new HomePageManager(driver);
            homePage.EnsurePageIsDisplayed();

            feedbackPage = new FeedbackPage(driver);
            feedbackPage.Navigate();
            feedbackPage.EnsurePageIsDisplayed();
            Assert.True(feedbackPage.PublishDisplayed());

        }

        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }

        [Fact]
        public void TestPublishFeedback()
        {
            feedbackPage.ClickPublish();
            feedbackPage.EnsureUnpublishedIsDisplayed();
            Assert.True(feedbackPage.UnpublishDisplayed());
        }
    }
}
