using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalTests.PatientPortalTests.E2ETests
{
    public class HomePageManager
    {
        private IWebDriver driver;
        public const string URI = "http://localhost:4200/";

        private IWebElement UsersLink => driver.FindElement(By.Id("users"));
        private IWebElement FeedbackLink => driver.FindElement(By.Id("feedback"));
        public string Title => driver.Title;

        public HomePageManager(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void EnsurePageIsDisplayed()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));
            wait.Until(condition =>
            {
                try
                {
                    return FeedbackLink.Displayed && UsersLink.Displayed;
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
        public void Navigate()
        {
            driver.Navigate().GoToUrl(URI);
        }
    }
}
