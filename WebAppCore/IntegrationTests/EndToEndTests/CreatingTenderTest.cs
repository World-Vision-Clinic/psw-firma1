using Integration_API.Dto;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace IntegrationTests.EndToEndTests
{
    public class CreatingTenderTest : IDisposable
    {
        private readonly IWebDriver driver;
        private Pages.TenderPage tenderPage;
        public CreatingTenderTest()
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


            tenderPage = new Pages.TenderPage(driver);      // create TenderPage
            tenderPage.Navigate();

        }

        [Fact]
        public void Create_tender()
        {
            InsertTenderData("TestTitle", "TestDescription", new MedicineDto("TestMedicine", 100, 10));

            tenderPage.AddTender();

            tenderPage.WaitForAlertDialog();
            tenderPage.AcceptAlert();
            tenderPage.WaitForAlertDialog();
            Assert.Equal(tenderPage.GetDialogMessage(), Pages.TenderPage.SuccessfulOrderingMessage);
        }
        [Fact]
        public void Forget_to_insert_title()
        {
            InsertTenderData("", "TestDescription", new MedicineDto("TestMedicine", 100, 10));

            tenderPage.AddTender();

            tenderPage.WaitForAlertDialog();
            Assert.Equal(tenderPage.GetDialogMessage(), Pages.TenderPage.UnsuccessfulOrderingMessage);
        }

        private void InsertTenderData(string title, string description, MedicineDto medicineDto)
        {
            tenderPage.InsertTitle(title);
            tenderPage.InsertDescription(description);
            tenderPage.InsertMedicineName(medicineDto.Name);
            tenderPage.InsertDosage(medicineDto.DosageInMg.ToString());
            tenderPage.InsertQuantity(medicineDto.Quantity.ToString());
            tenderPage.AddMedicine();
            tenderPage.Wait();
        }

        [Fact]
        public void Forget_to_insert_description()
        {
            tenderPage.InsertTitle("TestTitle");
            tenderPage.InsertMedicineName("TestMedicine");
            tenderPage.InsertDosage("100");
            tenderPage.InsertQuantity("10");
            tenderPage.AddMedicine();

            tenderPage.Wait();
            tenderPage.AddTender();

            tenderPage.WaitForAlertDialog();


            Assert.Equal(tenderPage.GetDialogMessage(), Pages.TenderPage.UnsuccessfulOrderingMessage);
        }
        [Fact]
        public void Forget_to_insert_medicine()
        {
            tenderPage.InsertTitle("TestTitle");
            tenderPage.InsertDescription("TestDescription");

            tenderPage.Wait();
            tenderPage.AddTender();
            tenderPage.WaitForAlertDialog();


            Assert.Equal(tenderPage.GetDialogMessage(), Pages.TenderPage.UnsuccessfulOrderingMessage);
        }
        [Fact]
        public void Forget_to_insert_all()
        {
            tenderPage.Wait();
            tenderPage.AddTender();

            tenderPage.WaitForAlertDialog();


            Assert.Equal(tenderPage.GetDialogMessage(), Pages.TenderPage.UnsuccessfulOrderingMessage);
        }

        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}
