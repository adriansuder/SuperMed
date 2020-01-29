using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace SuperMed.Tests.E2E.Pages
{
    public class EditAppoinmentPage
    {
        private readonly IWebDriver _driver;
        public EditAppoinmentPage(IWebDriver driver)
        {
            _driver = driver;
        }
        private IWebElement cancellVisitButton => _driver.FindElement(By.ClassName("cancellVisit"));
        public EditAppoinmentPage confirmVisitCancellation()
        {
            cancellVisitButton.Click();
            return this;
        }
        public EditAppoinmentPage acceptAlert()
        {
            _driver.SwitchTo().Alert().Accept();
            return this;
        }
    }
}
