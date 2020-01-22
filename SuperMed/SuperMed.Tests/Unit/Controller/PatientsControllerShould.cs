//using FluentAssertions;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Moq;
//using NUnit.Framework;
//using SuperMed.Controllers;
//using SuperMed.DAL.Repositories.Interfaces;
//using SuperMed.Models.ViewModels;
//using System;
//using System.Security.Claims;
//using System.Threading;
//using System.Threading.Tasks;
//using SuperMed.Entities;
//using SuperMed.Services;

//namespace SuperMed.Tests.Unit.Controller
//{
//    [TestFixture]
//    public class PatientsControllerShould
//    {
//        private Mock<IAppService> _appService;

//        private Patient patient;

//        [SetUp]
//        public void Setup()
//        {
//            _appService = new Mock<IAppService>();

//            patient = new Patient
//            {
//                FirstName = "Testpatient",
//                LastName = "Testpatient"
//            };
//        }

//        [Test]
//        public async Task Index_Return_View()
//        {
//            var sut = new PatientsController(_appService.Object) {
//                ControllerContext = new ControllerContext
//                {
//                    HttpContext = new DefaultHttpContext
//                    {
//                        User = new ClaimsPrincipal(
//                            new ClaimsIdentity(new[] {new Claim(ClaimTypes.Name, "tesuser")},
//                                "testAuthType"))
//                    }
//                }
//            };

//            var result = await sut.Index();
//            result.Should().BeOfType<ViewResult>();
//        }
        
//        [Test]
//        public async Task CreateVisitStep2_Returns_View_When_ModelStateInvalid()
//        {
//            _appService.Setup(m => m.GetDoctorsAppointmentsForDay(UserManager,It.IsAny<string>(), It.IsAny<DateTime>()))
//                .ReturnsAsync(It.IsAny<DoctorAbsence>());
            
//            var sut = new PatientsController(_appService.Object)
//            {
//                ControllerContext = new ControllerContext
//                {
//                    HttpContext = new DefaultHttpContext
//                    {
//                        User = new ClaimsPrincipal(
//                            new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, "tesuser") },
//                                "testAuthType"))
//                    }
//                }
//            };

//            sut.ModelState.AddModelError("test", "test");
            
//            var result = await sut.CreateVisitStep2(new CreateVisitViewModel());
//            result.Should().BeOfType<ViewResult>();
//        }

//        [Test]
//        public async Task SubmitChangedInfo_Redirects_To_IndexAction()
//        {
//            _patientsRepositoryMock.Setup(m => m.GetPatientByName(It.IsAny<string>()))
//                .ReturnsAsync(patient);

//            var sut = new PatientsController(
//                _patientsRepositoryMock.Object,
//                _doctorsRepositoryMock.Object,
//                _specializationRepositoryMock.Object,
//                _appointmentsRepositoryMock.Object,
//                _absenceRepositoryMock.Object)
//            {
//                ControllerContext = new ControllerContext
//                {
//                    HttpContext = new DefaultHttpContext
//                    {
//                        User = new ClaimsPrincipal(
//                            new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, "tesuser") },
//                                "testAuthType"))
//                    }
//                }
//            };

//            var result = await sut.SubmitChangedInfo(new ChangePatientInfoViewModel(), CancellationToken.None);
//            result.Should().BeOfType<RedirectToActionResult>();
//        }
//    }
//}
