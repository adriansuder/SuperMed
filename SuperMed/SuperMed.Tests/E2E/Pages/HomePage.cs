using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperMed.Tests.E2E.Pages
{
    public class HomePage
    {
        private readonly IWebDriver _driver;
        private const string URL = "https://localhost:44324";

        private IWebElement LoginPageButton => _driver.FindElement(By.Id("LoginPageButton"));
        private IWebElement RegisterPatientButton => _driver.FindElement(By.Id("registerPatient"));
        private IWebElement RegisterDoctorButton => _driver.FindElement(By.XPath("/html/body/div/main/div[2]/div/div[2]/div/p/a"));

        public HomePage(IWebDriver driver)
        {
            _driver = driver;
        }
        public HomePage GoToHomePage()
        {
            _driver.Navigate()
            .GoToUrl(URL);
            return this;
        }
        public HomePage GoToLoginPage()
        {
            LoginPageButton.Click();
            return this;
        }
        public HomePage GoToRegisterPatientPage()
        {
            RegisterPatientButton.Click();
            return this;
        }
        public HomePage GoToRegisterDoctorPage()
        {
            RegisterDoctorButton.Click();
            return this;
        }
    }
}

