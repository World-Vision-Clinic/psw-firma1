using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationTests.EndToEndTests.Pages
{
    class ObjectionPage
    {
        private readonly IWebDriver driver;
        public const string URI = "http://localhost:4200/manager-front-app/(showObjRepl:create-objection)";


        private IWebElement PharmacySelection => driver.FindElement(By.Id("pharmacySelection"));

        private IWebElement SendButton => driver.FindElement(By.Id("sendButton"));

        private IWebElement TextForm => driver.FindElement(By.Id("exampleFormControlTextarea2"));

        public const string SuccessfulObjectionCreation = "Successful";
        public const string UnsuccessfulObjectionCreation = "Objection wasn't sent!";


        public ObjectionPage(IWebDriver driver)
        {
            this.driver = driver;
        }
        public void SendObjection()
        {
            SendButton.Click();
        }

        public void InsertText(string inputText)
        {
            TextForm.SendKeys(inputText);
        }

        public void ChoosePharmacy(string pharmacyName)
        {
            var selectPharmacy = new SelectElement(PharmacySelection);
            selectPharmacy.SelectByText(pharmacyName);
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

        public void WaitForPharmacies()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("Jankovic")));
        }

        public void Navigate() => driver.Navigate().GoToUrl(URI);
    }
}
