using Integration.Partnership.Repository;
using Integration.Pharmacy.Repository;
using Integration.Pharmacy.Service;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests.EndToEndTests
{
    public class PublishingNewsTest :IDisposable
    {

        private readonly IWebDriver driver;
        private Pages.NewsPage newsPage;

        public PublishingNewsTest()
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

            newsPage = new Pages.NewsPage(driver);      
            newsPage.Navigate();
           
        }

        [Fact]
        public async void PublishingRecievedNewsTest()
        {
            RabbitMQService rabbitMQ = new RabbitMQService(new NewsRepository(), new PharmaciesRepository(), new TenderRepository(), false);
            CancellationToken token = new CancellationToken(false);
            rabbitMQ.StartAsync(token);


            var client = new RestSharp.RestClient("http://localhost:34616");
            var request = new RestRequest("/news/add");
            var values = new Dictionary<string, object>
            {
                {"title", "Test" }, {"content", "Sve maske su snizene za 15% " }, {"fromDate", "2021-12-16"}, {"toDate", "2021-12-19"}
            };
            request.AddJsonBody(values);
            IRestResponse response = client.Post(request);
            Console.WriteLine("Status: " + response.StatusCode.ToString());

            await Task.Delay(6000);
            //newsPage.EnsurePageIsDisplayed();

            newsPage.ChangeStatusOfNews();
            newsPage.WaitForAlertDialog();

            Assert.Equal(newsPage.GetDialogMessage(), Pages.NewsPage.SuccessfulNewsStatusChange);

        }

        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}
