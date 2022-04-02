using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace IntegrationTests.EndToEndTests
{
    public class OrderingMedicineTest : IDisposable
    {
        private readonly IWebDriver driver;
        private Pages.PharmaciesPage pharmaciesPage;
        
        public OrderingMedicineTest()
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


            pharmaciesPage = new Pages.PharmaciesPage(driver);      // create ProductsPage
            pharmaciesPage.Navigate();              
            
        }

        [Fact]
        public void Ordering_existing_medicine_test()
        {
            InsertData("Andol", "200", "2");

            pharmaciesPage.SearchPharmacies();

            pharmaciesPage.WaitForOrderButton();
            pharmaciesPage.OdrerMedicine();
            pharmaciesPage.WaitForAlertDialog();
            Assert.Equal(pharmaciesPage.GetDialogMessage(), Pages.PharmaciesPage.SuccessfulOrderingMessage);
        }

        [Fact]
        public void Ordering_unexisting_medicine_test()
        {
            InsertData("Blabla", "200", "2");

            pharmaciesPage.SearchPharmacies();

            pharmaciesPage.WaitForMessage();
            Assert.True(pharmaciesPage.CheckMessage());
        }

        private void InsertData(string medicineName, string weight, string quantity)
        {
            pharmaciesPage.InsertName(medicineName);
            pharmaciesPage.InsertWeight(weight);
            pharmaciesPage.InsertQuantity(quantity);
        }


        [Theory]
        [InlineData("Andol", "sss", "2")]
        [InlineData("Andol", "", "2")]
        public void Wrong_user_input_test(string medicineName, string weight, string quantity)
        {
            pharmaciesPage.InsertName(medicineName);
            pharmaciesPage.InsertWeight(weight);
            pharmaciesPage.InsertQuantity(quantity);
            pharmaciesPage.SearchPharmacies();

            pharmaciesPage.WaitForAlertDialog();

            Assert.Equal(pharmaciesPage.GetDialogMessage(), Pages.PharmaciesPage.UnsuccessfulOrderingMessage);
        }

        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}
