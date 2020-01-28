using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperMed.Tests.E2E.Pages
{
    public class RegisterSuccesfulPage
    {
        private readonly IWebDriver _driver;
        public RegisterSuccesfulPage(IWebDriver driver)
        {
            _driver = driver;
        }
        public IWebElement registerSuccesfulInfo => _driver.FindElement(By.Id("registerSuccesfulInfo"));
        public bool isRegisterSuccesfullInfoDisplayed()
        {
            return registerSuccesfulInfo.Displayed;
        }
    }
}
