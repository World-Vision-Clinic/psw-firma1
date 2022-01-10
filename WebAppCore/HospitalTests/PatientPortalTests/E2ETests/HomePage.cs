using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalTests.PatientPortalTests.End2End
{
    public class HomePage
    {
        private IWebDriver driver;
        public const string URI = "http://localhost:4200/medical-record";

        private IWebElement Feedback => driver.FindElement(By.Id("createfeedback"));
        private IWebElement MedicalRecord => driver.FindElement(By.Id("medicalrecord"));
        private IWebElement SignOut => driver.FindElement(By.Id("signout"));
        private IWebElement PatientName => driver.FindElement(By.Id("patientName"));
        private IWebElement CancelButton => driver.FindElement(By.XPath(".//button[text()='Cancel']"));
        public string Title => driver.Title;

        public HomePage(IWebDriver driver)
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
                    return Feedback.Displayed && MedicalRecord.Displayed && SignOut.Displayed && PatientName.Displayed;
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
        public bool FeedbackDisplayed()
        {
            return Feedback.Displayed;
        }
        public bool MedicalRecordDisplayed()
        {
            return MedicalRecord.Displayed;
        }
        public bool PatientNameDisplayed()
        {
            return PatientName.Displayed;
        }
        public bool CancelDisplayed()
        {
            return CancelButton.Displayed;
        }
        public bool CancelDisabled()
        {
            return !CancelButton.Enabled;
        }
        public void ClickFeedback()
        {
            Feedback.Click();
        }
        public void ClickCancel()
        {
            CancelButton.Click();
        }

        public void Navigate()
        {
            driver.Navigate().GoToUrl(URI);
        }
    }
}
