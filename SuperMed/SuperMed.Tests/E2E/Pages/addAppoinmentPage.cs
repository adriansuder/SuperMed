using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace SuperMed.Tests.E2E.Pages
{
    public class addAppoinmentPage
    {
        private readonly IWebDriver _driver;
        public addAppoinmentPage(IWebDriver driver)
        {
            _driver = driver;
        }
        public IWebElement appoinmentDate => _driver.FindElement(By.Id("StartDateTime"));
        public IWebElement description => _driver.FindElement(By.Id("Description"));
        public IWebElement goToStep2Button => _driver.FindElement(By.Id("GoToStep2"));
        public SelectElement doctorName => new SelectElement(_driver.FindElement(By.Id("DoctorName")));
        public addAppoinmentPage fillDate(string textToPass)
        {
            appoinmentDate.SendKeys(textToPass);
            return this;
        }
        public addAppoinmentPage fillDescription(string textToPass)
        {
            description.SendKeys(textToPass);
            return this;
        }
        public addAppoinmentPage GoToStep2()
        {
            goToStep2Button.Click();
            return this;
        }
        public addAppoinmentPage selectDoctor(string textToPass)
        {
            doctorName.SelectByText(textToPass);
            return this;
        }


    }
}
