using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalTests.PatientPortalTests.E2ETests
{
   public  class FeedbackPage
    {
        private IWebDriver driver;
        public const string URI = "http://localhost:4200/manager-feedback";

        private IWebElement Publish => driver.FindElement(By.Id("publish1"));
        private IWebElement Unpublish => driver.FindElement(By.Id("unpublish1"));        
        public string Title => driver.Title;

        public FeedbackPage(IWebDriver driver)
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
                    return Publish.Displayed;
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

        public void EnsureUnpublishedIsDisplayed()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));
            wait.Until(condition =>
            {
                try
                {
                    return Unpublish.Displayed;
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

        public bool PublishDisplayed()
        {
            return Publish.Displayed;
        }
        public bool UnpublishDisplayed()
        {
            return Unpublish.Displayed;
        }
       
        public void ClickPublish()
        {
            Publish.Click();
        }

        public void Navigate()
        {
            driver.Navigate().GoToUrl(URI);
        }
    }
}
