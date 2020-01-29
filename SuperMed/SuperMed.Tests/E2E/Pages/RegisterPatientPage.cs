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

        private IWebElement patientName => _driver.FindElement(By.Id("patientNameInput"));
        private IWebElement patientPassword => _driver.FindElement(By.Id("patientPasswordInput"));
        private IWebElement patientFirstName => _driver.FindElement(By.Id("patientFirstNameInput"));
        private IWebElement patientLastName => _driver.FindElement(By.Id("patientLastNameInput"));
        private IWebElement patientMail => _driver.FindElement(By.Id("patientMailInput"));
        private IWebElement patientPhone => _driver.FindElement(By.Id("patientPhoneInput"));
        private IWebElement patientBirthday => _driver.FindElement(By.Id("patientBirthdayInput"));
        private SelectElement patientGender => new SelectElement(_driver.FindElement(By.Id("patientGenderSelect")));
        private IWebElement registerButton => _driver.FindElement(By.ClassName("patientRegisterSubmit"));
        private IWebElement validationDiv => _driver.FindElement(By.ClassName("validation-summary-errors"));
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
