using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace SuperMed.Tests.E2E.Pages
{
    public class RegisterPatientPage
    {
        private readonly IWebDriver _driver;

        public RegisterPatientPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public IWebElement patientName => _driver.FindElement(By.Id("patientNameInput"));
        public IWebElement patientPassword => _driver.FindElement(By.Id("patientPasswordInput"));
        public IWebElement patientFirstName => _driver.FindElement(By.Id("patientFirstNameInput"));
        public IWebElement patientLastName => _driver.FindElement(By.Id("patientLastNameInput"));
        public IWebElement patientMail => _driver.FindElement(By.Id("patientMailInput"));
        public IWebElement patientPhone => _driver.FindElement(By.Id("patientPhoneInput"));
        public IWebElement patientBirthday => _driver.FindElement(By.Id("patientBirthdayInput"));
        public SelectElement patientGender => new SelectElement(_driver.FindElement(By.Id("patientGenderSelect")));
        public IWebElement registerButton => _driver.FindElement(By.ClassName("patientRegisterSubmit"));
        public IWebElement validationDiv => _driver.FindElement(By.ClassName("validation-summary-errors"));
        public RegisterPatientPage fillName(string textToPass)
        {
            patientName.SendKeys(textToPass);
            return this;
        }
        public RegisterPatientPage fillPassword(string textToPass)
        {
            patientPassword.SendKeys(textToPass);
            return this;
        }
        public RegisterPatientPage fillFirstName(string textToPass)
        {
            patientFirstName.SendKeys(textToPass);
            return this;
        }
        public RegisterPatientPage fillLastName(string textToPass)
        {
            patientLastName.SendKeys(textToPass);
            return this;
        }
        public RegisterPatientPage fillMail(string textToPass)
        {
            patientMail.SendKeys(textToPass);
            return this;
        }
        public RegisterPatientPage fillPhone(string textToPass)
        {
            patientPhone.SendKeys(textToPass);
            return this;
        }
        public RegisterPatientPage fillBirthday(string textToPass)
        {
            patientBirthday.SendKeys(textToPass);
            return this;
        }
        public RegisterPatientPage fillGender(string textToPass)
        {
            patientGender.SelectByText(textToPass);
            return this;
        }

        public RegisterPatientPage clickRegister()
        {
            Actions actions = new Actions(_driver);
            actions.MoveToElement(registerButton);
            registerButton.Click();
            return this;
        }
        public bool isValidationErrorDisplayed()
        {
            return validationDiv.Displayed;
        }


    }
}
