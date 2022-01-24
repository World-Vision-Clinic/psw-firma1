using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalTests.PatientPortalTests.End2End
{
    public class LoginPage
    {
        private IWebDriver driver;
        public const string URI = "http://localhost:4200/login";

        private IWebElement Username => driver.FindElement(By.Id("username"));
        private IWebElement Password => driver.FindElement(By.Id("pass"));
        private IWebElement LoginBtn => driver.FindElement(By.Id("loginBtn"));
        public string Title => driver.Title;

        public LoginPage(IWebDriver driver)
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
                    return LogInDisplayed() && UsernameDisplayed() && PasswordDisplayed();
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

        public bool LogInDisplayed()
        {
            return LoginBtn.Displayed;
        }
        public bool UsernameDisplayed()
        {
            return Username.Displayed;
        }
        public bool PasswordDisplayed()
        {
            return Password.Displayed;
        }

        public void InsertUsername(string username) 
        {
            Username.SendKeys(username);
        }
        public void InsertPassword(string pass)
        {
            Password.SendKeys(pass);
        }

        public void ClickLogin()
        {
            LoginBtn.Click();
        }

        public void Navigate()
        {
            driver.Navigate().GoToUrl(URI);
        }
    }
}
