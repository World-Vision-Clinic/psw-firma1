using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationTests.EndToEndTests.Pages
{
    public class PharmaciesPage
    {
        private readonly IWebDriver driver;
        public const string URI = "http://localhost:4200/manager-front-app/(showObjRepl:overview-pharmacies)";

        private IWebElement InputNameElement => driver.FindElement(By.Id("medicine_name"));
        private IWebElement InputWeightElement => driver.FindElement(By.Id("medicine_weight"));
        private IWebElement InputQuantityElement => driver.FindElement(By.Id("medicine_quantity"));
        private IWebElement SearchButtonElement => driver.FindElement(By.Id("searchBtn"));
        private IWebElement OrderButtonElement => driver.FindElement(By.Id("orderButton"));

        public const string SuccessfulOrderingMessage = "Medicine succesfully ordered";
        public PharmaciesPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void SearchPharmacies()
        {
            SearchButtonElement.Click();
        }

        public void OdrerMedicine()
        {
            OrderButtonElement.Click();
        }

        public void InsertName(string name)
        {
            InputNameElement.SendKeys(name);
        }

        public void InsertWeight(string weight)
        {
            InputWeightElement.SendKeys(weight);
        }
        public void InsertQuantity(string quantity)
        {
            InputQuantityElement.SendKeys(quantity);
        }

        public string GetDialogMessage()
        {
            return driver.SwitchTo().Alert().Text;
        }

        public void WaitForAlertDialog()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());
        }

        public void WaitForOrderButton()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("orderButton")));
        }

        public void Navigate() => driver.Navigate().GoToUrl(URI);
    }

}