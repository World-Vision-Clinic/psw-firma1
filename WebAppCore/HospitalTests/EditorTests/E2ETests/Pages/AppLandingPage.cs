using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalTests.PatientPortalTests.End2End
{
    public class AppLandingPage
    {
        private IWebDriver driver;
        public const string URI = "http://localhost:4200/";

        private IWebElement SignIn => driver.FindElement(By.Id("signin"));
        public string Title => driver.Title;

        public AppLandingPage(IWebDriver driver) 
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
                    return SignInDisplayed();
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

        public bool SignInDisplayed() 
        {
            return SignIn.Displayed;
        }

        public void ClickSignIn()
        {
            SignIn.Click();
        }

        public void Navigate() 
        {
            driver.Navigate().GoToUrl(URI);
        }
    }
}
