using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System.Linq;
using SuperMed.Models.Entities;
using SuperMed.DAL;

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
            public void RegisterPatient_ValidData_OpenPatientPanel()
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
                _driver.FindElement(By.Id("patientMailInput")).SendKeys(text + "@mail.pl");
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

                StringAssert.EndsWith("https://localhost:44324/Patients", _driver.Url);
                System.Threading.Thread.Sleep(1000);
            }
            [Test]
            public void RegisterPatient_ToShortPassword_ReturnError()
            {
                Random rnd = new Random();
                var text = Guid.NewGuid().ToString();
                var date = "01.01.1990";
                var number = rnd.Next(500000000, 999999999).ToString();

                _driver.Navigate().GoToUrl("https://localhost:44324");
                _driver.FindElement(By.Id("registerPatient")).Click();
                _driver.FindElement(By.Id("patientNameInput")).SendKeys(text);
                _driver.FindElement(By.Id("patientPasswordInput")).SendKeys("abc");
                _driver.FindElement(By.Id("patientFirstNameInput")).SendKeys(text);
                _driver.FindElement(By.Id("patientLastNameInput")).SendKeys(text);
                _driver.FindElement(By.Id("patientMailInput")).SendKeys(text + "@mail.pl");
                _driver.FindElement(By.Id("patientPhoneInput")).SendKeys(number);
                _driver.FindElement(By.Id("patientBirthdayInput")).SendKeys(date);
                SelectElement select = new SelectElement(_driver.FindElement(By.Id("patientGenderSelect")));
                select.SelectByText("mężczyzna");
                IWebElement elementRegister = _driver.FindElement(By.ClassName("patientRegisterSubmit"));
                Actions actions = new Actions(_driver);
                actions.MoveToElement(elementRegister);
                _driver.FindElement(By.ClassName("patientRegisterSubmit")).Click();
                var validationDiv = _driver.FindElement(By.ClassName("validation-summary-errors"));
                if (validationDiv.Displayed)
                {
                    List<IWebElement> webElems = new List<IWebElement>
                    {
                        _driver.FindElement(By.XPath("//li[contains(text(), '6 characters.')]")),
                        _driver.FindElement(By.XPath("//li[contains(text(), 'alphanumeric character')]")),
                        _driver.FindElement(By.XPath("//li[contains(text(), 'at least one digit')]")),
                        _driver.FindElement(By.XPath("//li[contains(text(), 'at least one uppercase')]"))
                    };
                    Assert.That(webElems.Count == 4);
                    StringAssert.EndsWith("https://localhost:44324/Account/RegisterPatient", _driver.Url);
                }
                else
                {
                    Assert.That(validationDiv.Displayed == true);
                }
                
            }

            [Test]
            public void LoginPatient_AddAppoinmentOnAvailableDate_Succes()
            {
                var testDate = "24.04.2020";
                //change date on every test, second test call will return false
                var Name = "testPatient1";
                var description = "Podstawowa wizyta stomatologiczna";
                _driver.Navigate().GoToUrl("https://localhost:44324/Account/Login");
                _driver.FindElement(By.Id("Name")).SendKeys(Name);
                _driver.FindElement(By.Id("Password")).SendKeys("!Supermed123");
                _driver.FindElement(By.ClassName("btn-primary")).Click();
                var count = _driver.FindElements(By.CssSelector(".UpCommingAppoinments")).ToList().Count;
                _driver.FindElement(By.XPath("//a[contains(text(), 'Zarejestruj wizytę')]")).Click();
                StringAssert.StartsWith(_driver.Url, "https://localhost:44324/Patients/CreateVisit");
                SelectElement select = new SelectElement(_driver.FindElement(By.Id("DoctorName")));
                select.SelectByText("Stomatolog - Jan Kowalski");
                _driver.FindElement(By.Id("StartDateTime")).SendKeys(testDate);
                _driver.FindElement(By.Id("Description")).SendKeys(description);
                _driver.FindElement(By.Id("GoToStep2")).Click();
                _driver.FindElement(By.CssSelector("input[value='11:00']")).Click();
                _driver.FindElement(By.Id("DelButton")).Click(); //Go to Step 3
                _driver.FindElement(By.CssSelector("input[type='submit']")).Click();
                Assert.That(_driver.FindElements(By.CssSelector(".UpCommingAppoinments")).ToList().Count == (count + 1));
                StringAssert.StartsWith(_driver.Url, "https://localhost:44324/Patients");
            }

            [Test]
            public void LoginDoctor_AddAbsenceAndDeleteAbsence_Succesful()
            {
                var Name = "doctor2";
                var absenceDate = "25.06.2020";
                string date = absenceDate.Replace(".", string.Empty);
                _driver.Navigate().GoToUrl("https://localhost:44324");
                _driver.FindElement(By.LinkText("Logowanie")).Click();
                _driver.FindElement(By.Id("Name")).SendKeys(Name);
                _driver.FindElement(By.Id("Password")).SendKeys("!SuperMed1");
                _driver.FindElement(By.ClassName("btn-primary")).Click();
                _driver.FindElement(By.Id("addAbsenceButton")).Click();
                _driver.FindElement(By.Id("AbsenceDate")).SendKeys(absenceDate); 
                _driver.FindElement(By.Id("AbsenceDescription")).SendKeys("Wyjazd na szkolenie");
                _driver.FindElement(By.CssSelector("input[value='Dodaj']")).Click(); 
                _driver.FindElement(By.Id("editAbsences")).Click();
                IWebElement deleteButton = _driver.FindElement(By.Id(date));
                Actions actions = new Actions(_driver);
                actions.MoveToElement(deleteButton);
                deleteButton.Click();
                System.Threading.Thread.Sleep(5000);

            }

            [TearDown]
            public void CloseBrowser()
            {
                _driver.Close();
            }
        }
    }
}

