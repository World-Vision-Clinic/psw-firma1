using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationTests.EndToEndTests.Pages
{
    public class TenderPage
    {
        private readonly IWebDriver driver;
        public const string URI = "http://localhost:4200/manager-front-app/(showObjRepl:create-tender)";
        private IWebElement InputTenderTitle => driver.FindElement(By.Name("tender-title"));
        private IWebElement InputStartDate => driver.FindElement(By.Name("start-date"));
        private IWebElement InputTenderDescriptioin => driver.FindElement(By.Name("tender-description"));
        private IWebElement InputMedicineName => driver.FindElement(By.Name("medicine-name"));
        private IWebElement InputDosage => driver.FindElement(By.Name("dosage"));
        private IWebElement InputQuantity => driver.FindElement(By.Name("quantity"));
        private IWebElement AddMedicineButton => driver.FindElement(By.Id("add-medicine"));
        private IWebElement CreateTenderButton => driver.FindElement(By.Id("create-tender"));
        public const string SuccessfulOrderingMessage = "Successfull.";
        public const string UnsuccessfulOrderingMessage = "Please fill all fields";

        public TenderPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void InsertStartDate(string startDate)
        {
            InputStartDate.SendKeys(startDate);
        }
        public void InsertTitle(string title)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("document.body.style.transform='scale(0.8)';");
            InputTenderTitle.SendKeys(title);
        }
        public void InsertDescription(string description)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("document.body.style.transform='scale(0.8)';");
            InputTenderDescriptioin.SendKeys(description);
        }
        public void InsertMedicineName(string name)
        {
            InputMedicineName.SendKeys(name);
        }
        public void InsertDosage(string dosage)
        {
            InputDosage.SendKeys(dosage);
        }
        public void InsertQuantity(string quantity)
        {
            InputQuantity.SendKeys(quantity);
        }
        public void AddMedicine()
        {
            AddMedicineButton.Click();
        }
        public void AddTender()
        {
            //((IJavaScriptExecutor)driver).ExecuteScript("document.body.style.transform='scale(0.8)';");
            //var button = WebDriverWait(browser, 10).until(EC.element_to_be_clickable((By.CSS_SELECTOR, 'button.dismiss')))
            
            //button.click()
            CreateTenderButton.Click();

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
        public void Wait()
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("document.body.style.transform='scale(0.5)';");
            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0,0,10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(driver.FindElement(By.Id("create-tender"))));
        }
        public void AcceptAlert()
        {
            driver.SwitchTo().Alert().Accept();
        }


        public void Navigate() => driver.Navigate().GoToUrl(URI);

    }
}
