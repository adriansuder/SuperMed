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
//using OpenQA.Selenium.Firefox;
using System.IO;
using SuperMed.Tests.E2E.Pages;

namespace SuperMed.Tests
{
    public class SeleniumTests
    {
        [TestFixture]
        public class SuperMedTests
        {
            IWebDriver _driver;
            HomePage homePage;
            RegisterPatientPage registerPatient;
            RegisterSuccesfulPage registerSuccesfull;
            LoginPage loginPage;
            PatientIndexPage patientPage;
            addAppoinmentPage addAppoinment;
            addAppoinmentStep2 step2;
            addAppoinmentStep3 step3;
            DoctorIndexPage doctorIndexPage;
            AddAbsencePage addAbsencePage;
            EditAbsencePage editAbsencePage;

            [SetUp]
            public void StartBrowser()
            {
                _driver = new ChromeDriver(@"C:\Users\adria\Desktop\REPOS\SuperMed\chromedriver_win32");
                _driver.Manage().Window.Maximize();
                homePage = new HomePage(_driver);
                registerPatient = new RegisterPatientPage(_driver);
                registerSuccesfull = new RegisterSuccesfulPage(_driver);
                loginPage = new LoginPage(_driver);
                patientPage = new PatientIndexPage(_driver);
                addAppoinment = new addAppoinmentPage(_driver);
                step2 = new addAppoinmentStep2(_driver);
                step3 = new addAppoinmentStep3(_driver);
                doctorIndexPage = new DoctorIndexPage(_driver);
                addAbsencePage = new AddAbsencePage(_driver);
                editAbsencePage = new EditAbsencePage(_driver);

            }

            [Test]
            public void RegisterPatient_ValidData_OpenPatientPanel()
            {
                Random rnd = new Random();
                var text = Guid.NewGuid().ToString();
                var date = "01.01.1990";
                var number = rnd.Next(500000000, 999999999).ToString();

                homePage.GoToHomePage()
                    .GoToRegisterPatientPage();
                registerPatient.fillName(text)
                    .fillPassword("!Supermed1")
                    .fillFirstName(text)
                    .fillLastName(text)
                    .fillMail(text + "@mail.pl")
                    .fillPhone(number)
                    .fillBirthday(date)
                    .fillGender("mężczyzna")
                    .clickRegister();
                Assert.That(registerSuccesfull.isRegisterSuccesfullInfoDisplayed, Is.True);
            }
            [Test]
            public void RegisterPatient_ToShortPassword_ReturnValidationError()
            {
                Random rnd = new Random();
                var text = Guid.NewGuid().ToString();
                var date = "01.01.1990";
                var number = rnd.Next(500000000, 999999999).ToString();

                homePage.GoToHomePage()
                    .GoToRegisterPatientPage();
                registerPatient.fillName(text)
                    .fillPassword("abc")
                    .fillFirstName(text)
                    .fillLastName(text)
                    .fillMail(text + "@mail.pl")
                    .fillPhone(number)
                    .fillBirthday(date)
                    .fillGender("mężczyzna")
                    .clickRegister();
                Assert.That(registerPatient.isValidationErrorDisplayed, Is.True);
            }

            [Test]
            public void LoginPatient_AddAppoinmentOnAvailableDate_Succes()
            {
                //change date on every test, second test call will return false
                var testDate = "08.02.2020";
                var name = "testPatient2";
                var description = "Podstawowa wizyta stomatologiczna";
                var password = "!SuperMed123";
                var selectDoctor = "Stomatolog - Jan Kowalski";

                homePage.GoToHomePage()
                    .GoToLoginPage();
                loginPage.fillUserName(name)
                    .fillUserPassword(password)
                    .ClickLogin();
                var count = _driver.FindElements(By.CssSelector("li.UpCommingAppoinments")).ToList().Count;
                patientPage.GoTo_addAppoinmentPage();
                addAppoinment.fillDate(testDate)
                    .fillDescription(description)
                    .selectDoctor(selectDoctor)
                    .GoToStep2();
                step2.selectHour()
                    .GoToStep3();
                step3.clickConfirm();
                StringAssert.EndsWith(_driver.Url, "https://localhost:44324/Patients");
                Assert.That(_driver.FindElements(By.CssSelector("li.UpCommingAppoinments")).ToList().Count == (count + 1));
            }

            [Test]
            public void LoginDoctor_AddAbsenceAndDeleteAbsence_Succesful()
            {
                var name = "doctor2";
                var password = "!SuperMed1";
                var absenceDate = "22.06.2020";
                var description = "Wyjazd na szkolenie";
                var dateElementId = absenceDate.Replace(".", string.Empty);

                homePage.GoToHomePage()
                    .GoToLoginPage();
                loginPage.fillUserName(name)
                    .fillUserPassword(password)
                    .ClickLogin();
                doctorIndexPage.GoToAddAbsencePage();
                addAbsencePage.fillAbsenceDate(absenceDate)
                    .fillAbsenceDescription(description)
                    .addAbsenceClick();
                Assert.That(doctorIndexPage.isAbsenceDisplayedOnIndexPage(dateElementId), Is.True);
                doctorIndexPage.GoToEditAbsencePage();
                editAbsencePage.clickOnDeleteButton(dateElementId)
                    .acceptAlert();
                Assert.That(doctorIndexPage.isAbsenceDisplayedOnIndexPage(dateElementId), Is.False);

            }

            [TearDown]
            public void CloseBrowser()
            {
                _driver.Close();
            }
        }
    }
}

