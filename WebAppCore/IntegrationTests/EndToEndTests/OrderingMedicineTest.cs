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
        public void OrderingExistingMedicineTest()
        {
            pharmaciesPage.InsertName("Andol");          
            pharmaciesPage.InsertWeight("200");
            pharmaciesPage.InsertQuantity("2");
            pharmaciesPage.SearchPharmacies();

            pharmaciesPage.WaitForOrderButton();
            
            pharmaciesPage.OdrerMedicine();
            pharmaciesPage.WaitForAlertDialog();

            Assert.Equal(pharmaciesPage.GetDialogMessage(), Pages.PharmaciesPage.SuccessfulOrderingMessage);
        }

        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}
