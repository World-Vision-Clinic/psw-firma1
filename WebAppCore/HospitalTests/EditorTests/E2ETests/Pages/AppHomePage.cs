using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalTests.PatientPortalTests.End2End
{
    public class AppHomePage
    {
        private IWebDriver driver;
        public const string URI = "http://localhost:4200/manager-feedback";

        private IWebElement HospitalEditor => driver.FindElement(By.Id("hospitalEditorBtn"));
        private IWebElement SignOut => driver.FindElement(By.Id("signout"));
        public string Title => driver.Title;

        public AppHomePage(IWebDriver driver)
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
                    return  SignOut.Displayed ;
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


        public bool SignOutDisplayed()
        {
            return SignOut.Displayed;
        }
       
        public void HospitalEditorClick()
        {
            HospitalEditor.Click();
        }
       

        public void Navigate()
        {
            driver.Navigate().GoToUrl(URI);
        }
    }
}
