using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace SuperMed.Tests.E2E.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;
        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
        }
        public IWebElement userName => _driver.FindElement(By.Id("Name"));
        public IWebElement userPassword => _driver.FindElement(By.Id("Password"));
        public IWebElement loginButton => _driver.FindElement(By.ClassName("btn-primary"));

        public LoginPage fillUserName(string textToPass)
        {
            userName.SendKeys(textToPass);
            return this;
        }
        public LoginPage fillUserPassword(string textToPass)
        {
            userPassword.SendKeys(textToPass);
            return this;
        }
        public LoginPage ClickLogin()
        {
            loginButton.Click();
            return this;
        }

    }
}
