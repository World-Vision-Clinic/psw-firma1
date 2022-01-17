using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace IntegrationTests.EndToEndTests
{
    public class ObjectionCreatingTest : IDisposable
    {
        private readonly IWebDriver driver;
        private Pages.ObjectionPage objectionPage;

        public ObjectionCreatingTest()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("start-maximized");            // open Browser in maximized mode
            options.AddArguments("disable-infobars");           // disabling infobars
            options.AddArguments("--disable-extensions");       // disabling extensions
            options.AddArguments("--disable-gpu");              // applicable to windows os only
            options.AddArguments("--disable-dev-shm-usage");    // overcome limited resource problems
            options.AddArguments("--no-sandbox");               // Bypass OS security model
            options.AddArguments("--disable-notifications");    // disable notifications

            driver = new ChromeDriver(options);

            objectionPage = new Pages.ObjectionPage(driver);
            objectionPage.Navigate();
        }

        [Fact]
        public void Create_objection()
        {
            objectionPage.WaitForPharmacies();
            objectionPage.ChoosePharmacy("Jankovic");
            objectionPage.InsertText("Ne valja usluga");
            objectionPage.SendObjection();
            objectionPage.WaitForAlertDialog();
            Assert.Equal(objectionPage.GetDialogMessage(), Pages.ObjectionPage.SuccessfulObjectionCreation);
        }

        [Fact]
        public void Empty_text()
        {
            objectionPage.WaitForPharmacies();
            objectionPage.ChoosePharmacy("Jankovic");
            objectionPage.SendObjection();
            objectionPage.WaitForAlertDialog();
            Assert.Equal(objectionPage.GetDialogMessage(), Pages.ObjectionPage.UnsuccessfulObjectionCreation);
        }

        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}
