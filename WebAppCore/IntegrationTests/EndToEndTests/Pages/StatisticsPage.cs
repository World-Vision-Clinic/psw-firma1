using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace IntegrationTests.EndToEndTests.Pages
{
    public class StatisticsPage
    {
        private readonly IWebDriver driver;
        public const string URI = "http://localhost:4200/manager-front-app/(showObjRepl:statistics)";

        private IWebElement TitleTenderStatistics => driver.FindElement(By.Id("tender-statistics"));
        private IWebElement LabelStartDate => driver.FindElement(By.Id("start-date-label"));
        private IWebElement InputStartDate => driver.FindElement(By.Name("start-date"));
        private IWebElement LabelEndDate => driver.FindElement(By.Id("end-date-label"));
        private IWebElement InputEndDate => driver.FindElement(By.Name("end-date"));
        private IWebElement GenerateButton => driver.FindElement(By.Id("generateButton"));

        public StatisticsPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void InsertStartDate(string startDate)
        {
            InputStartDate.SendKeys(startDate);
        }

        public void InsertEndDate(string endDate)
        {
            InputEndDate.SendKeys(endDate);
        }

        public void Generate()
        {
            GenerateButton.Click();
        }

        public string GetDialogMessage()
        {
            return driver.SwitchTo().Alert().Text;
        }

        public void WaitForAlertDialog()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 10));

            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());
        }

        public bool WaitForNewWindow(IWebDriver driver, int timeout)
        {
            bool flag = false;
            int counter = 0;
            while (!flag)
            {
                try
                {
                    if (driver.WindowHandles.Count > 1)
                    {
                        flag = true;
                        return flag;
                    }
                    Thread.Sleep(1000);
                    counter++;
                    if (counter > timeout)
                    {
                        return flag;
                    }
                }
                catch
                {
                    return false;
                }
            }
            return flag;
        }

        public void Navigate() => driver.Navigate().GoToUrl(URI);
    }
}
