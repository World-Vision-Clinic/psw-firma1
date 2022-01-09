using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalTests.PatientPortalTests.End2End
{
    public class CreateFeedbackPage
    {
        private IWebDriver driver;
        public const string URI = "http://localhost:4200/create-feedback";

        private IWebElement FeedbackBtn => driver.FindElement(By.Id("createBtn"));
        private IWebElement ContentField => driver.FindElement(By.Id("contentField"));
        public string Title => driver.Title;

        public CreateFeedbackPage(IWebDriver driver)
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
                    return FeedbackBtn.Displayed && ContentField.Displayed;
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

        public bool FeedbackBtnDisplayed()
        {
            return FeedbackBtn.Displayed;
        }
        public bool ContentFieldDisplayed()
        {
            return ContentField.Displayed;
        }
        public void InsertContent(string content)
        {
            ContentField.SendKeys(content);
        }
        public void ClickCreateFeedback()
        {
            FeedbackBtn.Click();
        }

        public void Navigate()
        {
            driver.Navigate().GoToUrl(URI);
        }
    }
}
