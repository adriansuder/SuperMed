using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace SuperMed.Tests.E2E.Pages
{
    public class AddAbsencePage
    {
        private readonly IWebDriver _driver;
        public AddAbsencePage(IWebDriver driver)
        {
            _driver = driver;
        }
        private IWebElement absenceDateInput => _driver.FindElement(By.Id("AbsenceDate"));
        private IWebElement absenceDescription => _driver.FindElement(By.Id("AbsenceDescription"));
        private IWebElement addAbsenceButton => _driver.FindElement(By.CssSelector("input[value='Dodaj']"));
        
        public AddAbsencePage fillAbsenceDate(string textToPass)
        {
            absenceDateInput.SendKeys(textToPass);
            return this;
        }
        public AddAbsencePage fillAbsenceDescription(string textToPass)
        {
            absenceDescription.SendKeys(textToPass);
            return this;
        }
        public AddAbsencePage addAbsenceClick()
        {
            addAbsenceButton.Click();
            return this;
        }
    }
}
