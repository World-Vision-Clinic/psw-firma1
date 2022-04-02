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

        [Theory]
        [InlineData("Jankovic", "Ne valja usluga", Pages.ObjectionPage.SuccessfulObjectionCreation)]
        [InlineData("Jankovic", "", Pages.ObjectionPage.UnsuccessfulObjectionCreation)]
        public void Create_objection_test(string pharmacy, string text, string expected)
        {
            objectionPage.WaitForPharmacies();
            objectionPage.ChoosePharmacy(pharmacy);
            objectionPage.InsertText(text);

            objectionPage.SendObjection();
            objectionPage.WaitForAlertDialog();

            Assert.Equal(objectionPage.GetDialogMessage(), expected);
        }

        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}
