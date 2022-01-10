using HospitalTests.PatientPortalTests.End2End;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HospitalTests.PatientPortalTests.E2ETests
{
    public class CancelAppointmentE2ETest : IDisposable
    {
        private readonly IWebDriver driver;
        private LandingPage landingPage;
        private LoginPage loginPage;
        private HomePage homePage;

        public CancelAppointmentE2ETest()
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

            loginPage = new LoginPage(driver);
            loginPage.Navigate();
            loginPage.EnsurePageIsDisplayed();
            Assert.True(loginPage.LogInDisplayed());
            Assert.True(loginPage.UsernameDisplayed());
            Assert.True(loginPage.PasswordDisplayed());

            loginPage.InsertUsername("Abcd");
            loginPage.InsertPassword("aaa");
            loginPage.ClickLogin();

            homePage = new HomePage(driver);
            homePage.EnsurePageIsDisplayed();
            Assert.True(homePage.CancelDisplayed());
            Assert.True(homePage.SignOutDisplayed());
        }
        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }

        [Fact]
        public void TestCancelAppointment()
        {
            homePage.ClickCancel();
            homePage.EnsurePageIsDisplayed();
            homePage.CancelDisabled();
        }
    }
}
