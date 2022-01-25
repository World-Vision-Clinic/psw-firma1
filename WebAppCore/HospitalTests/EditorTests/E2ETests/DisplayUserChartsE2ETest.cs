using HospitalTests.PatientPortalTests.End2End;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace HospitalTests.PatientPortalTests.E2ETests
{
    public class DisplayUserChartsE2ETest : IDisposable
    {
        private readonly IWebDriver driver;
        private LoginPageManager loginPage;
        private FeedbackPage feedbackPage;
        private AppHomePage homePage;
        private AppDoctorManagementPage doctorManagmentPage;

        public DisplayUserChartsE2ETest()
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

            loginPage.InsertUsername("manager");
            loginPage.InsertPassword("manager");
            loginPage.ClickLogin();

            homePage = new AppHomePage(driver);
            homePage.EnsurePageIsDisplayed();
            homePage.HospitalEditorClick();

            doctorManagmentPage = new AppDoctorManagementPage(driver);
            doctorManagmentPage.Navigate();
            doctorManagmentPage.EnsurePageIsDisplayed();

            
        }

        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }

        [Fact]
        public void TestChartListEmpty()
        {
            doctorManagmentPage.clickChartsButton();
            doctorManagmentPage.EnsureDatePickerDisplayed();
            doctorManagmentPage.pressApply();

            Assert.True(doctorManagmentPage.ensureChartListExists());
        }

        [Fact]
        public void CheckChartListPie()
        {
            doctorManagmentPage.clickChartsButton();
            doctorManagmentPage.EnsureDatePickerDisplayed();
            doctorManagmentPage.sendStartDate("1/1/2022");
            doctorManagmentPage.sendEndDate("1/31/2022");
            doctorManagmentPage.pressApply();

            doctorManagmentPage.EnsureSlavicaExists();
            doctorManagmentPage.SelectDoctor();
            Assert.True(doctorManagmentPage.doctorChartExists());
        }
    }
}
