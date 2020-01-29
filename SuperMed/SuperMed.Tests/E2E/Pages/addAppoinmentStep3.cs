using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace SuperMed.Tests.E2E.Pages
{
    public class addAppoinmentStep3
    {
        private readonly IWebDriver _driver;
        public addAppoinmentStep3(IWebDriver driver)
        {
            _driver = driver;
        }
        private IWebElement confirmButton => _driver.FindElement(By.CssSelector("input[type='submit']"));
        public addAppoinmentStep3 clickConfirm()
        {
            confirmButton.Click();
            return this;
        }

    }
}
