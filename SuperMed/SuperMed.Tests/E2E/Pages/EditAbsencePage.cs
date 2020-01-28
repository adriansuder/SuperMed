using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace SuperMed.Tests.E2E.Pages
{
    public class EditAbsencePage
    {
        private readonly IWebDriver _driver;
        public EditAbsencePage(IWebDriver driver)
        {
            _driver = driver;
        }
        public EditAbsencePage clickOnDeleteButton(string id)
        {
            _driver.FindElement(By.Id(id)).Click();
            return this;
        }
        public EditAbsencePage acceptAlert()
        {
            _driver.SwitchTo().Alert().Accept();
            return this;
        }
    }
}
