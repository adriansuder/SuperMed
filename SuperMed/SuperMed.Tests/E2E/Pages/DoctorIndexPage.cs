using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace SuperMed.Tests.E2E.Pages
{
    public class DoctorIndexPage
    {
        private readonly IWebDriver _driver;
        public DoctorIndexPage(IWebDriver driver)
        {
            _driver = driver;
        }
        public IWebElement addAbsenceButton => _driver.FindElement(By.Id("addAbsenceButton"));
        public IWebElement editAbsenceButton => _driver.FindElement(By.Id("editAbsences"));
        public DoctorIndexPage GoToAddAbsencePage()
        {
            addAbsenceButton.Click();
            return this;
        }
        public DoctorIndexPage GoToEditAbsencePage()
        {
            editAbsenceButton.Click();
            return this;
        }
        public bool isAbsenceDisplayedOnIndexPage(string dateToPass)
        {
            //WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(2));
            //wait.Until(x => _driver.FindElement(By.XPath("//li[@id='"+dateToPass+"']")).Displayed);
            try
            {
                _driver.FindElement(By.XPath("//li[@id='" + dateToPass + "']"));
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

    }
}
