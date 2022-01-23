using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationTests.EndToEndTests.Pages
{
    public class NewsPage
    {
        private readonly IWebDriver driver;
        public const string URI = "http://localhost:4200/manager-front-app/(showObjRepl:news)";


        private IWebElement changeNewsStatusButton => driver.FindElement(By.Id("Test"));

        public const string SuccessfulNewsStatusChange = "You've successfully changed news publish status.";

        public void EnsurePageIsDisplayed()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));
            wait.Until(condition =>
            {
                try
                {
                    return changeNewsStatusButton != null;
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            });
        }

        public NewsPage(IWebDriver driver)
        {
            this.driver = driver;
        }
        public void ChangeStatusOfNews()
        {
            changeNewsStatusButton.Click();
        }

        public void WaitForAlertDialog()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());
        }

        public string GetDialogMessage()
        {
            return driver.SwitchTo().Alert().Text;
        }

        public void Navigate() => driver.Navigate().GoToUrl(URI);
    }
}
