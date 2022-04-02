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

        [Theory]
        [InlineData("", "", "Dates must be selected", false)]                                       // forget_to_insert_dates
        [InlineData("", "12/25/2021", "Dates must be selected", false)]                             // forget_to_insert_start_date
        [InlineData("12/19/2021", "", "Dates must be selected", false)]                             // forget_to_insert_end_date
        [InlineData("12/25/2021", "12/19/2021", "End date must be after starting date", false)]     // end_date_before_start_date
        [InlineData("12/19/2021", "12/25/2021", "", true)]                                          // ok
        public void Inserting_dates_test(string startDate, string endDate, string expectedMessage, bool newWindow)
        {
            statisticsPage.InsertStartDate(startDate);
            statisticsPage.InsertEndDate(endDate);

            statisticsPage.Generate();

            Assert.Equal(expectedMessage, statisticsPage.GetDialogMessage());
            Assert.Equal(statisticsPage.WaitForNewWindow(driver, 10), newWindow);
        }

        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}
