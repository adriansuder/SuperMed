using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;



namespace SuperMed.Tests
{
    public class SeleniumTests
    {
        [TestFixture]
        public class FirstTests
        {
            IWebDriver _driver;
            [SetUp]
            public void StartBrowser()
            {
                _driver = new ChromeDriver(@"C:\Users\adria\Desktop\REPOS\SuperMed\chromedriver_win32");
            }

            [Test]
            public void RegisterPatientTest()
            {
                Random rnd = new Random();
                var text = Guid.NewGuid().ToString();
                var date = "01.01.1990";
                var number = rnd.Next(500000000, 999999999).ToString();

                _driver.Navigate().GoToUrl("https://localhost:44324");
                _driver.FindElement(By.Id("registerPatient")).Click();
                _driver.FindElement(By.Id("patientNameInput")).SendKeys(text);
                _driver.FindElement(By.Id("patientPasswordInput")).SendKeys("!Supermed123");
                _driver.FindElement(By.Id("patientFirstNameInput")).SendKeys(text);
                _driver.FindElement(By.Id("patientLastNameInput")).SendKeys(text);
                _driver.FindElement(By.Id("patientMailInput")).SendKeys(text+"@mail.pl");
                _driver.FindElement(By.Id("patientPhoneInput")).SendKeys(number);
                _driver.FindElement(By.Id("patientBirthdayInput")).SendKeys(date);
                SelectElement select = new SelectElement(_driver.FindElement(By.Id("patientGenderSelect")));
                select.SelectByText("mężczyzna");

                IWebElement elementRegister = _driver.FindElement(By.ClassName("patientRegisterSubmit"));
                Actions actions = new Actions(_driver);
                actions.MoveToElement(elementRegister);
                _driver.FindElement(By.ClassName("patientRegisterSubmit")).Click();
                System.Threading.Thread.Sleep(1000);

                _driver.FindElement(By.Id("openPatientsPanel")).Click();

                StringAssert.StartsWith("https://localhost:44324/Patients", _driver.Url);
                System.Threading.Thread.Sleep(1000);
            }

            [TearDown]
            public void CloseBrowser()
            {
                _driver.Close();
            }
        }
    }
}

