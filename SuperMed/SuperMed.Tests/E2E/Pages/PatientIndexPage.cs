using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace SuperMed.Tests.E2E.Pages
{
    public class PatientIndexPage
    {
        private readonly IWebDriver _driver;
        public PatientIndexPage(IWebDriver driver)
        {
            _driver = driver;
        }
        public IWebElement addAppoinmentButton => _driver.FindElement(By.XPath("//a[contains(text(), 'Zarejestruj wizytę')]"));

        public PatientIndexPage GoTo_addAppoinmentPage()
        {
            addAppoinmentButton.Click();
            return this;
        }

    }
}
