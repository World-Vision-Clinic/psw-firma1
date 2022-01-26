using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;

namespace IntegrationTests.EndToEndTests
{
    public class GenerateStatisticsTest : IDisposable
    {
        private readonly IWebDriver driver;
        private Pages.StatisticsPage statisticsPage;

        public GenerateStatisticsTest()
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


            statisticsPage = new Pages.StatisticsPage(driver);      // create StatisticsPage
            statisticsPage.Navigate();
        }

        [Fact]
        public void Forget_to_insert_dates()
        {
            statisticsPage.Generate();

            statisticsPage.WaitForAlertDialog();

            Assert.Equal("Dates must be selected", statisticsPage.GetDialogMessage());
        }

        [Fact]
        public void Forget_to_insert_start_date()
        {
            statisticsPage.InsertEndDate("12/25/2021");

            statisticsPage.Generate();

            statisticsPage.WaitForAlertDialog();

            Assert.Equal("Dates must be selected", statisticsPage.GetDialogMessage());
        }

        [Fact]
        public void Forget_to_insert_end_date()
        {
            statisticsPage.InsertStartDate("12/19/2021");

            statisticsPage.Generate();

            statisticsPage.WaitForAlertDialog();

            Assert.Equal("Dates must be selected", statisticsPage.GetDialogMessage());
        }

        [Fact]
        public void End_date_before_start_date()
        {
            statisticsPage.InsertStartDate("12/25/2021");

            statisticsPage.InsertEndDate("12/19/2021");

            statisticsPage.Generate();

            statisticsPage.WaitForAlertDialog();

            Assert.Equal("End date must be after starting date", statisticsPage.GetDialogMessage());
        }

        /*[Fact]
        public void Generate_statistics()
        {
            statisticsPage.InsertStartDate("12/19/2021");

            statisticsPage.InsertEndDate("12/25/2021");

            statisticsPage.Generate();

            Assert.True(statisticsPage.WaitForNewWindow(driver, 10));
        }*/

        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}
