using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalTests.PatientPortalTests.E2ETests
{
    public class UsersPage
    {
        private IWebDriver driver;
        public const string URI = "http://localhost:4200/block-patients";

        private IWebElement Block => driver.FindElement(By.Id("block0"));
        private IWebElement User => driver.FindElement(By.Id("user0"));
        public string Title => driver.Title;

        public UsersPage(IWebDriver driver)
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
                    return Block.Displayed && User.Displayed;
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
        public void EnsureUserDisplayed()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));
            wait.Until(condition =>
            {
                try
                {
                    return User.Displayed;
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

        public bool BlockDisplayed()
        {
            try
            {
                return Block.Displayed;
            }
            catch (NoSuchElementException e)
            {
                return false;
            }
        }

        public bool UserDisplayed()
        {
            return User.Displayed;
        }

        public void ClickBlock()
        {
            Block.Click();
        }

        public void Navigate()
        {
            driver.Navigate().GoToUrl(URI);
        }
    }
}
