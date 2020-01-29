using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace SuperMed.Tests.E2E.Pages
{
    public class addAppoinmentStep2
    {
        private readonly IWebDriver _driver;
        public addAppoinmentStep2(IWebDriver driver)
        {
            _driver = driver;
        }
        private IWebElement appoinmentTime => _driver.FindElement(By.CssSelector("input[value='11:00']"));
        private IWebElement goToStep3 => _driver.FindElement(By.Id("DelButton"));
        public addAppoinmentStep2 selectHour()
        {
            appoinmentTime.Click();
            return this;
        }
        public addAppoinmentStep2 GoToStep3()
        {
            goToStep3.Click();
            return this;
        }
    }
}
