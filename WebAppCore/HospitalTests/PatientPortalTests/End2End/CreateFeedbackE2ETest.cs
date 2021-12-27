using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace HospitalTests.PatientPortalTests.End2End
{
    public class CreateFeedbackE2ETest : IDisposable
    {
        private readonly IWebDriver driver;
        private LandingPage landingPage;
        private LoginPage loginPage;
        private HomePage homePage;
        private CreateFeedbackPage createFeedbackPage;

        public CreateFeedbackE2ETest()
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
            /*
            landingPage = new LandingPage(driver);
            landingPage.Navigate();
            landingPage.EnsurePageIsDisplayed();
            Assert.True(landingPage.SignInDisplayer());
            landingPage.ClickSignIn();*/

            loginPage = new LoginPage(driver);
            loginPage.Navigate();
            loginPage.EnsurePageIsDisplayed();
            Assert.True(loginPage.LogInDisplayed());
            Assert.True(loginPage.UsernameDisplayed());
            Assert.True(loginPage.PasswordDisplayed());

            loginPage.InsertUsername("mihajlo");
            loginPage.InsertPassword("123");
            loginPage.ClickLogin();

            homePage = new HomePage(driver);
            homePage.EnsurePageIsDisplayed();
            Assert.True(homePage.FeedbackDisplayed());
            Assert.True(homePage.SignOutDisplayed());

            homePage.ClickFeedback();

            createFeedbackPage = new CreateFeedbackPage(driver);
            createFeedbackPage.EnsurePageIsDisplayed();
            Assert.True(createFeedbackPage.FeedbackBtnDisplayed());
            Assert.True(createFeedbackPage.ContentFieldDisplayed());
        }

        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }

        [Fact]
        public void TestCreateFeedback() 
        {
            createFeedbackPage.InsertContent("komentar");
            createFeedbackPage.ClickCreateFeedback();
            homePage.EnsurePageIsDisplayed();
            Assert.True(homePage.FeedbackDisplayed());
            Assert.True(homePage.SignOutDisplayed());
            Assert.True(homePage.PatientNameDisplayed());
        }
    }
}
