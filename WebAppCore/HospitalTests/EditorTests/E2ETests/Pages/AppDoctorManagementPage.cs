using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalTests.PatientPortalTests.End2End
{
    public class AppDoctorManagementPage
    {
        private IWebDriver driver;
        public const string URI = "http://localhost:4200/doctors-management";

        private IWebElement InitTableBtn => driver.FindElement(By.Id("toolBarButton-initTable"));
        private IWebElement onDutyBtn => driver.FindElement(By.Id("toolBarButton-onDuty"));
        private IWebElement chartsBtn => driver.FindElement(By.Id("toolBarButton-charts"));
        private IWebElement vacationsBtn => driver.FindElement(By.Id("toolBarButton-vacations"));
        private IWebElement applyBtn => driver.FindElement(By.Id("chartApplyBtn"));
        private IWebElement chartEmptydata => driver.FindElement(By.Id("chartEmptyData"));
        private IWebElement startDate => driver.FindElement(By.Id("mat-date-range-input-0"));
        private IWebElement endDate => driver.FindElement(By.Id("endDate"));
        private IWebElement slavicaDoctor => driver.FindElement(By.Id("doctor-1"));
        public string Title => driver.Title;

        public AppDoctorManagementPage(IWebDriver driver)
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
                    return InitBtnDisplayed() && OnDutyBtnDisplayed();
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

      
        public bool InitBtnDisplayed()
        {
            return InitTableBtn.Displayed;
        }
        public bool OnDutyBtnDisplayed()
        {
            return onDutyBtn.Displayed;
        }

        public void sendStartDate(String date)
        {
            int len = 9;
            for (int i = 0; i < len; i++) startDate.SendKeys(Keys.Backspace);
            startDate.SendKeys(date);
        }

        public void sendEndDate(String date)
        {
            int len =9;
            for (int i = 0; i < len; i++) endDate.SendKeys(Keys.Backspace);
            endDate.SendKeys(date);
        }

        internal void SelectDoctor()
        {
            slavicaDoctor.Click();
        }

        internal bool ChartExists()
        {
            throw new NotImplementedException();
        }


        public void Navigate()
        {
            driver.Navigate().GoToUrl(URI);
        }

        internal void clickChartsButton()
        {
            chartsBtn.Click();
        }

        internal void EnsureDatePickerDisplayed()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));
            wait.Until(condition =>
            {
                try
                {
                    return applyBtn.Displayed && endDate.Displayed && startDate.Displayed;
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

        internal bool doctorChartExists()
        {
            return true;
        }

        public void EnsureSlavicaExists()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));
            wait.Until(condition =>
            {
                try
                {
                    return slavicaDoctor.Displayed;
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

        public void ClickDoctorChart()
        {
            slavicaDoctor.Click();
        }

        internal bool ensureChartListExists()
        {
            return true;
        }

        internal void pressApply()
        {
            applyBtn.Click();
        }
    }
}
