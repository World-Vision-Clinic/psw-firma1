using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace HospitalTests.PatientPortalTests.E2ETests
{
    public class BlockPatientE2ETest
    {
        private readonly IWebDriver driver;
        private LoginPageManager loginPage;
        private UsersPage usersPage;
        private HomePageManager homePage;

        public BlockPatientE2ETest()
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

            loginPage.InsertUsername("pera");
            loginPage.InsertPassword("123");
            loginPage.ClickLogin();

            homePage = new HomePageManager(driver);
            homePage.EnsurePageIsDisplayed();

            usersPage = new UsersPage(driver);
            usersPage.Navigate();
            usersPage.EnsurePageIsDisplayed();
            Assert.True(usersPage.BlockDisplayed());

        }

        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }

        [Fact]
        public void TestBlockUser()
        {
            usersPage.ClickBlock();
            usersPage.Navigate();
            usersPage.EnsureUserDisplayed();
            Assert.False(usersPage.BlockDisplayed());
            Assert.True(usersPage.UserDisplayed());
        }
    }
}
